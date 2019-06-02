using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSC_Admin_Website.Models;

namespace SSC_WEB.Controllers
{
    public class ChucNangsController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: ChucNangs
        public ActionResult Index()
        {
            var chucNangs = db.ChucNangs.Include(c => c.LoaiNhom).ToList();
            return View(chucNangs);
        }

        // GET: ChucNangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNang chucNang = db.ChucNangs.Find(id);
            if (chucNang == null)
            {
                return HttpNotFound();
            }
            return View(chucNang);
        }

        // GET: ChucNangs/Create
        public ActionResult Create()
        {
            var AdminSuper = db.ChucNangs.SingleOrDefault(m => m.MaLN == 1);
            if (AdminSuper != null)
            {
                ViewBag.MaLN = new SelectList(db.LoaiNhoms.Where(m => m.MaLN != 1 ), "MaLN", "TenLN");
            }
            else
                ViewBag.MaLN = new SelectList(db.LoaiNhoms, "MaLN", "TenLN");

            return View();
        }

        // POST: ChucNangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCN,TenCN,MaLN,TrangThaiPV")] ChucNang cn)
        {
            if (ModelState.IsValid)
            {

                    db.sp_ChucNang_INSERT(cn.TenCN,cn.MaLN);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

            ViewBag.MaLN = new SelectList(db.LoaiNhoms, "MaLN", "TenLN", cn.MaLN);
            return View(cn);
        }

        // GET: ChucNangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNang chucNang = db.ChucNangs.Find(id);
            if (chucNang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLN = new SelectList(db.LoaiNhoms, "MaLN", "TenLN", chucNang.MaLN);
            return View(chucNang);
        }

        // POST: ChucNangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCN,TenCN,MaLN,TrangThaiPV")] ChucNang cn)
        {
            if (ModelState.IsValid)
            {
                db.sp_ChucNang_UPDATE(cn.MaCN,cn.TenCN,cn.MaLN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLN = new SelectList(db.LoaiNhoms, "MaLN", "TenLN", cn.MaLN);
            return View(cn);
        }

        // GET: ChucNangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNang chucNang = db.ChucNangs.Find(id);
            if (chucNang == null)
            {
                return HttpNotFound();
            }
            return View(chucNang);
        }

        // POST: ChucNangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChucNang chucNang = db.ChucNangs.Find(id);
            db.ChucNangs.Remove(chucNang);
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
