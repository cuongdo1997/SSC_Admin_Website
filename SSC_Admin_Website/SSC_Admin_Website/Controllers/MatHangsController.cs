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
using System.Data.SqlClient;

namespace SSC_Admin_Website.Controllers
{
    public class MatHangsController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: MatHangs
        public ActionResult Index(int? page)
        {
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("mathangs_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);
            List<V_MatHang> list = db.V_MatHang.ToList();
            return View(list.OrderBy(x => x.MaMH).ToPagedList(pagenum, pagesize));
        }

        // GET: MatHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaDV = new SelectList(db.DonVi_MatHang, "MaDV", "TenDV");
            return View();
        }

        // POST: MatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenMH,SoLuongTon,DonGia,MaDV,TrangThai")] MatHang matHang, HttpPostedFileBase HinhAnh)
        {
            ViewBag.MaDV = new SelectList(db.DonVi_MatHang, "MaDV", "TenDV", matHang.MaDV);
            string tenMH = Utility.Trim(matHang.TenMH);
            int? soLuongTon = matHang.SoLuongTon;
            decimal donGia;
            bool isValid;
            
            
            if (tenMH != null && (Utility.StringIsInvalid(tenMH) || tenMH.Length > 50))
            {
                ViewBag.Validate_TenMH = "Tên mặt hàng không hợp lệ";
                return View();
            }

            if (soLuongTon != null && soLuongTon < 0)
            {
                ViewBag.Validate_SoLuongTon = "Số lượng tồn không hợp lệ";
                return View();
            }

            isValid = decimal.TryParse(matHang.DonGia.ToString(), out donGia);
            if (!isValid)
            {
                ViewBag.Validate_DonGia = "Đơn giá không hợp lệ";
                return View();
            }

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                matHang.ImageMH = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(matHang.ImageMH, 0, HinhAnh.ContentLength);
            }

            if (ModelState.IsValid)
            {
                matHang.IsDeleted = false;
                db.MatHangs.Add(matHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(matHang);
        }

        // GET: MatHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = db.MatHangs.Find(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDV = new SelectList(db.DonVi_MatHang, "MaDV", "TenDV", matHang.MaDV);
            return View(matHang);
        }

        // POST: MatHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMH,TenMH,SoLuongTon,DonGia,MaDV,TrangThai,ImageMH")] MatHang matHang, HttpPostedFileBase HinhAnh, FormCollection f)
        {
            ViewBag.MaDV = new SelectList(db.DonVi_MatHang, "MaDV", "TenDV", matHang.MaDV);
            string tenMH = Utility.Trim(matHang.TenMH);
            int? soLuongTon = matHang.SoLuongTon;
            decimal donGia;
            bool isValid;


            if (tenMH != null && (Utility.StringIsInvalid(tenMH) || tenMH.Length > 50))
            {
                ViewBag.Validate_TenMH = "Tên mặt hàng không hợp lệ";
                return View();
            }

            if (soLuongTon != null && soLuongTon < 0)
            {
                ViewBag.Validate_SoLuongTon = "Số lượng tồn không hợp lệ";
                return View();
            }

            isValid = decimal.TryParse(matHang.DonGia.ToString(), out donGia);
            if (!isValid)
            {
                ViewBag.Validate_DonGia = "Đơn giá không hợp lệ";
                return View();
            }
            MatHang o = db.MatHangs.FirstOrDefault(x => x.MaMH == matHang.MaMH);

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                matHang.ImageMH = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(matHang.ImageMH, 0, HinhAnh.ContentLength);
                
                o.ImageMH = matHang.ImageMH;
            }
            else if (Convert.ToBoolean(f["isClear"]) == true)
            {
                matHang.ImageMH = null;
                o.ImageMH = matHang.ImageMH;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    o.TenMH = matHang.TenMH;
                    o.SoLuongTon = matHang.SoLuongTon;
                    o.MaDV = matHang.MaDV;
                    o.DonGia = matHang.DonGia;
                    o.TrangThai = matHang.TrangThai;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }
            return View(matHang);
        }

        // GET: MatHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = db.MatHangs.Find(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            return View(matHang);
        }

        // POST: MatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                db.sp_MatHang_DELETE(id);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
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
