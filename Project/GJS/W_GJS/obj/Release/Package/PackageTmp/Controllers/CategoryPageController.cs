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
    public class CategoryPageController : Controller
    {
        //
        public GJSEntities Db_gsj;
        // GET: /CategoryPage/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CATEGORY_PAGE.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_CATEGORY_PAGE CATEGORY_PAGE)
        {
            
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CATEGORY_PAGE.ACTIVE = true;
                CATEGORY_PAGE.STATUS = 0;
                CATEGORY_PAGE.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CATEGORY_PAGE).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_PAGE);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CATEGORY_PAGE_CD)
        {
            if (CATEGORY_PAGE_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CATEGORY_PAGE CATEGORY_PAGE_edit = Db_gsj.O_CATEGORY_PAGE.Single(t => t.CATEGORY_PAGE_CD == CATEGORY_PAGE_CD);
            if (CATEGORY_PAGE_edit == null)
                return HttpNotFound();
            return View(CATEGORY_PAGE_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_CATEGORY_PAGE CATEGORY_PAGE)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CATEGORY_PAGE CATEGORY_PAGE_edit = Db_gsj.O_CATEGORY_PAGE.Single(t => t.CATEGORY_PAGE_CD == CATEGORY_PAGE.CATEGORY_PAGE_CD);
                CATEGORY_PAGE_edit.CATEGORY_PAGE_CODE = CATEGORY_PAGE.CATEGORY_PAGE_CODE;
                CATEGORY_PAGE_edit.CATEGORY_PAGE_NAME = CATEGORY_PAGE.CATEGORY_PAGE_NAME;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_PAGE);
            }
        }


        public ActionResult Deactive([Bind(Include = "CATEGORY_PAGE_CD")]O_CATEGORY_PAGE CATEGORY_PAGE)
        {
            if (CATEGORY_PAGE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_PAGE = Db_gsj.O_CATEGORY_PAGE.Single(t => t.CATEGORY_PAGE_CD == CATEGORY_PAGE.CATEGORY_PAGE_CD);
            CATEGORY_PAGE.ACTIVE = false;
            Db_gsj.Entry(CATEGORY_PAGE).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CATEGORY_PAGE_CD")]O_CATEGORY_PAGE CATEGORY_PAGE)
        {
            if (CATEGORY_PAGE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_PAGE = Db_gsj.O_CATEGORY_PAGE.Single(t => t.CATEGORY_PAGE_CD == CATEGORY_PAGE.CATEGORY_PAGE_CD);
            CATEGORY_PAGE.ACTIVE = true;
            Db_gsj.Entry(CATEGORY_PAGE).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}