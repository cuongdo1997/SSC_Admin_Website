using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSC_Admin_Website.Models;
using PagedList;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;

namespace SSC_Admin_Website.Controllers
{
    public class KIOSKsController : Controller
    {
        private QLKIOSKEntities db = new QLKIOSKEntities();

        // GET: KIOSKs
        public ActionResult Index(int? page)
        {
            //Lấy pagesize
            Config cf_pagesize = db.Configs.SingleOrDefault(x => x.VariableName.Equals("kiosks_index_pagesize"));
            int pagenum = page ?? 1;
            int pagesize = cf_pagesize == null ? 5 : Convert.ToInt32(cf_pagesize.Value);

            List<V_KIOSK> list = db.V_KIOSK.ToList();
            return View(list.OrderBy(x=>x.NgayXD).ToPagedList(pagenum, pagesize));
        }

        // GET: KIOSKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KIOSKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKO,TenKO,NgayXD,NgayVH,DiaDiem,TrangThaiKo,ImageBanner,ConnectStr")] KIOSK kIOSK, HttpPostedFileBase fImageBanner)
        {
            //Trim string
            string mako = kIOSK.MAKO.Trim();
            string tenko = kIOSK.TenKO == null ? kIOSK.TenKO : kIOSK.TenKO.Trim();
            string diadiem = kIOSK.DiaDiem == null ? kIOSK.DiaDiem : kIOSK.DiaDiem.Trim();
            string connectstr = kIOSK.ConnectStr == null ? kIOSK.ConnectStr : kIOSK.ConnectStr.Trim();

            //Validate
            if (Utility.StringIsInvalid(mako) || mako.Length > 18)
            {
                ViewBag.Validate_MaKO = "Mã KIOSK không hợp lệ";
                return View(kIOSK);
            }
            if (tenko != null && (Utility.StringIsInvalid(tenko) || tenko.Length > 100))
            {
                ViewBag.Validate_TenKO = "Tên KIOSK không hợp lệ";
                return View(kIOSK);
            }
            if ((kIOSK.NgayXD != null && kIOSK.NgayVH == null) || (kIOSK.NgayXD == null && kIOSK.NgayVH != null))
            {
                ViewBag.Validate_Ngay = "Ngày xây dựng, ngày vận hành không hợp lệ";
            }
            if (kIOSK.NgayXD != null && kIOSK.NgayVH != null && kIOSK.NgayXD > kIOSK.NgayVH)
            {
                ViewBag.Validate_Ngay = "Ngày xây dựng phải <= ngày vận hành";
            }
            if (diadiem != null && (Utility.StringIsInvalid(diadiem) || diadiem.Length > 100))
            {
                ViewBag.Validate_DiaDiem = "Địa điểm không hợp lệ";
                return View(kIOSK);
            }
            if (connectstr != null && (Utility.StringIsInvalid(connectstr) || connectstr.Length > 200))
            {
                ViewBag.Validate_ConnectStr = "Connect string không hợp lệ";
                return View(kIOSK);
            }

            if (fImageBanner != null && fImageBanner.ContentLength > 0)
            {
                kIOSK.ImageBanner = new byte[fImageBanner.ContentLength]; // file1 to store image in binary formate  
                fImageBanner.InputStream.Read(kIOSK.ImageBanner, 0, fImageBanner.ContentLength);                
            }


            if (ModelState.IsValid)
            {
                try
                {
                    kIOSK.IsDeleted = false;
                    db.KIOSKs.Add(kIOSK);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }

            return View(kIOSK);
        }

        // GET: KIOSKs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KIOSK kIOSK = db.KIOSKs.Find(id);
            if (kIOSK == null)
            {
                return HttpNotFound();
            }
            return View(kIOSK);
        }

        // POST: KIOSKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKO,TenKO,NgayXD,NgayVH,DiaDiem,TrangThaiKo,ImageBanner,ConnectStr")] KIOSK kIOSK, HttpPostedFileBase fImageBanner, FormCollection f)
        {
            //Trim
            string mako = kIOSK.MAKO.Trim();
            string tenko = kIOSK.TenKO == null ? kIOSK.TenKO : kIOSK.TenKO.Trim();
            string diadiem = kIOSK.DiaDiem == null ? kIOSK.DiaDiem : kIOSK.DiaDiem.Trim();
            string connectstr = kIOSK.ConnectStr == null ? kIOSK.ConnectStr : kIOSK.ConnectStr.Trim();

            //Validate
            if (Utility.StringIsInvalid(mako) || mako.Length > 18)
            {
                ViewBag.validate_mako = "Mã KIOSK không hợp lệ";
                return View(kIOSK);
            }
            if (tenko != null && (Utility.StringIsInvalid(tenko) || tenko.Length > 100))
            {
                ViewBag.validate_tenko = "Tên KIOSK không hợp lệ";
                return View(kIOSK);
            }
            if ((kIOSK.NgayXD != null && kIOSK.NgayVH == null) || (kIOSK.NgayXD == null && kIOSK.NgayVH != null))
            {
                ViewBag.Validate_Ngay = "Ngày xây dựng, ngày vận hành không hợp lệ";
            }
            if (kIOSK.NgayXD != null && kIOSK.NgayVH != null && kIOSK.NgayXD > kIOSK.NgayVH)
            {
                ViewBag.Validate_Ngay = "Ngày xây dựng phải <= ngày vận hành";
            }
            if (diadiem != null && (Utility.StringIsInvalid(diadiem) || diadiem.Length > 100))
            {
                ViewBag.Validate_DiaDiem = "Địa điểm không hợp lệ";
                return View(kIOSK);
            }
            if (connectstr != null && (Utility.StringIsInvalid(connectstr) || connectstr.Length > 200))
            {
                ViewBag.Validate_ConnectStr = "Connect string không hợp lệ";
                return View(kIOSK);
            }

            KIOSK o = db.KIOSKs.FirstOrDefault(x => x.MAKO == kIOSK.MAKO);

            if (fImageBanner != null && fImageBanner.ContentLength > 0)
            {
                kIOSK.ImageBanner = new byte[fImageBanner.ContentLength]; // file1 to store image in binary formate  
                fImageBanner.InputStream.Read(kIOSK.ImageBanner, 0, fImageBanner.ContentLength);
                
                o.ImageBanner = kIOSK.ImageBanner;
            }
            else if (Convert.ToBoolean(f["isClear"]) == true)
            {
                kIOSK.ImageBanner = null;
                o.ImageBanner = kIOSK.ImageBanner;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    o.TenKO = kIOSK.TenKO;
                    o.NgayXD = kIOSK.NgayXD;
                    o.NgayVH = kIOSK.NgayVH;
                    o.DiaDiem = kIOSK.DiaDiem;
                    o.ConnectStr = kIOSK.ConnectStr;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
                }
            }

            return View(kIOSK);
        }

        // GET: KIOSKs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KIOSK kIOSK = db.KIOSKs.Find(id);
            if (kIOSK == null)
            {
                return HttpNotFound();
            }
            return View(kIOSK);
        }

        // POST: KIOSKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KIOSK kIOSK = db.KIOSKs.Find(id);
            if (kIOSK == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.sp_KIOSK_DELETE(id);
                }
                catch (SqlException ex)
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

        [ChildActionOnly]
        public ActionResult _ClearToken()
        {
            string password = ConfigurationManager.AppSettings["apipass"].ToString();
            ViewBag.APIPassword = password;
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> ClearToken(FormCollection f)
        {
            string password = f["password"];
            bool response = await KIOSKAPI_Web.ClearAllTokensAsync(password);
            return View(response);
        }
        
    }
}
