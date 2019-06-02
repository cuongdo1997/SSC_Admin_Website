using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSC_Admin_Website.Models;
using PagedList;

namespace SSC_Admin_Website.Controllers
{
    public class TaiKhoanAdminsController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: TaiKhoans
        public ActionResult Index(int? page)
        {
            return View();
        }

        public ActionResult _GetAccountsByLoaiNhom(int maln, int? page)
        {
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("taikhoanadmins_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);

            
            List<sp_GetAccountsByMaLN_Result> taiKhoans = db.sp_GetAccountsByMaLN(maln).ToList();
            return PartialView(taiKhoans.ToPagedList(pagenum, pagesize));
        }

        // GET: TaiKhoans/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            string cmnd = f["CMND"];
            string tenkh = f["TenKH"];
            string diachi = f["DiaChi"];
            string email = f["Email"];
            string sdt = f["SDT"];

            int loainhom = Convert.ToInt32(f["LoaiNhom"]);
            string username = f["Username"];
            string password = f["Password"];
            string repassword = f["RetypePassword"];
            bool trangthai = Convert.ToBoolean(f["TrangThai"]);

            if (Utility.StringIsInvalid(username) || username.Length > 50)
            {
                ViewBag.Validate_Username = "Invalid username";
                return View();
            }

            if (Utility.StringIsInvalid(password))
            {
                ViewBag.Validate_Password = "Invalid password";
                return View();
            }

            if (!password.Equals(repassword))
            {
                ViewBag.Validate_MatchPassword = "Password does not match";
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    password = Utility.CreateMD5(Utility.Base64Encode(password));
                    db.sp_KhachHang_INSERT(cmnd, tenkh, diachi, email, sdt);
                    int makh = db.sp_MaKHMoi().First().Value;
                    db.sp_TaiKhoan_INSERT(username, password, makh, null);
                    int matk = db.sp_TKMoi().First().Value;
                    List<int?> macns = db.sp_ChucNangTheoLoaiNhom(loainhom).ToList();
                    if (macns.Count > 0)
                    {
                        foreach (int macn in macns)
                        {
                            db.sp_PhanQuyen_INSERT(matk, macn, false);
                        }
                        
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            
            return View();
        }

        public JsonResult CheckKhachHang(string cmnd)
        {
            int count = db.sp_checkKH(cmnd).ToList().Count;
            if (count == 0)
                return Json(count, JsonRequestBehavior.AllowGet);
            else
            {
                var query = db.sp_checkKH(cmnd).ToList();
                return Json(query, JsonRequestBehavior.AllowGet);
            }
        }

        [ChildActionOnly]
        public ActionResult _DropDown_LoaiNhom()
        {
            List<LoaiNhom> list = db.LoaiNhoms.Where(x=>x.MaLN != 1 && x.MaLN != 4).ToList();
            return PartialView(list);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Enable(int matk)
        {
            try
            {
                TaiKhoan tk = db.TaiKhoans.SingleOrDefault(x => x.MaTK == matk);
                if (tk == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                db.sp_TaiKhoan_Enable(tk.MaTK);
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult Disable(int matk)
        {
            try
            {
                TaiKhoan tk = db.TaiKhoans.SingleOrDefault(x => x.MaTK == matk);
                if (tk == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                db.sp_TaiKhoan_Disable(tk.MaTK);
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        public ActionResult TaiKhoan()
        {
            //List<PHANQUYEN> list = Session["TaiKhoan"] as List<PHANQUYEN>;
            if (Session["TaiKhoan"] == null)
            {
                return PartialView();
            }
            else
            {
                TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
                ViewBag.TenKH = db.KhachHangs.SingleOrDefault(m => m.MaKH == tk.MaKH).TenKH;
                int maln = db.sp_CN_Login(tk.MaTK).First().Value;
                ViewBag.tenln = db.LoaiNhoms.Single(m => m.MaLN == maln).TenLN;
                return PartialView(tk);
            }

        }
        public ActionResult FormLogin()
        {
            return View();
        }
        [HttpPost, ActionName("FormLogin")]
        [ValidateAntiForgeryToken]
        public ActionResult FormLogin(string name, string password, string lat12, string lon12)
        {
            password = EnCryptMD5.MD5Hash(EncodeBase64.Base64Encode(password));
            TaiKhoan tk = db.TaiKhoans.SingleOrDefault(m => m.Username == name && m.Password == password);
            if (tk == null)
            {
                ViewData["Loi1"] = "Tài khoản hoặc mật khẩu sai rồi!!!";
            }
            else
            {
                int maln = db.sp_CN_Login(tk.MaTK).First().Value;
                string vitri = "Vĩ độ:" + lat12 + ", Kinh độ :" + lon12;
                db.sp_LichSu_DangNhap_INSERT(tk.MaTK, vitri);
                db.SaveChanges();
                string tenln = db.LoaiNhoms.Single(m => m.MaLN == maln).TenLN;
                if (tenln.Contains("Khách Hàng"))
                {
                    Session["TaiKhoan"] = tk;
                    return RedirectToAction("Create", "DotKhuyenMais");
                }
                else
                {
                    Session["TaiKhoan"] = tk;
                    return RedirectToAction("SuperAdmin", "TaiKhoans");
                }

            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Login", "TaiKhoan");
        }



        //end class
    }
}
