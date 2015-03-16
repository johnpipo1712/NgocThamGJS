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
    public class AdvertisementController : Controller
    {
        //
        public GJSEntities Db_gsj;
        // GET: /CategoryPage/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_ADVERTISEMENT.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)] 
        public ActionResult Create(O_ADVERTISEMENT ADVERTISEMENT)
        {
            
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                ADVERTISEMENT.ACTIVE = true;
                ADVERTISEMENT.STATUS = 0;
                ADVERTISEMENT.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(ADVERTISEMENT).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(ADVERTISEMENT);
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
            O_ADVERTISEMENT ADVERTISEMENT_edit = Db_gsj.O_ADVERTISEMENT.Single(t => t.CD == CD);
            if (ADVERTISEMENT_edit == null)
                return HttpNotFound();
            return View(ADVERTISEMENT_edit);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(O_ADVERTISEMENT ADVERTISEMENT_CONTENT)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_ADVERTISEMENT ADVERTISEMENT_edit = Db_gsj.O_ADVERTISEMENT.Single(t => t.CD == ADVERTISEMENT_CONTENT.CD);
                ADVERTISEMENT_edit.ADVERTISEMENT = ADVERTISEMENT_CONTENT.ADVERTISEMENT;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(ADVERTISEMENT_CONTENT);
            }
        }


        public ActionResult Deactive([Bind(Include = "CD")]O_ADVERTISEMENT ADVERTISEMENT)
        {
            if (ADVERTISEMENT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            ADVERTISEMENT = Db_gsj.O_ADVERTISEMENT.Single(t => t.CD == ADVERTISEMENT.CD);
            ADVERTISEMENT.ACTIVE = false;
            Db_gsj.Entry(ADVERTISEMENT).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CD")]O_ADVERTISEMENT ADVERTISEMENT)
        {
            if (ADVERTISEMENT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            ADVERTISEMENT = Db_gsj.O_ADVERTISEMENT.Single(t => t.CD == ADVERTISEMENT.CD);
            ADVERTISEMENT.ACTIVE = true;
            Db_gsj.Entry(ADVERTISEMENT).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}