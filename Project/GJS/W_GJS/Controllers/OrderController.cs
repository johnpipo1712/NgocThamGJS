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
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_ORDER.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_ORDER ORDER)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                ORDER.ACTIVE = true;
                ORDER.STATUS = 0;
                ORDER.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(ORDER).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(ORDER);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? ORDER_CD)
        {
            if (ORDER_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_ORDER ORDER_edit = Db_gsj.O_ORDER.Single(t => t.ORDER_CD == ORDER_CD);
            if (ORDER_edit == null)
                return HttpNotFound();
            return View(ORDER_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_ORDER ORDER)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_ORDER ORDER_edit = Db_gsj.O_ORDER.Single(t => t.ORDER_CD == ORDER.ORDER_CD);
                ORDER_edit.ORDER_CODE = ORDER.ORDER_CODE;
                ORDER_edit.EMPLOYEE_CD = ORDER.EMPLOYEE_CD;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(ORDER);
            }
        }


        public ActionResult Deactive([Bind(Include = "ORDER_CD")]O_ORDER ORDER)
        {
            if (ORDER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            ORDER = Db_gsj.O_ORDER.Single(t => t.ORDER_CD == ORDER.ORDER_CD);
            ORDER.ACTIVE = false;
            Db_gsj.Entry(ORDER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "ORDER_CD")]O_ORDER ORDER)
        {
            if (ORDER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            ORDER = Db_gsj.O_ORDER.Single(t => t.ORDER_CD == ORDER.ORDER_CD);
            ORDER.ACTIVE = true;
            Db_gsj.Entry(ORDER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detail([Bind(Include = "ORDER_CD")]O_ORDER ORDER)
        {
            Db_gsj = new GJSEntities();
            ViewBag.Total = O_ORDER.Price_Total(Db_gsj.O_ORDER.Single(t => t.ORDER_CD == ORDER.ORDER_CD));
            return View(Db_gsj.D_ORDER_DETAIL.Where(t=>t.ORDER_CD == ORDER.ORDER_CD).ToList());
        }
    }
}