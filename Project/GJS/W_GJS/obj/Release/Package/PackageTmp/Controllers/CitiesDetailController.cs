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
    public class CitiesDetailController : Controller
    {
        //
        // GET: /CITIES_DETAIL_Detail/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.D_CITIES_DETAIL.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();
            var query = Db_gsj.O_CITIES.Select(t => new { t.CITIES_CD, t.CITIES_NAME }).ToList();
            ViewBag.Cities = new SelectList(query.AsEnumerable(), "CITIES_CD", "CITIES_NAME", 1);
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(D_CITIES_DETAIL CITIES_DETAIL)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CITIES_DETAIL.ACTIVE = true;
                CITIES_DETAIL.STATUS = 0;
                CITIES_DETAIL.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CITIES_DETAIL).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var query = Db_gsj.O_CITIES.Select(t => new { t.CITIES_CD, t.CITIES_NAME }).ToList();
                ViewBag.Cities = new SelectList(query.AsEnumerable(), "CITIES_CD", "CITIES_NAME", CITIES_DETAIL.CITIES_CD);
                
                return View(CITIES_DETAIL);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CITIES_DETAIL_CD)
        {
            if (CITIES_DETAIL_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            D_CITIES_DETAIL CITIES_DETAIL_edit = Db_gsj.D_CITIES_DETAIL.Single(t => t.CITIES_DETAIL_CD == CITIES_DETAIL_CD);
            var query = Db_gsj.O_CITIES.Select(t => new { t.CITIES_CD, t.CITIES_NAME }).ToList();
            ViewBag.Cities = new SelectList(query.AsEnumerable(), "CITIES_CD", "CITIES_NAME", CITIES_DETAIL_edit.CITIES_DETAIL_CD);
            
            if (CITIES_DETAIL_edit == null)
                return HttpNotFound();
            return View(CITIES_DETAIL_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(D_CITIES_DETAIL CITIES_DETAIL)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                D_CITIES_DETAIL CITIES_DETAIL_edit = Db_gsj.D_CITIES_DETAIL.Single(t => t.CITIES_DETAIL_CD == CITIES_DETAIL.CITIES_DETAIL_CD);
                CITIES_DETAIL_edit.CITIES_DETAIL_CODE = CITIES_DETAIL.CITIES_DETAIL_CODE;
                CITIES_DETAIL_edit.CITIES_DETAIL_NAME = CITIES_DETAIL.CITIES_DETAIL_NAME;
                CITIES_DETAIL_edit.CITIES_CD = CITIES_DETAIL.CITIES_CD;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var query = Db_gsj.O_CITIES.Select(t => new { t.CITIES_CD, t.CITIES_NAME }).ToList();
                ViewBag.Cities = new SelectList(query.AsEnumerable(), "CITIES_CD", "CITIES_NAME", CITIES_DETAIL.CITIES_CD);
                return View(CITIES_DETAIL);
            }
        }


        public ActionResult Deactive([Bind(Include = "CITIES_DETAIL_CD")]D_CITIES_DETAIL CITIES_DETAIL)
        {
            if (CITIES_DETAIL == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CITIES_DETAIL = Db_gsj.D_CITIES_DETAIL.Single(t => t.CITIES_DETAIL_CD == CITIES_DETAIL.CITIES_DETAIL_CD);
            CITIES_DETAIL.ACTIVE = false;
            Db_gsj.Entry(CITIES_DETAIL).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CITIES_DETAIL_CD")]D_CITIES_DETAIL CITIES_DETAIL)
        {
            if (CITIES_DETAIL == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CITIES_DETAIL = Db_gsj.D_CITIES_DETAIL.Single(t => t.CITIES_DETAIL_CD == CITIES_DETAIL.CITIES_DETAIL_CD);
            CITIES_DETAIL.ACTIVE = true;
            Db_gsj.Entry(CITIES_DETAIL).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}