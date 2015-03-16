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
    public class CategoryGranulesController : Controller
    {
        //
        public GJSEntities Db_gsj;
        // GET: /CategoryNews/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CATEGORY_GRANULES.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(O_CATEGORY_GRANULES CATEGORY_GRANULES)
        {
            
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CATEGORY_GRANULES.ACTIVE = true;
                CATEGORY_GRANULES.STATUS = 0;
                CATEGORY_GRANULES.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CATEGORY_GRANULES).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_GRANULES);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CATEGORY_GRANULES_CD)
        {
            if (CATEGORY_GRANULES_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CATEGORY_GRANULES CATEGORY_GRANULES_edit = Db_gsj.O_CATEGORY_GRANULES.Single(t => t.CATEGORY_GRANULES_CD == CATEGORY_GRANULES_CD);
            if (CATEGORY_GRANULES_edit == null)
                return HttpNotFound();
            return View(CATEGORY_GRANULES_edit);

        }

        [HttpPost]
        public ActionResult Edit(O_CATEGORY_GRANULES CATEGORY_GRANULES)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CATEGORY_GRANULES CATEGORY_GRANULES_edit = Db_gsj.O_CATEGORY_GRANULES.Single(t => t.CATEGORY_GRANULES_CD == CATEGORY_GRANULES.CATEGORY_GRANULES_CD);
                CATEGORY_GRANULES_edit.CATEGORY_GRANULES_CONTENT = CATEGORY_GRANULES.CATEGORY_GRANULES_CONTENT;
                CATEGORY_GRANULES_edit.CATEGORY_GRANULES_NAME = CATEGORY_GRANULES.CATEGORY_GRANULES_NAME;
                CATEGORY_GRANULES_edit.CATEGORY_GRANULES_WEIGHT = CATEGORY_GRANULES.CATEGORY_GRANULES_WEIGHT;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_GRANULES);
            }
        }


        public ActionResult Deactive([Bind(Include = "CATEGORY_GRANULES_CD")]O_CATEGORY_GRANULES CATEGORY_GRANULES)
        {
            if (CATEGORY_GRANULES == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_GRANULES = Db_gsj.O_CATEGORY_GRANULES.Single(t => t.CATEGORY_GRANULES_CD == CATEGORY_GRANULES.CATEGORY_GRANULES_CD);
            CATEGORY_GRANULES.ACTIVE = false;
            Db_gsj.Entry(CATEGORY_GRANULES).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CATEGORY_GRANULES_CD")]O_CATEGORY_GRANULES CATEGORY_GRANULES)
        {
            if (CATEGORY_GRANULES == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_GRANULES = Db_gsj.O_CATEGORY_GRANULES.Single(t => t.CATEGORY_GRANULES_CD == CATEGORY_GRANULES.CATEGORY_GRANULES_CD);
            CATEGORY_GRANULES.ACTIVE = true;
            Db_gsj.Entry(CATEGORY_GRANULES).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}