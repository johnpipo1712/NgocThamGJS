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
    public class CitiesController : Controller
    {
        //
        // GET: /Cities/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CITIES.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_CITIES CITIES)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CITIES.ACTIVE = true;
                CITIES.STATUS = 0;
                CITIES.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CITIES).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CITIES);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CITIES_CD)
        {
            if (CITIES_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CITIES CITIES_edit = Db_gsj.O_CITIES.Single(t => t.CITIES_CD == CITIES_CD);
            if (CITIES_edit == null)
                return HttpNotFound();
            return View(CITIES_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_CITIES CITIES)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CITIES CITIES_edit = Db_gsj.O_CITIES.Single(t => t.CITIES_CD == CITIES.CITIES_CD);
                CITIES_edit.CITIES_CODE = CITIES.CITIES_CODE;
                CITIES_edit.CITIES_NAME = CITIES.CITIES_NAME;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CITIES);
            }
        }


        public ActionResult Deactive([Bind(Include = "CITIES_CD")]O_CITIES CITIES)
        {
            if (CITIES == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CITIES = Db_gsj.O_CITIES.Single(t => t.CITIES_CD == CITIES.CITIES_CD);
            CITIES.ACTIVE = false;
            Db_gsj.Entry(CITIES).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CITIES_CD")]O_CITIES CITIES)
        {
            if (CITIES == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CITIES = Db_gsj.O_CITIES.Single(t => t.CITIES_CD == CITIES.CITIES_CD);
            CITIES.ACTIVE = true;
            Db_gsj.Entry(CITIES).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}