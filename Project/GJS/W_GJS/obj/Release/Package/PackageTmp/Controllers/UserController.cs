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
    public class UserController : Controller
    {
        //
        // GET: /User/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {

            Db_gsj = new GJSEntities();
            return View(Db_gsj.S_USER.Where(t=>t.STATUS != 1).ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();

            var queryD = Db_gsj.S_PST.Where(t=>t.PST_CD != 1).ToList();
            ViewBag.pst = new SelectList(queryD.AsEnumerable(), "PST_CD", "PST_NAME", 2);
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(S_USER USER)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();

                USER.ACTIVE = true;
                USER.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(USER).State = EntityState.Added;
                Db_gsj.SaveChanges();

                O_USER_PST USER_PST = new O_USER_PST();
                USER_PST.ACTIVE = true;
                USER_PST.STATUS = 0;
                USER_PST.CREATEDATE = DateTime.Now;
                USER_PST.PST_CD = USER.STATUS;
                USER_PST.USER_CD = USER.USER_CD;
                Db_gsj.Entry(USER_PST).State = EntityState.Added;
                Db_gsj.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var queryD = Db_gsj.S_PST.Where(t => t.PST_CD != 1).ToList();
                ViewBag.pst = new SelectList(queryD.AsEnumerable(), "PST_CD", "PST_NAME", USER.STATUS);
                return View(USER);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? USER_CD)
        {
            if (USER_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
           
            S_USER USER_edit = Db_gsj.S_USER.Single(t => t.USER_CD == USER_CD);
            var queryD = Db_gsj.S_PST.Where(t => t.PST_CD != 1).ToList();
            ViewBag.pst = new SelectList(queryD.AsEnumerable(), "PST_CD", "PST_NAME", USER_edit.O_USER_PST.ToList()[0].PST_CD);
            if (USER_edit == null)
                return HttpNotFound();
            return View(USER_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(S_USER USER)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                S_USER USER_edit = Db_gsj.S_USER.Single(t => t.USER_CD == USER.USER_CD);
                USER_edit.STATUS = USER.STATUS;
                Db_gsj.SaveChanges();

             
                O_USER_PST USER_PST = Db_gsj.O_USER_PST.Single(t => t.USER_CD == USER.USER_CD );
                USER_PST.ACTIVE = true;
                USER_PST.STATUS = 0;
                USER_PST.CREATEDATE = DateTime.Now;
                USER_PST.PST_CD = USER.STATUS;
                Db_gsj.Entry(USER_PST).State = EntityState.Modified;
                Db_gsj.SaveChanges();


                return RedirectToAction("Index");

            }
            else
            {
                Db_gsj = new GJSEntities();
                var queryD = Db_gsj.S_PST.Where(t => t.PST_CD != 1).ToList();
                ViewBag.pst = new SelectList(queryD.AsEnumerable(), "PST_CD", "PST_NAME", USER.STATUS);
          
                return View(USER);
            }
        }


        public ActionResult Deactive([Bind(Include = "USER_CD")]S_USER USER)
        {
            if (USER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            USER = Db_gsj.S_USER.Single(t => t.USER_CD == USER.USER_CD);
            USER.ACTIVE = false;
            Db_gsj.Entry(USER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "USER_CD")]S_USER USER)
        {
            if (USER == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            USER = Db_gsj.S_USER.Single(t => t.USER_CD == USER.USER_CD);
            USER.ACTIVE = true;
            Db_gsj.Entry(USER).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}