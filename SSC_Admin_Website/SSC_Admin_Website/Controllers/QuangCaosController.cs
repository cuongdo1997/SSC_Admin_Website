using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSC_Admin_Website.Models;
using PagedList;

namespace SSC_Admin_Website.Controllers
{
    public class QuangCaosController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: QuangCaos
        public ActionResult Index(int? page)
        {
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("quangcaos_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);

            List<V_QuangCao> list = db.V_QuangCao.ToList();
            return View(list.OrderByDescending(x => x.MaQC).ToPagedList(pagenum, pagesize));
        }

        // GET: QuangCaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuangCaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQC,NoiDung,ImageQC,ThoiLuong")] QuangCao quangCao, HttpPostedFileBase HinhAnh)
        {
            string noidung = quangCao.NoiDung == null ? quangCao.NoiDung : quangCao.NoiDung.Trim();
            int thoiluong = quangCao.ThoiLuong;

            if (noidung != null && (Utility.StringIsInvalid(noidung) || noidung.Length > 200))
            {
                ViewBag.Validate_NoiDung = "";
                return View(quangCao);
            }

            if (thoiluong < 0)
            {
                ViewBag.Validate_ThoiLuong = "";
                return View(quangCao);
            }

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                quangCao.ImageQC = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(quangCao.ImageQC, 0, HinhAnh.ContentLength);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    quangCao.IsDeleted = false;
                    db.QuangCaos.Add(quangCao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }

            return View(quangCao);
        }

        // GET: QuangCaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: QuangCaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQC,NoiDung,ImageQC,ThoiLuong")] QuangCao quangCao, HttpPostedFileBase HinhAnh, FormCollection f)
        {
            string noidung = quangCao.NoiDung == null ? quangCao.NoiDung : quangCao.NoiDung.Trim();
            int thoiluong = quangCao.ThoiLuong;

            if (noidung != null && (Utility.StringIsInvalid(noidung) || noidung.Length > 200))
            {
                ViewBag.Validate_NoiDung = "";
                return View(quangCao);
            }

            if (thoiluong < 0)
            {
                ViewBag.Validate_ThoiLuong = "";
                return View(quangCao);
            }

            QuangCao o = db.QuangCaos.FirstOrDefault(x => x.MaQC == quangCao.MaQC);

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                quangCao.ImageQC = new byte[HinhAnh.ContentLength]; // file1 to store image in binary formate  
                HinhAnh.InputStream.Read(quangCao.ImageQC, 0, HinhAnh.ContentLength);
                o.ImageQC = quangCao.ImageQC;
            }
            else if (Convert.ToBoolean(f["isClear"]) == true)
            {
                quangCao.ImageQC = null;
                o.ImageQC = quangCao.ImageQC;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    o.NoiDung = quangCao.NoiDung;
                    o.ThoiLuong = quangCao.ThoiLuong;
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }

            return View(quangCao);
        }

        // GET: QuangCaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.QuangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: QuangCaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                db.sp_QuangCao_DELETE(id);
            }
            catch (SqlException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
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
