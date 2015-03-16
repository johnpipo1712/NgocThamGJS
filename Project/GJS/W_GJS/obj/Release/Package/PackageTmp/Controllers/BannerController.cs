using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using W_GJS.General;
using W_GJS.Models;

namespace W_GJS.Controllers
{
    public class BannerController : Controller
    {
        //
        // GET: /Banner/
             public GJSEntities Db_gsj;
        // GET: /CategoryNews/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_BANNER.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();
          
            return View();
        }

        [HttpPost]
        public ActionResult Create(O_BANNER BANNER)
        {

            if (ModelState.IsValid)
            {
                bool checkImage = false;
                checkImage = Process.CheckExtensionImg(BANNER.URL_IMAGE);
                if (checkImage == false)
                {
                    Db_gsj = new GJSEntities();
                    BANNER.ACTIVE = true;
                    BANNER.STATUS = 0;
                    BANNER.CREATEDATE = DateTime.Now;
                    Db_gsj.Entry(BANNER).State = EntityState.Added;
                    Db_gsj.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra đường dẫn hình ảnh");
          
                    return View(BANNER);
                }

            }
            else
            {
          
                return View(BANNER);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CD)
        {
            if (CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_BANNER BANNER_edit = Db_gsj.O_BANNER.Single(t => t.CD == CD);
            Db_gsj = new GJSEntities();
          
            if (BANNER_edit == null)
                return HttpNotFound();
            return View(BANNER_edit);

        }

        [HttpPost]
        public ActionResult Edit(O_BANNER BANNER)
        {
            if (ModelState.IsValid)
            {
                bool checkImage = false;
                checkImage = Process.CheckExtensionImg(BANNER.URL_IMAGE);
                if (checkImage == false)
                {
                    Db_gsj = new GJSEntities();
                    O_BANNER BANNER_edit = Db_gsj.O_BANNER.Single(t => t.CD == BANNER.CD);
                    BANNER_edit.URL_IMAGE = BANNER.URL_IMAGE;
                    Db_gsj.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra đường dẫn hình ảnh");
                 
                    return View(BANNER);
                }

            }
            else
            {
                
                return View(BANNER);
            }
        }


        public ActionResult Deactive([Bind(Include = "CD")]O_BANNER BANNER)
        {
            if (BANNER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            BANNER = Db_gsj.O_BANNER.Single(t => t.CD == BANNER.CD);
            BANNER.ACTIVE = false;
            Db_gsj.Entry(BANNER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CD")]O_BANNER BANNER)
        {
            if (BANNER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            BANNER = Db_gsj.O_BANNER.Single(t => t.CD == BANNER.CD);
            BANNER.ACTIVE = true;
            Db_gsj.Entry(BANNER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete([Bind(Include = "CD")]O_BANNER BANNER)
        {
            if (BANNER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_BANNER BANNER_delete = Db_gsj.O_BANNER.Single(t => t.CD == BANNER.CD);
            Db_gsj.Entry(BANNER_delete).State = EntityState.Deleted;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}