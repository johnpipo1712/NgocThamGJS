using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using W_GJS.Models;
namespace W_GJS.Controllers
{
    public class CategoryNewsController : Controller
    {
        //
        public GJSEntities Db_gsj;
        // GET: /CategoryNews/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CATEGORY_NEWS.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_CATEGORY_NEWS CATEGORY_NEWS)
        {
            
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CATEGORY_NEWS.ACTIVE = true;
                CATEGORY_NEWS.STATUS = 0;
                CATEGORY_NEWS.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CATEGORY_NEWS).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_NEWS);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CATEGORY_NEWS_CD)
        {
            if (CATEGORY_NEWS_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CATEGORY_NEWS CATEGORY_NEWS_edit = Db_gsj.O_CATEGORY_NEWS.Single(t => t.CATEGORY_NEWS_CD == CATEGORY_NEWS_CD);
            if (CATEGORY_NEWS_edit == null)
                return HttpNotFound();
            return View(CATEGORY_NEWS_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_CATEGORY_NEWS CATEGORY_NEWS)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CATEGORY_NEWS CATEGORY_NEWS_edit = Db_gsj.O_CATEGORY_NEWS.Single(t => t.CATEGORY_NEWS_CD == CATEGORY_NEWS.CATEGORY_NEWS_CD);
                CATEGORY_NEWS_edit.CATEGORY_NEWS_CODE = CATEGORY_NEWS.CATEGORY_NEWS_CODE;
                CATEGORY_NEWS_edit.CATEGORY_NEWS_NAME = CATEGORY_NEWS.CATEGORY_NEWS_NAME;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_NEWS);
            }
        }


        public ActionResult Deactive([Bind(Include = "CATEGORY_NEWS_CD")]O_CATEGORY_NEWS CATEGORY_NEWS)
        {
            if (CATEGORY_NEWS == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_NEWS = Db_gsj.O_CATEGORY_NEWS.Single(t => t.CATEGORY_NEWS_CD == CATEGORY_NEWS.CATEGORY_NEWS_CD);
            CATEGORY_NEWS.ACTIVE = false;
            Db_gsj.Entry(CATEGORY_NEWS).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CATEGORY_NEWS_CD")]O_CATEGORY_NEWS CATEGORY_NEWS)
        {
            if (CATEGORY_NEWS == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_NEWS = Db_gsj.O_CATEGORY_NEWS.Single(t => t.CATEGORY_NEWS_CD == CATEGORY_NEWS.CATEGORY_NEWS_CD);
            CATEGORY_NEWS.ACTIVE = true;
            Db_gsj.Entry(CATEGORY_NEWS).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}