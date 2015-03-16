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
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CUSTOMER.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_CUSTOMER CUSTOMER)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CUSTOMER.ACTIVE = true;
                CUSTOMER.STATUS = 0;
                CUSTOMER.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CUSTOMER).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CUSTOMER);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CUSTOMER_CD)
        {
            if (CUSTOMER_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CUSTOMER CUSTOMER_edit = Db_gsj.O_CUSTOMER.Single(t => t.CUSTOMER_CD == CUSTOMER_CD);
            if (CUSTOMER_edit == null)
                return HttpNotFound();
            return View(CUSTOMER_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_CUSTOMER CUSTOMER)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CUSTOMER CUSTOMER_edit = Db_gsj.O_CUSTOMER.Single(t => t.CUSTOMER_CD == CUSTOMER.CUSTOMER_CD);
                CUSTOMER_edit.CUSTOMER_CODE = CUSTOMER.CUSTOMER_CODE;
                CUSTOMER_edit.CUSTOMER_NAME = CUSTOMER.CUSTOMER_NAME;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CUSTOMER);
            }
        }


        public ActionResult Deactive([Bind(Include = "CUSTOMER_CD")]O_CUSTOMER CUSTOMER)
        {
            if (CUSTOMER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CUSTOMER = Db_gsj.O_CUSTOMER.Single(t => t.CUSTOMER_CD == CUSTOMER.CUSTOMER_CD);
            CUSTOMER.ACTIVE = false;
            Db_gsj.Entry(CUSTOMER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CUSTOMER_CD")]O_CUSTOMER CUSTOMER)
        {
            if (CUSTOMER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CUSTOMER = Db_gsj.O_CUSTOMER.Single(t => t.CUSTOMER_CD == CUSTOMER.CUSTOMER_CD);
            CUSTOMER.ACTIVE = true;
            Db_gsj.Entry(CUSTOMER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
        //public JsonResult CheckPstAjax(long? CUSTOMER_CD, String CHECK)
        //{
        //    Db_gsj = new GJSEntities();
        //    if (CHECK == "true")
        //    {
        //        O_USER_CUSTOMER User = Db_gsj.O_USER_CUSTOMER.Single(t => t.CUSTOMER_CD == CUSTOMER_CD);
        //        O_USER_PST Pst = Db_gsj.O_USER_PST.Single(t => t.USER_CD == User.USER_CD);
        //        S_USER user_edit = Db_gsj.S_USER.Single(t => t.USER_CD == User.USER_CD);
        //        Pst.PST_CD = 2;
        //        user_edit.STATUS = 2;
        //        Db_gsj.SaveChanges();
        //    }
        //    else
        //    {
        //        O_USER_CUSTOMER User = Db_gsj.O_USER_CUSTOMER.Single(t => t.CUSTOMER_CD == CUSTOMER_CD);
        //        O_USER_PST Pst = Db_gsj.O_USER_PST.Single(t => t.USER_CD == User.USER_CD);
        //        S_USER user_edit = Db_gsj.S_USER.Single(t => t.USER_CD == User.USER_CD);
        //        Pst.PST_CD = 3;
        //        user_edit.STATUS = 3;
        //        Db_gsj.SaveChanges();
        //    }
        //    return Json(1);
        //}

        public JsonResult ChangePstAjax(long? CUSTOMER_CD, long? change)
        {
            Db_gsj = new GJSEntities();
            O_USER_CUSTOMER User = Db_gsj.O_USER_CUSTOMER.Single(t => t.CUSTOMER_CD == CUSTOMER_CD);
            O_USER_PST Pst = Db_gsj.O_USER_PST.Single(t => t.USER_CD == User.USER_CD);
            S_USER user_edit = Db_gsj.S_USER.Single(t => t.USER_CD == User.USER_CD);
            Pst.PST_CD = change;
            user_edit.STATUS = change;
            Db_gsj.SaveChanges();
           
            return Json(1);
        }
    }
}