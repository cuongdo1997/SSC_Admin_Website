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

        // GET: DotKhuyenMais
        public ActionResult Index(int? page)
        {
            //Lấy pagesize
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("dotkhuyenmais_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);

            List<V_KIOSK> list = db.V_KIOSK.ToList();
            return View(list.OrderBy(x => x.NgayXD).ToPagedList(pagenum, pagesize));
        }

        // GET: DotKhuyenMais/Create
        public ActionResult Create()
        {
            ViewBag.SoHD = new SelectList(db.HopDongs, "SoHD", "NoiDungHD");
            return View();
        }

        // POST: DotKhuyenMais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKM,NgayTao,NgayBD,NgayKT,SoHD,TrangThaiPV")] DotKhuyenMai dotKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.DotKhuyenMais.Add(dotKhuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoHD = new SelectList(db.HopDongs, "SoHD", "NoiDungHD", dotKhuyenMai.SoHD);
            return View(dotKhuyenMai);
        }

        // GET: DotKhuyenMais/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.SoHD = new SelectList(db.HopDongs, "SoHD", "NoiDungHD", dotKhuyenMai.SoHD);
            return View(dotKhuyenMai);
        }

        // POST: DotKhuyenMais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKM,NgayTao,NgayBD,NgayKT,SoHD,TrangThaiPV")] DotKhuyenMai dotKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dotKhuyenMai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SoHD = new SelectList(db.HopDongs, "SoHD", "NoiDungHD", dotKhuyenMai.SoHD);
            return View(dotKhuyenMai);
        }

        // GET: DotKhuyenMais/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: DotKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DotKhuyenMai dotKhuyenMai = db.DotKhuyenMais.Find(id);
            db.DotKhuyenMais.Remove(dotKhuyenMai);
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
