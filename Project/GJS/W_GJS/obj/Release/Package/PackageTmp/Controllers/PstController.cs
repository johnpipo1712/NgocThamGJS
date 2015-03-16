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
    public class PstController : Controller
    {
        //
        // GET: /Pst/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.S_PST);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(S_PST PST)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                PST.ACTIVE = true;
                PST.STATUS = 0;
                PST.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(PST).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(PST);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? PST_CD)
        {
            if (PST_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            S_PST PST_edit = Db_gsj.S_PST.Single(t => t.PST_CD == PST_CD);
            if (PST_edit == null)
                return HttpNotFound();
            return View(PST_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(S_PST PST)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                S_PST PST_edit = Db_gsj.S_PST.Single(t => t.PST_CD == PST.PST_CD);
                PST_edit.PST_CODE = PST.PST_CODE;
                PST_edit.PST_NAME = PST.PST_NAME;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(PST);
            }
        }


        public ActionResult Deactive([Bind(Include = "PST_CD")]S_PST PST)
        {
            if (PST == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PST = Db_gsj.S_PST.Single(t => t.PST_CD == PST.PST_CD);
            PST.ACTIVE = false;
            Db_gsj.Entry(PST).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "PST_CD")]S_PST PST)
        {
            if (PST == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PST = Db_gsj.S_PST.Single(t => t.PST_CD == PST.PST_CD);
            PST.ACTIVE = true;
            Db_gsj.Entry(PST).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}