using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSC_Admin_Website.Models;

namespace SSC_Admin_Website.Controllers
{
    public class HinhThuc_ThanhToansController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: HinhThuc_ThanhToans
        public ActionResult Index()
        {
            return View(db.HinhThuc_ThanhToan.ToList());
        }

        // GET: HinhThuc_ThanhToans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThuc_ThanhToan hinhThuc_ThanhToan = db.HinhThuc_ThanhToan.Find(id);
            if (hinhThuc_ThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(hinhThuc_ThanhToan);
        }

        // GET: HinhThuc_ThanhToans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HinhThuc_ThanhToans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHTTT,TenHTTT,IsDeleted,ImageHTTT")] HinhThuc_ThanhToan hinhThuc_ThanhToan, HttpPostedFileBase HinhAnh)
        {
            string tenHTTT = hinhThuc_ThanhToan.TenHTTT == null ? null : hinhThuc_ThanhToan.TenHTTT.Trim();

            if (Utility.StringIsInvalid(tenHTTT) || tenHTTT.Length > 50)
            {
                ViewBag.Validate_TenLMH = "Tên HTTT không hợp lệ";
                return View();
            }

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                hinhThuc_ThanhToan.ImageHTTT = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(hinhThuc_ThanhToan.ImageHTTT, 0, HinhAnh.ContentLength);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    hinhThuc_ThanhToan.IsDeleted = false;
                    db.HinhThuc_ThanhToan.Add(hinhThuc_ThanhToan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }

            return View(hinhThuc_ThanhToan);
        }

        // GET: HinhThuc_ThanhToans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThuc_ThanhToan hinhThuc_ThanhToan = db.HinhThuc_ThanhToan.Find(id);
            if (hinhThuc_ThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(hinhThuc_ThanhToan);
        }

        // POST: HinhThuc_ThanhToans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHTTT,TenHTTT,IsDeleted,ImageHTTT")] HinhThuc_ThanhToan hinhThuc_ThanhToan, HttpPostedFileBase HinhAnh, FormCollection f)
        {
            string tenHTTT = hinhThuc_ThanhToan.TenHTTT == null ? null : hinhThuc_ThanhToan.TenHTTT.Trim();

            if (Utility.StringIsInvalid(tenHTTT) || tenHTTT.Length > 50)
            {
                ViewBag.Validate_TenLMH = "Tên loại mặt hàng không hợp lệ";
                return View();
            }

            HinhThuc_ThanhToan o = db.HinhThuc_ThanhToan.FirstOrDefault(x => x.MaHTTT == hinhThuc_ThanhToan.MaHTTT);

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                hinhThuc_ThanhToan.ImageHTTT = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(hinhThuc_ThanhToan.ImageHTTT, 0, HinhAnh.ContentLength);

                o.ImageHTTT = hinhThuc_ThanhToan.ImageHTTT;
            }
            else if (Convert.ToBoolean(f["isClear"]) == true)
            {
                hinhThuc_ThanhToan.ImageHTTT = null;
                o.ImageHTTT = hinhThuc_ThanhToan.ImageHTTT;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    o.TenHTTT = hinhThuc_ThanhToan.TenHTTT;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }
            return View(hinhThuc_ThanhToan);
        }

        // GET: HinhThuc_ThanhToans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThuc_ThanhToan hinhThuc_ThanhToan = db.HinhThuc_ThanhToan.Find(id);
            if (hinhThuc_ThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(hinhThuc_ThanhToan);
        }

        // POST: HinhThuc_ThanhToans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhThuc_ThanhToan hinhThuc_ThanhToan = db.HinhThuc_ThanhToan.Find(id);
            db.HinhThuc_ThanhToan.Remove(hinhThuc_ThanhToan);
            db.SaveChanges();
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
