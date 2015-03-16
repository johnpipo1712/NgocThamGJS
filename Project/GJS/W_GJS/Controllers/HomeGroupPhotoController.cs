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
    public class HomeGroupPhotoController : Controller
    {
        //
        // GET: /HomeGroupPhoto/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_HOME_GROUP_PHOTO.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(O_HOME_GROUP_PHOTO HOME_GROUP_PHOTO)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                HOME_GROUP_PHOTO.ACTIVE = true;
                HOME_GROUP_PHOTO.STATUS = 0;
                HOME_GROUP_PHOTO.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(HOME_GROUP_PHOTO).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(HOME_GROUP_PHOTO);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? HOME_GROUP_PHOTO_CD)
        {
            if (HOME_GROUP_PHOTO_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_HOME_GROUP_PHOTO HOME_GROUP_PHOTO_edit = Db_gsj.O_HOME_GROUP_PHOTO.Single(t => t.HOME_GROUP_PHOTO_CD == HOME_GROUP_PHOTO_CD);
            if (HOME_GROUP_PHOTO_edit == null)
                return HttpNotFound();
            return View(HOME_GROUP_PHOTO_edit);

        }

        [HttpPost]

        public ActionResult Edit(O_HOME_GROUP_PHOTO HOME_GROUP_PHOTO)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_HOME_GROUP_PHOTO HOME_GROUP_PHOTO_edit = Db_gsj.O_HOME_GROUP_PHOTO.Single(t => t.HOME_GROUP_PHOTO_CD == HOME_GROUP_PHOTO.HOME_GROUP_PHOTO_CD);
                HOME_GROUP_PHOTO_edit.PHOTO_URL_1 = HOME_GROUP_PHOTO.PHOTO_URL_1;
                HOME_GROUP_PHOTO_edit.PHOTO_URL_2 = HOME_GROUP_PHOTO.PHOTO_URL_2;
                HOME_GROUP_PHOTO_edit.PHOTO_URL_3 = HOME_GROUP_PHOTO.PHOTO_URL_3;
                HOME_GROUP_PHOTO_edit.PHOTO_URL_4 = HOME_GROUP_PHOTO.PHOTO_URL_4;
                HOME_GROUP_PHOTO_edit.PHOTO_URL_5 = HOME_GROUP_PHOTO.PHOTO_URL_5;
                HOME_GROUP_PHOTO_edit.PHOTO_URL_6 = HOME_GROUP_PHOTO.PHOTO_URL_6;
            
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(HOME_GROUP_PHOTO);
            }
        }


        public ActionResult Deactive([Bind(Include = "HOME_GROUP_PHOTO_CD")]O_HOME_GROUP_PHOTO HOME_GROUP_PHOTO)
        {
            if (HOME_GROUP_PHOTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            HOME_GROUP_PHOTO = Db_gsj.O_HOME_GROUP_PHOTO.Single(t => t.HOME_GROUP_PHOTO_CD == HOME_GROUP_PHOTO.HOME_GROUP_PHOTO_CD);
            HOME_GROUP_PHOTO.ACTIVE = false;
            Db_gsj.Entry(HOME_GROUP_PHOTO).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "HOME_GROUP_PHOTO_CD")]O_HOME_GROUP_PHOTO HOME_GROUP_PHOTO)
        {
            if (HOME_GROUP_PHOTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            HOME_GROUP_PHOTO = Db_gsj.O_HOME_GROUP_PHOTO.Single(t => t.HOME_GROUP_PHOTO_CD == HOME_GROUP_PHOTO.HOME_GROUP_PHOTO_CD);
            HOME_GROUP_PHOTO.ACTIVE = true;
            Db_gsj.Entry(HOME_GROUP_PHOTO).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}