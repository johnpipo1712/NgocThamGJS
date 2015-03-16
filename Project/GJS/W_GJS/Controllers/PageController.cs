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
    public class PageController : Controller
    {
        //
        // GET: /PAGE/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.M_PAGE.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();
            var queryD = Db_gsj.O_CATEGORY_PAGE.ToList();
            ViewBag.pageCategory = new SelectList(queryD.AsEnumerable(), "CATEGORY_PAGE_CD", "CATEGORY_PAGE_NAME", 1);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
   
        public ActionResult Create(M_PAGE PAGE)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                PAGE.ACTIVE = true;
                PAGE.STATUS = 0;
                PAGE.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(PAGE).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                 var queryD = Db_gsj.O_CATEGORY_PAGE.ToList();
                ViewBag.pageCategory = new SelectList(queryD.AsEnumerable(), "CATEGORY_PAGE_CD", "CATEGORY_PAGE_NAME", PAGE.CATEGORY_PAGE_CD);
                return View(PAGE);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? PAGE_COTRACT_CD)
        {
            if (PAGE_COTRACT_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();

          
            M_PAGE PAGE_edit = Db_gsj.M_PAGE.Single(t => t.PAGE_COTRACT_CD == PAGE_COTRACT_CD);
            //var queryD = Db_gsj.O_CATEGORY_PAGE.ToList();
            //ViewBag.pageCategory = new SelectList(queryD.AsEnumerable(), "CATEGORY_PAGE_CD", "CATEGORY_PAGE_NAME", PAGE_edit.CATEGORY_PAGE_CD);

            if (PAGE_edit == null)
                return HttpNotFound();
            return View(PAGE_edit);

        }

        [HttpPost]
        [ValidateInput(false)]
   
        public ActionResult Edit(M_PAGE PAGE)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                M_PAGE PAGE_edit = Db_gsj.M_PAGE.Single(t => t.PAGE_COTRACT_CD == PAGE.PAGE_COTRACT_CD);
                PAGE_edit.PAGE_COTRACT_CONTENT = PAGE.PAGE_COTRACT_CONTENT;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                //Db_gsj = new GJSEntities();
              
                //var queryD = Db_gsj.O_CATEGORY_PAGE.ToList();
                //ViewBag.pageCategory = new SelectList(queryD.AsEnumerable(), "CATEGORY_PAGE_CD", "CATEGORY_PAGE_NAME", PAGE.CATEGORY_PAGE_CD);
       
                return View(PAGE);
            }
        }


        public ActionResult Deactive([Bind(Include = "PAGE_COTRACT_CD")]M_PAGE PAGE)
        {
            if (PAGE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PAGE = Db_gsj.M_PAGE.Single(t => t.PAGE_COTRACT_CD == PAGE.PAGE_COTRACT_CD);
            PAGE.ACTIVE = false;
            Db_gsj.Entry(PAGE).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "PAGE_COTRACT_CD")]M_PAGE PAGE)
        {
            if (PAGE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PAGE = Db_gsj.M_PAGE.Single(t => t.PAGE_COTRACT_CD == PAGE.PAGE_COTRACT_CD);
            PAGE.ACTIVE = true;
            Db_gsj.Entry(PAGE).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}