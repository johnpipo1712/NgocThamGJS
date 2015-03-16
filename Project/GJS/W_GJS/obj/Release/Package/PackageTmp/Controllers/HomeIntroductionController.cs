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
    public class HomeIntroductionController : Controller
    {
        //
        // GET: /HomeIntroduction/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_HOME_INTRODUCTION.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(O_HOME_INTRODUCTION HOME_INTRODUCTION)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                HOME_INTRODUCTION.ACTIVE = true;
                HOME_INTRODUCTION.STATUS = 0;
                HOME_INTRODUCTION.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(HOME_INTRODUCTION).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(HOME_INTRODUCTION);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? O_HOME_INTRODUCTION_CD)
        {
            if (O_HOME_INTRODUCTION_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_HOME_INTRODUCTION HOME_INTRODUCTION_edit = Db_gsj.O_HOME_INTRODUCTION.Single(t => t.O_HOME_INTRODUCTION_CD == O_HOME_INTRODUCTION_CD);
            if (HOME_INTRODUCTION_edit == null)
                return HttpNotFound();
            return View(HOME_INTRODUCTION_edit);

        }

        [HttpPost]

        public ActionResult Edit(O_HOME_INTRODUCTION HOME_INTRODUCTION)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_HOME_INTRODUCTION HOME_INTRODUCTION_edit = Db_gsj.O_HOME_INTRODUCTION.Single(t => t.O_HOME_INTRODUCTION_CD == HOME_INTRODUCTION.O_HOME_INTRODUCTION_CD);
                HOME_INTRODUCTION_edit.O_HOME_INTRODUCTION_CONTENT = HOME_INTRODUCTION.O_HOME_INTRODUCTION_CONTENT;
                HOME_INTRODUCTION_edit.O_HOME_INTRODUCTION_AUTHOR = HOME_INTRODUCTION.O_HOME_INTRODUCTION_AUTHOR;
                HOME_INTRODUCTION_edit.O_HOME_INTRODUCTION_PHOTO = HOME_INTRODUCTION.O_HOME_INTRODUCTION_PHOTO;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(HOME_INTRODUCTION);
            }
        }


        public ActionResult Deactive([Bind(Include = "O_HOME_INTRODUCTION_CD")]O_HOME_INTRODUCTION HOME_INTRODUCTION)
        {
            if (HOME_INTRODUCTION == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            HOME_INTRODUCTION = Db_gsj.O_HOME_INTRODUCTION.Single(t => t.O_HOME_INTRODUCTION_CD == HOME_INTRODUCTION.O_HOME_INTRODUCTION_CD);
            HOME_INTRODUCTION.ACTIVE = false;
            Db_gsj.Entry(HOME_INTRODUCTION).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "O_HOME_INTRODUCTION_CD")]O_HOME_INTRODUCTION HOME_INTRODUCTION)
        {
            if (HOME_INTRODUCTION == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            HOME_INTRODUCTION = Db_gsj.O_HOME_INTRODUCTION.Single(t => t.O_HOME_INTRODUCTION_CD == HOME_INTRODUCTION.O_HOME_INTRODUCTION_CD);
            HOME_INTRODUCTION.ACTIVE = true;
            Db_gsj.Entry(HOME_INTRODUCTION).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}