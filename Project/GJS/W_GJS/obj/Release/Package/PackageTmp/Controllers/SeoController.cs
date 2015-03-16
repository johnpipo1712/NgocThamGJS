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
    public class SeoController : Controller
    {
        //
        // GET: /Seo/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_SEO.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_SEO SEO)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                SEO.ACTIVE = true;
                SEO.STATUS = 0;
                SEO.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(SEO).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(SEO);
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
            O_SEO SEO_edit = Db_gsj.O_SEO.Single(t => t.CD == CD);
            if (SEO_edit == null)
                return HttpNotFound();
            return View(SEO_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_SEO SEO)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_SEO SEO_edit = Db_gsj.O_SEO.Single(t => t.CD == SEO.CD);
                SEO_edit.META_DES = SEO.META_DES;
                SEO_edit.META_KEYWORDS = SEO.META_KEYWORDS;
                SEO_edit.META_REVISIT = SEO.META_REVISIT;
                SEO_edit.META_ROBOTS = SEO.META_ROBOTS;
                SEO_edit.TITILE = SEO.TITILE;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(SEO);
            }
        }


        public ActionResult Deactive([Bind(Include = "CD")]O_SEO SEO)
        {
            if (SEO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            SEO = Db_gsj.O_SEO.Single(t => t.CD == SEO.CD);
            SEO.ACTIVE = false;
            Db_gsj.Entry(SEO).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CD")]O_SEO SEO)
        {
            if (SEO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            SEO = Db_gsj.O_SEO.Single(t => t.CD == SEO.CD);
            SEO.ACTIVE = true;
            Db_gsj.Entry(SEO).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}