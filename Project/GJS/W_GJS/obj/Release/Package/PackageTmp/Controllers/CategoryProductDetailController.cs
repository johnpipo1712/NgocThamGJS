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
    public class CategoryProductDetailController : Controller
    {
        //
        // GET: /CategoryProductDetail/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CATEGORY_PRODUCT_DETAIL.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();
            var query = Db_gsj.O_CATEGORY_PRODUCT.Select(t => new { t.CATEGORY_PRODUCT_CD, t.CATEGORY_PRODUCT_NAME }).ToList();
            ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_CD", "CATEGORY_PRODUCT_NAME", 1);
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CATEGORY_PRODUCT_DETAIL.TAG_ALT = CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_NAME;
                CATEGORY_PRODUCT_DETAIL.ACTIVE = true;
                CATEGORY_PRODUCT_DETAIL.STATUS = 0;
                CATEGORY_PRODUCT_DETAIL.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CATEGORY_PRODUCT_DETAIL).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var query = Db_gsj.O_CATEGORY_PRODUCT.Select(t => new { t.CATEGORY_PRODUCT_CD, t.CATEGORY_PRODUCT_NAME }).ToList();
                ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_CD", "CATEGORY_PRODUCT_NAME", CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_CD);
         
                return View(CATEGORY_PRODUCT_DETAIL);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CATEGORY_PRODUCT_DETAIL_CD)
        {
            if (CATEGORY_PRODUCT_DETAIL_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL_edit = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL_CD);
            Db_gsj = new GJSEntities();
            var query = Db_gsj.O_CATEGORY_PRODUCT.Select(t => new { t.CATEGORY_PRODUCT_CD, t.CATEGORY_PRODUCT_NAME }).ToList();
            ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_CD", "CATEGORY_PRODUCT_NAME", CATEGORY_PRODUCT_DETAIL_edit.CATEGORY_PRODUCT_CD);
            if (CATEGORY_PRODUCT_DETAIL_edit == null)
                return HttpNotFound();
            return View(CATEGORY_PRODUCT_DETAIL_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL_edit = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_CD);
                CATEGORY_PRODUCT_DETAIL_edit.CATEGORY_PRODUCT_DETAIL_CODE = CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_CODE;
                CATEGORY_PRODUCT_DETAIL_edit.CATEGORY_PRODUCT_DETAIL_NAME = CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_NAME;
                CATEGORY_PRODUCT_DETAIL_edit.CATEGORY_PRODUCT_CD = CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_CD;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var query = Db_gsj.O_CATEGORY_PRODUCT.Select(t => new { t.CATEGORY_PRODUCT_CD, t.CATEGORY_PRODUCT_NAME }).ToList();
                ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_CD", "CATEGORY_PRODUCT_NAME", CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_CD);
                return View(CATEGORY_PRODUCT_DETAIL);
            }
        }


        public ActionResult Deactive([Bind(Include = "CATEGORY_PRODUCT_DETAIL_CD")]O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL)
        {
            if (CATEGORY_PRODUCT_DETAIL == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_PRODUCT_DETAIL = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_CD);
            CATEGORY_PRODUCT_DETAIL.ACTIVE = false;
            Db_gsj.Entry(CATEGORY_PRODUCT_DETAIL).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CATEGORY_PRODUCT_DETAIL_CD")]O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL)
        {
            if (CATEGORY_PRODUCT_DETAIL == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_PRODUCT_DETAIL = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_CD);
            CATEGORY_PRODUCT_DETAIL.ACTIVE = true;
            Db_gsj.Entry(CATEGORY_PRODUCT_DETAIL).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}