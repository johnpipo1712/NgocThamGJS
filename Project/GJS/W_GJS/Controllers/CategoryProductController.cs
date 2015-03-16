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
    public class CategoryProductController : Controller
    {
        //
        // GET: /CategoryProduct/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CATEGORY_PRODUCT.Where(t => t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_CATEGORY_PRODUCT CATEGORY_PRODUCT)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CATEGORY_PRODUCT.TAG_ALT = CATEGORY_PRODUCT.CATEGORY_PRODUCT_NAME;
                CATEGORY_PRODUCT.ACTIVE = true;
                CATEGORY_PRODUCT.STATUS = 0;
                CATEGORY_PRODUCT.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CATEGORY_PRODUCT).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_PRODUCT);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CATEGORY_PRODUCT_CD)
        {
            if (CATEGORY_PRODUCT_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CATEGORY_PRODUCT CATEGORY_PRODUCT_edit = Db_gsj.O_CATEGORY_PRODUCT.Single(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT_CD);
            if (CATEGORY_PRODUCT_edit == null)
                return HttpNotFound();
            return View(CATEGORY_PRODUCT_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_CATEGORY_PRODUCT CATEGORY_PRODUCT)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CATEGORY_PRODUCT CATEGORY_PRODUCT_edit = Db_gsj.O_CATEGORY_PRODUCT.Single(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT.CATEGORY_PRODUCT_CD);
                CATEGORY_PRODUCT_edit.CATEGORY_PRODUCT_CODE = CATEGORY_PRODUCT.CATEGORY_PRODUCT_CODE;
                CATEGORY_PRODUCT_edit.CATEGORY_PRODUCT_NAME = CATEGORY_PRODUCT.CATEGORY_PRODUCT_NAME;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_PRODUCT);
            }
        }


        public ActionResult Deactive([Bind(Include = "CATEGORY_PRODUCT_CD")]O_CATEGORY_PRODUCT CATEGORY_PRODUCT)
        {
            if (CATEGORY_PRODUCT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_PRODUCT = Db_gsj.O_CATEGORY_PRODUCT.Single(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT.CATEGORY_PRODUCT_CD);
            CATEGORY_PRODUCT.ACTIVE = false;
            Db_gsj.Entry(CATEGORY_PRODUCT).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CATEGORY_PRODUCT_CD")]O_CATEGORY_PRODUCT CATEGORY_PRODUCT)
        {
            if (CATEGORY_PRODUCT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_PRODUCT = Db_gsj.O_CATEGORY_PRODUCT.Single(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT.CATEGORY_PRODUCT_CD);
            CATEGORY_PRODUCT.ACTIVE = true;
            Db_gsj.Entry(CATEGORY_PRODUCT).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}