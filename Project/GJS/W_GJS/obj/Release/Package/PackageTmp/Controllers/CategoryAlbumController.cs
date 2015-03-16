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
    public class CategoryAlbumController : Controller
    {
        //
        public GJSEntities Db_gsj;
        // GET: /CategoryNews/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_CATEGORY_ALBUM.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(O_CATEGORY_ALBUM CATEGORY_ALBUM)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                CATEGORY_ALBUM.ACTIVE = true;
                CATEGORY_ALBUM.STATUS = 0;
                CATEGORY_ALBUM.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(CATEGORY_ALBUM).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_ALBUM);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? CATEGORY_ALBUM_CD)
        {
            if (CATEGORY_ALBUM_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_CATEGORY_ALBUM CATEGORY_ALBUM_edit = Db_gsj.O_CATEGORY_ALBUM.Single(t => t.CATEGORY_ALBUM_CD == CATEGORY_ALBUM_CD);
            if (CATEGORY_ALBUM_edit == null)
                return HttpNotFound();
            return View(CATEGORY_ALBUM_edit);

        }

        [HttpPost]
        public ActionResult Edit(O_CATEGORY_ALBUM CATEGORY_ALBUM)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_CATEGORY_ALBUM CATEGORY_ALBUM_edit = Db_gsj.O_CATEGORY_ALBUM.Single(t => t.CATEGORY_ALBUM_CD == CATEGORY_ALBUM.CATEGORY_ALBUM_CD);
                CATEGORY_ALBUM_edit.CATEGORY_ALBUM_NAME = CATEGORY_ALBUM.CATEGORY_ALBUM_NAME;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(CATEGORY_ALBUM);
            }
        }


        public ActionResult Deactive([Bind(Include = "CATEGORY_ALBUM_CD")]O_CATEGORY_ALBUM CATEGORY_ALBUM)
        {
            if (CATEGORY_ALBUM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_ALBUM = Db_gsj.O_CATEGORY_ALBUM.Single(t => t.CATEGORY_ALBUM_CD == CATEGORY_ALBUM.CATEGORY_ALBUM_CD);
            CATEGORY_ALBUM.ACTIVE = false;
            Db_gsj.Entry(CATEGORY_ALBUM).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "CATEGORY_ALBUM_CD")]O_CATEGORY_ALBUM CATEGORY_ALBUM)
        {
            if (CATEGORY_ALBUM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            CATEGORY_ALBUM = Db_gsj.O_CATEGORY_ALBUM.Single(t => t.CATEGORY_ALBUM_CD == CATEGORY_ALBUM.CATEGORY_ALBUM_CD);
            CATEGORY_ALBUM.ACTIVE = true;
            Db_gsj.Entry(CATEGORY_ALBUM).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}