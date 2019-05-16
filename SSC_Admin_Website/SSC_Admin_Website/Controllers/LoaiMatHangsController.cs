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
    public class LoaiMatHangsController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: LoaiMatHangs
        public ActionResult Index(int? page)
        {
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("loaimathangs_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);
            List<V_LoaiMatHang> list = db.V_LoaiMatHang.ToList();
            return View(list.OrderBy(x => x.MaLMH).ToPagedList(pagenum, pagesize));
        }

        // GET: LoaiMatHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiMatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenLMH, ImageLMH")] LoaiMatHang loaiMatHang, HttpPostedFileBase HinhAnh)
        {
            string tenlmh = loaiMatHang.TenLMH == null ? null : loaiMatHang.TenLMH.Trim();

            if (Utility.StringIsInvalid(tenlmh) || tenlmh.Length > 50)
            {
                ViewBag.Validate_TenLMH = "Tên loại mặt hàng không hợp lệ";
                return View();
            }

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                loaiMatHang.ImageLMH = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(loaiMatHang.ImageLMH, 0, HinhAnh.ContentLength);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    loaiMatHang.IsDeleted = false;
                    db.LoaiMatHangs.Add(loaiMatHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }

            return View(loaiMatHang);
        }

        // GET: LoaiMatHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMatHang loaiMatHang = db.LoaiMatHangs.Find(id);
            if (loaiMatHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiMatHang);
        }

        // POST: LoaiMatHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLMH,TenLMH, ImageLMH")] LoaiMatHang loaiMatHang, HttpPostedFileBase HinhAnh, FormCollection f)
        {
            string tenlmh = loaiMatHang.TenLMH == null ? null : loaiMatHang.TenLMH.Trim();

            if (Utility.StringIsInvalid(tenlmh) || tenlmh.Length > 50)
            {
                ViewBag.Validate_TenLMH = "Tên loại mặt hàng không hợp lệ";
                return View();
            }

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                loaiMatHang.ImageLMH = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(loaiMatHang.ImageLMH, 0, HinhAnh.ContentLength);
                LoaiMatHang o = db.LoaiMatHangs.FirstOrDefault(x => x.MaLMH == loaiMatHang.MaLMH);
                o.ImageLMH = loaiMatHang.ImageLMH;
            }
            else if (Convert.ToBoolean(f["isClear"]) == true)
            {
                loaiMatHang.ImageLMH = null;
                LoaiMatHang o = db.LoaiMatHangs.FirstOrDefault(x => x.MaLMH == loaiMatHang.MaLMH);
                o.ImageLMH = loaiMatHang.ImageLMH;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }
            return View(loaiMatHang);
        }

        // GET: LoaiMatHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiMatHang loaiMatHang = db.LoaiMatHangs.Find(id);
            if (loaiMatHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiMatHang);
        }

        // POST: LoaiMatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiMatHang loaiMatHang = db.LoaiMatHangs.Find(id);
            if (loaiMatHang == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.sp_LoaiMatHang_DELETE(id);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }
            return RedirectToAction("Index");
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
