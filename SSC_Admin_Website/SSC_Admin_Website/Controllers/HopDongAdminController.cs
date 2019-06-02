
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSC_Admin_Website.Models;
namespace SSC_Admin_Website.Controllers
{
    public class HopDongAdminController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();
        public JsonResult CountKO(int malhd)
        {
            int dem = db.sp_KO_ChuaSuDung(malhd,0).ToList().Count;
            return Json(dem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetKOChuaSuDung(int malhd, int sl)
        {
            var query = db.sp_KO_ChuaSuDung(malhd, 0).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckKhachHang(string cmnd)
        {
            int count = db.sp_checkKH(cmnd).ToList().Count;
            if(count == 0)
                return Json(count, JsonRequestBehavior.AllowGet);
            else
            {
                var query = db.sp_checkKH(cmnd).ToList();
                return Json(query, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: HopDong
        public ActionResult CreateHopDong()
        {
            ViewBag.MaLHD =db.LoaiHopDongs.Where(m => m.MaLHD != 3).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHopDong(HopDong hd, FormCollection f)      
        {
            string checkcmnd = "";
            int sl = 0;
            string hoten ="";
            string cmnd = "";
            string diachi = "";
            string email = "";
            string sdt = "";
            ViewBag.MaLHD = db.LoaiHopDongs.Where(m => m.MaLHD != 3).ToList();
            if (!Validation.StringIsInvalid(f["checkcmnd"].ToString()) && Validation.StringIsCMND(f["checkcmnd"].ToString()))
            {
                checkcmnd = f["checkcmnd"].ToString().Trim();
            }else
            {
                ViewBag.Checkcmnd = "Vui lòng kiểm tra lại CMND đã nhập.";
            }
            if (Validation.StringIsNumber(f["sl"].ToString()))
            {
                sl = Convert.ToInt32(f["sl"].ToString());
            }
            else
            {
                ViewBag.Checksl = "Vui lòng nhập lại số lượng KIOSK.";
            }
            
            // code catch error
            if (checkcmnd.Length ==12 || checkcmnd.Length == 9)
            {
                //Check Họ và tên
                if (!Validation.StringIsInvalid(f["hoten"].ToString()) && f["hoten"].ToString().Length <=50)
                {
                    hoten = f["hoten"].ToString().Trim();
                }
                else
                {
                    ViewBag.hoten = "Vui lòng kiểm tra lại Họ và Tên đã nhập.";
                    return View();
                }
                //Check cmnd
                if (!Validation.StringIsInvalid(f["cmnd"].ToString()) && Validation.StringIsCMND(f["cmnd"].ToString()))
                {
                    cmnd = f["cmnd"].ToString().Trim();
                }
                else
                {
                    ViewBag.Checkcmnd = "Vui lòng kiểm tra lại CMND đã nhập.";
                    return View();
                }
                //Chek dia chỉ
                if (!Validation.StringIsInvalid(f["diachi"].ToString()) && f["diachi"].ToString().Length <= 150)
                {
                    diachi = f["diachi"].ToString().Trim();
                }
                else
                {
                    ViewBag.diachi = "Vui lòng kiểm tra lại địa chỉ đã nhập.";
                    return View();
                }
                //Check Email
                if (!Validation.StringIsInvalid(f["email"].ToString()) && Validation.StringIsEmail(f["email"].ToString()) && f["email"].ToString().Length <= 50)
                {
                    email = f["email"].ToString().Trim();
                }
                else
                {
                    ViewBag.email = "Vui lòng kiểm tra lại Email đã nhập.";
                    return View();
                }
                //Check Số Điện Thoaik
                if (!Validation.StringIsInvalid(f["sdt"].ToString()) && Validation.StringIsPhoneNumber(f["sdt"].ToString()))
                {
                    sdt = f["sdt"].ToString().Trim();
                }
                else
                {
                    ViewBag.sdt = "Vui lòng kiểm tra lại Số Điện Thoại đã nhập.";
                    return View();
                }
                //check malhd
                int malhd = 0;
                if (!Validation.StringIsInvalid(f["malhd"].ToString()) && Validation.StringIsNumber(f["malhd"].ToString()))
                {
                    malhd = Convert.ToInt32(f["malhd"].ToString().Trim());
                }
                else
                {
                    ViewBag.lhd = "Vui lòng chọn loại hợp đồng.";
                    return View();
                }
                
                
                if (malhd == 0)
                {
                    ViewBag.lhd = "Vui lòng chọn Loại Hợp Đồng";
                    return View();
                }
                int count = db.sp_checkKH(cmnd).ToList().Count;
                DateTime ngaylaphd = DateTime.Now.AddDays(1);
                if (!Validation.StringIsInvalid(f["ngaylaphd"].ToString()))
                {
                    ngaylaphd = Convert.ToDateTime(f["ngaylaphd"].ToString().Trim());
                    if(ngaylaphd == DateTime.Now)
                    {
                        ViewBag.ngaylaphd = "Vui lòng kiểm tra lại ngày lập hóa đơn.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ngaylaphd = "Vui lòng kiểm tra lại ngày lập hóa đơn.";
                    return View();
                }

                if (count == 0)
                {      
                    db.sp_KhachHang_INSERT(cmnd, hoten, diachi, email, sdt);
                    db.SaveChanges();
                    int makh = db.sp_MaKHMoi().First().Value;
                    var query = db.sp_KO_ChuaSuDung(malhd, sl).ToList();
                    if (malhd == 1)
                    {
                        db.sp_HopDong_INSERT(ngaylaphd.Date, hd.NoiDungHD, malhd, makh, null);
                        db.SaveChanges();
                        int mahd = db.sp_HDMoi().First().Value;
                        string username = "";
                        string pass =  "";
                        if (!Validation.StringIsInvalid(f["username"].ToString()) && f["username"].ToString().Length <= 50)
                        {
                            username = f["username"].ToString().Trim();
                        }
                        else
                        {
                            ViewBag.username = "Vui lòng kiểm tra lại Username đã nhập.";
                            return View();
                        }
                        if (!Validation.StringIsInvalid(f["pass"].ToString()) && f["pass"].ToString().Length <= 50 && Validation.StringIsPass(f["pass"].ToString()))
                        {
                            pass = f["pass"].ToString().Trim();
                        }
                        else
                        {
                            ViewBag.password = "Vui lòng kiểm tra lại mật khẩu đã nhập.";
                            return View();
                        }

                        db.sp_TaiKhoan_INSERT(username,EnCryptMD5.MD5Hash(EncodeBase64.Base64Encode(pass)), makh, mahd);
                        db.SaveChanges();
                        int matk = db.sp_TKMoi().First().Value;
                        int macn = db.sp_GetMaCN("user").First().Value;
                        db.sp_PhanQuyen_INSERT(matk, macn,true);
                        foreach (var item in query)
                        {
                            string mako = item.MAKO;
                            DateTime ngaybd = DateTime.Now.AddDays(1);
                            DateTime ngaykt = DateTime.Now.AddDays(1);
                            //check ngay bat dau
                            if (!Validation.StringIsInvalid(f["NgayBD_" + item.MAKO].ToString()))
                            {
                                ngaybd = Convert.ToDateTime(f["NgayBD_" + item.MAKO].ToString().Trim());
                                if (ngaybd == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                return View();
                            }
                            //check ngay ket thuc
                            if (!Validation.StringIsInvalid(f["NgayKT_" + item.MAKO].ToString()))
                            {
                                ngaykt = Convert.ToDateTime(f["NgayKT_" + item.MAKO].ToString().Trim());
                                if (ngaykt == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                return View();
                            }
                            
                            db.sp_ChiTietHopDong_INSERT(mahd, mako, ngaybd.Date, ngaykt.Date);
                            db.SaveChanges();

                        }
                        return RedirectToAction("ManageHopDong", "HopDong");
                    }
                    else
                    {
                        db.sp_HopDong_INSERT(ngaylaphd.Date, hd.NoiDungHD, malhd, null, makh);
                        int a = hd.SoHD;
                        db.SaveChanges();
                        int mahd = db.sp_HDMoi().First().Value;
                        foreach (var item in query)
                        {
                            DateTime ngaybd = DateTime.Now.AddDays(1);
                            DateTime ngaykt = DateTime.Now.AddDays(1);
                            //check ngay bat dau
                            if (!Validation.StringIsInvalid(f["NgayBD_" + item.MAKO].ToString()))
                            {
                                ngaybd = Convert.ToDateTime(f["NgayBD_" + item.MAKO].ToString().Trim());
                                if (ngaybd == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                return View();
                            }
                            //check ngay ket thuc
                            if (!Validation.StringIsInvalid(f["NgayKT_" + item.MAKO].ToString()))
                            {
                                ngaykt = Convert.ToDateTime(f["NgayKT_" + item.MAKO].ToString().Trim());
                                if (ngaykt == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                return View();
                            }
                            db.sp_ChiTietThueKIOSKQC_INSERT(mahd, item.MAKO.ToString(), ngaybd.Date, ngaykt.Date);
                            db.SaveChanges();
                        }
                        return RedirectToAction("ManageHopDong", "HopDong");
                    }
                }
                else
                {
                    int makh = db.sp_checkKH(cmnd).First().MaKH;
                    var query = db.sp_KO_ChuaSuDung(malhd, sl).ToList();
                    if (malhd == 1)
                    {
                        db.sp_HopDong_INSERT(ngaylaphd.Date, hd.NoiDungHD, malhd, makh, null);

                        db.SaveChanges();
                        int mahd = db.sp_HDMoi().First().Value;
                        foreach (var item in query)
                        {
                            string mako = item.MAKO;
                            DateTime ngaybd = DateTime.Now.AddDays(1);
                            DateTime ngaykt = DateTime.Now.AddDays(1);
                            //check ngay bat dau
                            if (!Validation.StringIsInvalid(f["NgayBD_" + item.MAKO].ToString()))
                            {
                                ngaybd = Convert.ToDateTime(f["NgayBD_" + item.MAKO].ToString().Trim());
                                if (ngaybd == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                return View();
                            }
                            //check ngay ket thuc
                            if (!Validation.StringIsInvalid(f["NgayKT_" + item.MAKO].ToString()))
                            {
                                ngaykt = Convert.ToDateTime(f["NgayKT_" + item.MAKO].ToString().Trim());
                                if (ngaykt == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                return View();
                            }
                            db.sp_ChiTietHopDong_INSERT(mahd, mako, ngaybd.Date, ngaykt.Date);
                            db.SaveChanges();

                        }
                        return RedirectToAction("ManageHopDong", "HopDong");
                    }
                    else
                    {
                        db.sp_HopDong_INSERT(ngaylaphd.Date, hd.NoiDungHD, malhd, null, makh);
                        int a = hd.SoHD;
                        db.SaveChanges();
                        int mahd = db.sp_HDMoi().First().Value;
                        foreach (var item in query)
                        {
                            DateTime ngaybd = DateTime.Now.AddDays(1);
                            DateTime ngaykt = DateTime.Now.AddDays(1);
                            //check ngay bat dau
                            if (!Validation.StringIsInvalid(f["NgayBD_" + item.MAKO].ToString()))
                            {
                                ngaybd = Convert.ToDateTime(f["NgayBD_" + item.MAKO].ToString().Trim());
                                if (ngaybd == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                return View();
                            }
                            //check ngay ket thuc
                            if (!Validation.StringIsInvalid(f["NgayKT_" + item.MAKO].ToString()))
                            {
                                ngaykt = Convert.ToDateTime(f["NgayKT_" + item.MAKO].ToString().Trim());
                                if (ngaykt == DateTime.Now.AddDays(1))
                                {
                                    ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                return View();
                            }
                            db.sp_ChiTietThueKIOSKQC_INSERT(mahd, item.MAKO.ToString(), ngaybd.Date, ngaykt.Date);
                            db.SaveChanges();
                        }
                        return RedirectToAction("ManageHopDong", "HopDong");
                    }
            //----------------
                }
            }else
            {
                ViewData["checkcmnd"] = "Vui lòng nhập đúng số Chứng Minh Nhân Dân.";
                return View();
            }
                 
        }
        // Get Hợp đồng theo mã loại hợp đồng
        public ActionResult Get_HopDong_ByMaLHD (int malhd)
        {
            return PartialView(db.sp_GetHopDongByMaLHD(malhd).ToList());
        }
        // GetDetail Hop Dong
        [HttpPost]
        public ActionResult HD_Detail(int sohd)
        {
            ViewBag.Ngaylaphd = db.HopDongs.Single(m => m.SoHD == sohd).NgayLapHD;
            // Láy chi tiết Hợp đồng
            return PartialView(db.sp_Detail_HopDongBySoHD(sohd).ToList());
        }
        [HttpPost]
        public ActionResult HD_Customer(int sohd)
        {
            // Lấy thông tin khách hàng
            return PartialView(db.sp_GetKhachHang_ByHopDong(sohd).First());
        }

        public JsonResult testnha(int sohd, DateTime ngaykt, string mako)
        {
            db.sp_ChiTietHopDong_UPDATE(sohd, ngaykt, mako);
            return Json(JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManageHopDong()
        {
            ViewBag.MaLHD = db.LoaiHopDongs.Where(m => m.MaLHD != 3).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ManageHopDong(FormCollection data)
        {
            string result = "success";
            ViewBag.MaLHD = db.LoaiHopDongs.Where(m => m.MaLHD != 3).ToList();
            int malhd = 0;
            if (!Validation.StringIsInvalid(data["malhd"]) && Validation.StringIsNumber(data["malhd"]))
            {
                malhd = Convert.ToInt32(data["malhd"].ToString().Trim());
            }
            else
            {
                ViewBag.lhd = "Vui lòng chọn loại hợp đồng.";
                result = "failed";
                return View();
            }
            var query = db.sp_KO_ChuaSuDung(malhd, 0).ToList();
            foreach (var item in query)
            {
                if (!Validation.StringIsInvalid(data["mako" + item.MAKO]))
                {
                    if (data["mako" + item.MAKO].ToString() == item.MAKO)
                    {
                        DateTime ngaybd = DateTime.Now.AddDays(1);
                        DateTime ngaykt = DateTime.Now.AddDays(1);
                        //check ngay bat dau
                        if (!Validation.StringIsInvalid(data["NgayBD_" + item.MAKO]))
                        {
                            ngaybd = Convert.ToDateTime(data["NgayBD_" + item.MAKO].ToString().Trim());
                            if (ngaybd == DateTime.Now.AddDays(1))
                            {
                                ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                                result = "failed";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.ngaybd = "Vui lòng kiểm tra lại ngày bắt đầu của Kiosk.";
                            result = "failed";
                            return View();
                        }
                        //check ngay ket thuc
                        if (!Validation.StringIsInvalid(data["NgayKT_" + item.MAKO]))
                        {
                            ngaykt = Convert.ToDateTime(data["NgayKT_" + item.MAKO].ToString().Trim());
                            if (ngaykt == DateTime.Now.AddDays(1))
                            {
                                ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                                result = "failed";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.ngaykt = "Vui lòng kiểm tra lại ngày kết thúc Kiosk.";
                            result = "failed";
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        int mahd = int.Parse(data["mahd"].ToString().Trim());
                        int count = db.sp_Check_KO_SoHD(mahd, item.MAKO.ToString(), malhd).First().Value;
                        if (malhd == 1)
                        {
                            if (count == 0)
                            {
                                db.sp_ChiTietHopDong_INSERT(mahd, item.MAKO.ToString(), ngaybd.Date, ngaykt.Date);
                            }
                            else
                            {
                                db.sp_ChiTietHopDong_UPDATE(mahd, ngaykt.Date, item.MAKO.ToString());
                            }

                        }
                        else
                        {
                            if (count == 0)
                            {
                                db.sp_ChiTietThueKIOSKQC_INSERT(mahd, item.MAKO.ToString(), ngaybd.Date, ngaykt.Date);
                            }
                            else
                            {
                                db.sp_ChiTietThueKIOSKQC_UPDATE(mahd, ngaykt.Date, item.MAKO.ToString());
                            }

                        }
                    }
                }
            }

            return View();
        }
    }
}