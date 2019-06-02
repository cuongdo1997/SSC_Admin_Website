using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSC_Admin_Website.Models;
using PagedList;

namespace SSC_Admin_Website.Controllers
{
    public class DotKhuyenMaisController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();
        
        public bool CheckTaiKhoan()
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk == null)
                return false;
            else
                return true;
        }
        // GET: DotKhuyenMais
        public ActionResult Index(int? page)
        {
            if(CheckTaiKhoan() == true)
            {
                //Lấy pagesize
                Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("dotkhuyenmais_index_pagesize"));
                int pagenum = page ?? 1;
                int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);

                List<DotKhuyenMai> list = db.DotKhuyenMais.ToList();
                return View(list.OrderBy(x => x.NgayBD).ToPagedList(pagenum, pagesize));
            }
            else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }
        }
        public ActionResult Detail(int? id)
        {
            if (CheckTaiKhoan() == true)
            {
                ViewBag.GiamGiaMhTheoSl = db.sp_GiamGiaSP_ByMaKM(id).ToList();
                ViewBag.count1 = db.sp_GiamGiaSP_ByMaKM(id).Count();
                ViewBag.GiamGiaTGDH = db.sp_GiamGiaTGDH_ByMaKM(id).ToList();
                ViewBag.count2 = db.sp_GiamGiaTGDH_ByMaKM(id).Count();
                return View();
            }
            else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }

        }
        public ActionResult LoadGiamGiaSP(int? page)
        {
            //if (CheckTaiKhoan() == true)
            //{
            //    Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("dotkhuyenmais_index_pagesize"));
            //    int pagenum = page ?? 1;
            //    int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);
            //    var query = db.GetMatHang_ByDKM().ToList();
            //    return PartialView(query.ToPagedList(pagenum, pagesize));
            //}
            //else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }

        }
        public ActionResult LoadGiamTGDH()
        {
            if (CheckTaiKhoan() == true)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }
        }
        // GET: DotKhuyenMais/Create
        public ActionResult Create()    
        {
            if(CheckTaiKhoan() == false)
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }
            else
            {
                TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
                ViewBag.SoHD = db.sp_Get_SOHD(tk.MaTK).First().Value;
                // kiem tra tai khoan co quyen tao khuyen mai k
                ViewBag.checktk = db.sp_checkCN_TK("khuyến mãi", tk.MaTK).First().Value;
                if (ViewBag.checktk == true)
                {
                    return View();
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Not Found Page");
            }
             
            
        }

        // POST: DotKhuyenMais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( FormCollection f)
        {
            TaiKhoan tk = Session["TaiKhoan"] as TaiKhoan;
            if (tk == null)
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }
            else
            {
                int sohd = db.sp_Get_SOHD(tk.MaTK).First().Value;
                int malkm = 0;
                
                if (Validation.StringIsInvalid(f["malkm"].ToString()))
                {
                    ViewBag.malkm = "Vui lòng chọn hình thức khuyến mãi.";
                }
                else
                {
                    malkm = int.Parse(f["malkm"].ToString());
                }
                if (Validation.StringIsInvalid(f["ngaybd"].ToString()) || Validation.StringIsInvalid(f["ngaykt"].ToString()))
                {
                    ViewBag.malkm = "Vui lòng kiểm tra lại ngày bắt đầu và ngày kết thúc.";
                }
                else
                {
                    DateTime ngaybd = Convert.ToDateTime(f["ngaybd"].ToString());
                    DateTime ngaykt = Convert.ToDateTime(f["ngaykt"].ToString());
                    // code add vào đợt khuyen mãi
                    db.sp_DotKhuyenMai_INSERT(ngaybd.Date, ngaykt.Date, sohd);
                    int makm = db.DotKhuyenMais.OrderByDescending(m => m.MaKM).FirstOrDefault().MaKM;
                    if (malkm == 0)
                    {
                        //var query = db.GetMatHang_ByDKM().ToList();
                        //foreach (var item in query)
                        //{

                        //    if(string.Compare(f["check" + item.MaMH], item.MaMH.ToString()) == 0)
                        //    {
                        //        if (!Validation.StringIsInvalid(f["gtgmh" + item.MaMH].ToString()))
                        //        {
                        //            decimal giam = (decimal.Parse(f["gtgmh" + item.MaMH].ToString())) / 100;
                        //            db.sp_ChiTietGiamGiaTheoSL_INSERT(makm, int.Parse(item.MaMH.ToString()), 1, giam);
                        //        }
                        //    }
                        //    //else
                        //    //{
                        //    //    ViewBag.MatHang = "Vui lòng chọn mặt hàng tích vào ô check để được khuyến mẫi";
                        //    //    return View();
                        //    //}
                        //}
                        return RedirectToAction("Index", "DotKhuyenMais");
                    }
                    else
                    {
                        decimal TongTriGia = 0;
                        decimal TriGiaGiam = 0;
                        if (!Validation.StringIsInvalid(f["ttgth"].ToString()) && !Validation.StringIsInvalid(f["gtgdh"].ToString()))
                        {
                                TongTriGia = Decimal.Parse(f["ttgth"].ToString());
                                TriGiaGiam = (Decimal.Parse(f["gtgdh"].ToString()))/100;
                                db.sp_KMTheoTGHD_INSERT(TongTriGia, TriGiaGiam, makm);
                                return RedirectToAction("Index", "DotKhuyenMais");
                        }
                    }
                }
                return View();
            };
        }

        // GET: DotKhuyenMais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (CheckTaiKhoan() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DotKhuyenMai dotKhuyenMai = db.DotKhuyenMais.Find(id);
                if (dotKhuyenMai == null)
                {
                    return HttpNotFound();
                }
                return View(dotKhuyenMai);
            }
            else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }
        }

        // POST: DotKhuyenMais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKM,NgayTao,NgayBD,NgayKT,SoHD,TrangThaiPV")] DotKhuyenMai dkm)
        {
            if (CheckTaiKhoan() == true)
            {
                if (ModelState.IsValid)
                {
                    db.sp_DotKhuyenMai_UPDATE(dkm.MaKM, dkm.NgayBD, dkm.NgayKT);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dkm);
            }
            else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }
        }

        // GET: DotKhuyenMais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (CheckTaiKhoan() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DotKhuyenMai dotKhuyenMai = db.DotKhuyenMais.Find(id);
                if (dotKhuyenMai == null)
                {
                    return HttpNotFound();
                }
                return View(dotKhuyenMai);
            }
            else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }

        }

        // POST: DotKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (CheckTaiKhoan() == true)
            {
                db.sp_DotKhuyenMai_DELETE(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("FormLogin", "TaiKhoanAdmins");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
