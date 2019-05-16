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
    public class DonVi_MatHangsController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: DonVi_MatHang
        public ActionResult Index(int? page)
        {
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("donvimathangs_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);
            List<V_DonViMatHang> list = db.V_DonViMatHang.ToList();
            return View(list.OrderBy(x => x.MaDV).ToPagedList(pagenum, pagesize));
        }

        // GET: DonVi_MatHang/Create
        public ActionResult Create()
        {
            ViewBag.MALMH = new SelectList(db.LoaiMatHangs, "MaLMH", "TenLMH");
            return View();
        }

        // POST: DonVi_MatHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDV,TenDV,MALMH")] DonVi_MatHang donVi_MatHang)
        {
            ViewBag.MALMH = new SelectList(db.LoaiMatHangs, "MaLMH", "TenLMH", donVi_MatHang.MALMH);
            //Trim
            donVi_MatHang.TenDV = donVi_MatHang.TenDV == null ? null : donVi_MatHang.TenDV.Trim();

            //Validate
            if (Utility.StringIsInvalid(donVi_MatHang.TenDV) || donVi_MatHang.TenDV.Length > 50)
            {
                ViewBag.Validate_TenDV = "Tên đơn vị không hợp lệ";
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.sp_DonVi_MatHang_INSERT(donVi_MatHang.TenDV, donVi_MatHang.MALMH);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }
            
            return View(donVi_MatHang);
        }

        // GET: DonVi_MatHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi_MatHang donVi_MatHang = db.DonVi_MatHang.Find(id);
            if (donVi_MatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MALMH = new SelectList(db.LoaiMatHangs, "MaLMH", "TenLMH", donVi_MatHang.MALMH);
            return View(donVi_MatHang);
        }

        // POST: DonVi_MatHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDV,TenDV,MALMH,TrangThaiPV")] DonVi_MatHang donVi_MatHang)

        {
            ViewBag.MALMH = new SelectList(db.LoaiMatHangs, "MaLMH", "TenLMH", donVi_MatHang.MALMH);
            //Trim
            donVi_MatHang.TenDV = donVi_MatHang.TenDV == null ? null : donVi_MatHang.TenDV.Trim();

            //Validate
            if (Utility.StringIsInvalid(donVi_MatHang.TenDV) || donVi_MatHang.TenDV.Length > 50)
            {
                ViewBag.Validate_TenDV = "Tên đơn vị không hợp lệ";
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.sp_DonVi_MatHang_UPDATE(donVi_MatHang.MaDV, donVi_MatHang.TenDV, donVi_MatHang.MALMH);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }
            ViewBag.MALMH = new SelectList(db.LoaiMatHangs, "MaLMH", "TenLMH", donVi_MatHang.MALMH);
            return View(donVi_MatHang);
        }

        // GET: DonVi_MatHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi_MatHang donVi_MatHang = db.DonVi_MatHang.Find(id);
            if (donVi_MatHang == null)
            {
                return HttpNotFound();
            }
            return View(donVi_MatHang);
        }

        // POST: DonVi_MatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonVi_MatHang donVi_MatHang = db.DonVi_MatHang.Find(id);
            if (donVi_MatHang == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.sp_DonVi_MatHang_DELETE(id);
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
