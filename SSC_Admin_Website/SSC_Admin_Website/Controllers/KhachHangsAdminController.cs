﻿using System;
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
    public class KhachHangsAdminController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: KhachHangs
        public ActionResult Index(int? page)
        {
            //Lấy pagesize
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("khachhangs_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);

            List<V_KhachHang> khachHangs = db.V_KhachHang.ToList();
            return View(khachHangs.ToPagedList(pagenum, pagesize));
        }

        // GET: KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,CMND,TenKH,DiaChi,Email,SDT,TrangThaiPV")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.sp_KhachHang_INSERT(kh.CMND, kh.TenKH, kh.DiaChi, kh.Email, kh.SDT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }
        // GET: KhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,CMND,TenKH,DiaChi,Email,SDT")] KhachHang khachHang)
        {
            //Trim
            khachHang.TenKH = khachHang.TenKH == null ? null : khachHang.TenKH.Trim();
            khachHang.DiaChi = khachHang.DiaChi == null ? null : khachHang.DiaChi.Trim();
            khachHang.Email = khachHang.Email == null ? null : khachHang.Email.Trim();
            khachHang.SDT = khachHang.SDT == null ? null : khachHang.SDT.Trim();

            //Validate
            if (Utility.StringIsInvalid(khachHang.CMND) || khachHang.CMND.Length > 13 || !Utility.StringIsCMND(khachHang.CMND))
            {
                ViewBag.Validate_CMND = "CMND không hợp lệ";
                return View(khachHang);
            }
            if (khachHang.TenKH != null && (Utility.StringIsInvalid(khachHang.TenKH) || khachHang.TenKH.Length > 50))
            {
                ViewBag.Validate_TenKH = "Tên khách hàng không hợp lệ";
                return View(khachHang);
            }
            if (khachHang.DiaChi != null && (Utility.StringIsInvalid(khachHang.DiaChi) || khachHang.DiaChi.Length > 150))
            {
                ViewBag.Validate_DiaChi = "Địa chỉ không hợp lệ";
                return View(khachHang);
            }
            if (khachHang.Email != null && (Utility.StringIsInvalid(khachHang.Email) || !Utility.StringIsEmail(khachHang.Email) || khachHang.Email.Length > 50))
            {
                ViewBag.Validate_Email = "Email không hợp lệ";
                return View(khachHang);
            }
            if (khachHang.SDT != null && (Utility.StringIsInvalid(khachHang.SDT) || !Utility.StringIsPhoneNumber(khachHang.SDT) || khachHang.SDT.Length > 10))
            {
                ViewBag.Validate_SDT = "SĐT không hợp lệ";
                return View(khachHang);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.sp_KhachHang_UPDATE(khachHang.MaKH, khachHang.CMND, khachHang.TenKH, khachHang.DiaChi, khachHang.Email, khachHang.SDT);
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }
            return View(khachHang);
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
