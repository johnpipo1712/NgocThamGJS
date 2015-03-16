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
    public class NewsController : Controller
    {
        //
        // GET: /News/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_NEWS.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();
            var queryD = Db_gsj.O_CATEGORY_NEWS.ToList();
            ViewBag.categoryNews = new SelectList(queryD.AsEnumerable(), "CATEGORY_NEWS_CD", "CATEGORY_NEWS_NAME", 1);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
   
        public ActionResult Create(O_NEWS NEWS)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                NEWS.ACTIVE = true;
                NEWS.STATUS = 0;
                NEWS.CREATEDATE = DateTime.Now;
                NEWS.EMPLOYEE_CD = 1;
                Db_gsj.Entry(NEWS).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var queryD = Db_gsj.O_CATEGORY_NEWS.ToList();
                ViewBag.categoryNews = new SelectList(queryD.AsEnumerable(), "CATEGORY_NEWS_CD", "CATEGORY_NEWS_NAME", NEWS.CATEGORY_NEWS_CD);
           
                return View(NEWS);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? NEWS_CD)
        {
            if (NEWS_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_NEWS NEWS_edit = Db_gsj.O_NEWS.Single(t => t.NEWS_CD == NEWS_CD);
            var queryD = Db_gsj.O_CATEGORY_NEWS.ToList();
            ViewBag.categoryNews = new SelectList(queryD.AsEnumerable(), "CATEGORY_NEWS_CD", "CATEGORY_NEWS_NAME", NEWS_edit.CATEGORY_NEWS_CD);
           
            if (NEWS_edit == null)
                return HttpNotFound();
            return View(NEWS_edit);

        }

        [HttpPost]
        [ValidateInput(false)]
   
        public ActionResult Edit(O_NEWS NEWS)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_NEWS NEWS_edit = Db_gsj.O_NEWS.Single(t => t.NEWS_CD == NEWS.NEWS_CD);
                NEWS_edit.IMAGE_NEWS = NEWS.IMAGE_NEWS;
                NEWS_edit.NEWS_CONTENT = NEWS.NEWS_CONTENT;
                NEWS_edit.NEW_DESCRIPTIONS = NEWS.NEW_DESCRIPTIONS;
                NEWS_edit.NEWS_TITLE = NEWS.NEWS_TITLE;
                NEWS_edit.SOURCE_COPY = NEWS.SOURCE_COPY;
                NEWS_edit.O_CATEGORY_NEWS = NEWS.O_CATEGORY_NEWS;
                NEWS_edit.CATEGORY_NEWS_CD = NEWS.CATEGORY_NEWS_CD;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var queryD = Db_gsj.O_CATEGORY_NEWS.ToList();
                ViewBag.categoryNews = new SelectList(queryD.AsEnumerable(), "CATEGORY_NEWS_CD", "CATEGORY_NEWS_NAME", NEWS.CATEGORY_NEWS_CD);
           
                return View(NEWS);
            }
        }


        public ActionResult Deactive([Bind(Include = "NEWS_CD")]O_NEWS NEWS)
        {
            if (NEWS == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            NEWS = Db_gsj.O_NEWS.Single(t => t.NEWS_CD == NEWS.NEWS_CD);
            NEWS.ACTIVE = false;
            Db_gsj.Entry(NEWS).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "NEWS_CD")]O_NEWS NEWS)
        {
            if (NEWS == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            NEWS = Db_gsj.O_NEWS.Single(t => t.NEWS_CD == NEWS.NEWS_CD);
            NEWS.ACTIVE = true;
            Db_gsj.Entry(NEWS).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult CheckNewAjax(long? NEWS_CD, String CHECK)
        {
            Db_gsj = new GJSEntities();
            if (CHECK == "true")
            {
                O_NEWS NEWS = Db_gsj.O_NEWS.Single(t => t.NEWS_CD == NEWS_CD);

                O_NEWS_NEW NEWSNEW = new O_NEWS_NEW();
                NEWSNEW.O_NEWS = NEWS;
                NEWSNEW.NEWS_CD = NEWS.NEWS_CD;
                NEWSNEW.ACTIVE = true;
                NEWSNEW.STATUS = 0;
                NEWSNEW.CREATEDATE = DateTime.Now;

                NEWS.O_NEWS_NEW.Add(NEWSNEW);
                Db_gsj.Entry(NEWSNEW).State = EntityState.Added;
                Db_gsj.SaveChanges();

                //NEWS.CREATEDATE = DateTime.Now;
                //Db_gsj.Entry(NEWS).State = EntityState.Modified;
                //Db_gsj.SaveChanges();
            }
            else
            {
                List<O_NEWS_NEW> NEWSNEW = Db_gsj.O_NEWS_NEW.Where(t => t.NEWS_CD == NEWS_CD).ToList();
                Db_gsj.O_NEWS_NEW.RemoveRange(NEWSNEW);
                Db_gsj.SaveChanges();
            }
            return Json(1);
        }
        public JsonResult CheckHightlightAjax(long? NEWS_CD, String CHECK)
        {
            Db_gsj = new GJSEntities();
            if (CHECK == "true")
            {
                O_NEWS NEWS = Db_gsj.O_NEWS.Single(t => t.NEWS_CD == NEWS_CD);
                O_NEWS_HIGHLIGHTS NEWSHIGHLIGHTS = new O_NEWS_HIGHLIGHTS();
                NEWSHIGHLIGHTS.O_NEWS = NEWS;
                NEWSHIGHLIGHTS.NEWS_CD = NEWS.NEWS_CD;
                NEWSHIGHLIGHTS.ACTIVE = true;
                NEWSHIGHLIGHTS.STATUS = 0;
                NEWSHIGHLIGHTS.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(NEWSHIGHLIGHTS).State = EntityState.Added;
                Db_gsj.SaveChanges();
            }
            else
            {
                List<O_NEWS_HIGHLIGHTS> NEWSHIGHLIGHTS = Db_gsj.O_NEWS_HIGHLIGHTS.Where(t => t.NEWS_CD == NEWS_CD).ToList();
                Db_gsj.O_NEWS_HIGHLIGHTS.RemoveRange(NEWSHIGHLIGHTS);
                Db_gsj.SaveChanges();
            }
            return Json(1);
        }
    }
}