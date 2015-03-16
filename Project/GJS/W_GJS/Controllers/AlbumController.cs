using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using W_GJS.General;
using W_GJS.Models;
namespace W_GJS.Controllers
{
    public class AlbumController : Controller
    {
        //
        // GET: /Album/
        public GJSEntities Db_gsj;
        // GET: /CategoryNews/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_ALBUM.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();
            var query = Db_gsj.O_CATEGORY_ALBUM.ToList();
            ViewBag.category = new SelectList(query.AsEnumerable(), "CATEGORY_ALBUM_CD", "CATEGORY_ALBUM_NAME", 1);
          
            return View();
        }

        [HttpPost]
        public ActionResult Create(O_ALBUM ALBUM)
        {

            if (ModelState.IsValid)
            {
                bool checkImage = false;
                checkImage = Process.CheckExtensionImg(ALBUM.URL_IMAGE);
                if (checkImage == false)
                {
                    Db_gsj = new GJSEntities();
                    ALBUM.ACTIVE = true;
                    ALBUM.STATUS = 0;
                    ALBUM.CREATEDATE = DateTime.Now;
                    Db_gsj.Entry(ALBUM).State = EntityState.Added;
                    Db_gsj.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Db_gsj = new GJSEntities();
                    ModelState.AddModelError("", "Vui lòng kiểm tra đường dẫn hình ảnh");
                    var query = Db_gsj.O_CATEGORY_ALBUM.ToList();
                    ViewBag.category = new SelectList(query.AsEnumerable(), "CATEGORY_ALBUM_CD", "CATEGORY_ALBUM_NAME", ALBUM.CATEGORY_ALBUM_CD);
 
                    return View(ALBUM);
                }

            }
            else
            {
                Db_gsj = new GJSEntities();
                var query = Db_gsj.O_CATEGORY_ALBUM.ToList();
                 
                ViewBag.category = new SelectList(query.AsEnumerable(), "CATEGORY_ALBUM_CD", "CATEGORY_ALBUM_NAME", ALBUM.CATEGORY_ALBUM_CD);
                return View(ALBUM);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? ALBUM_CD)
        {
            if (ALBUM_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_ALBUM ALBUM_edit = Db_gsj.O_ALBUM.Single(t => t.ALBUM_CD == ALBUM_CD);
            Db_gsj = new GJSEntities();
            var query = Db_gsj.O_CATEGORY_ALBUM.ToList();
            ViewBag.category = new SelectList(query.AsEnumerable(), "CATEGORY_ALBUM_CD", "CATEGORY_ALBUM_NAME", ALBUM_edit.CATEGORY_ALBUM_CD);
          
            if (ALBUM_edit == null)
                return HttpNotFound();
            return View(ALBUM_edit);

        }

        [HttpPost]
        public ActionResult Edit(O_ALBUM ALBUM)
        {
            if (ModelState.IsValid)
            {
                bool checkImage = false;
                checkImage = Process.CheckExtensionImg(ALBUM.URL_IMAGE);
                if (checkImage == false)
                {
                    Db_gsj = new GJSEntities();
                    O_ALBUM ALBUM_edit = Db_gsj.O_ALBUM.Single(t => t.ALBUM_CD == ALBUM.ALBUM_CD);
                    ALBUM_edit.URL_IMAGE = ALBUM.URL_IMAGE;
                    ALBUM_edit.CATEGORY_ALBUM_CD = ALBUM.CATEGORY_ALBUM_CD;
                    Db_gsj.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra đường dẫn hình ảnh");
                    var query = Db_gsj.O_CATEGORY_ALBUM.ToList();
                    ViewBag.category = new SelectList(query.AsEnumerable(), "CATEGORY_ALBUM_CD", "CATEGORY_ALBUM_NAME", ALBUM.CATEGORY_ALBUM_CD);
                    return View(ALBUM);
                }

            }
            else
            {
                var query = Db_gsj.O_CATEGORY_ALBUM.ToList();
                ViewBag.category = new SelectList(query.AsEnumerable(), "CATEGORY_ALBUM_CD", "CATEGORY_ALBUM_NAME", ALBUM.CATEGORY_ALBUM_CD);
                return View(ALBUM);
            }
        }


        public ActionResult Deactive([Bind(Include = "ALBUM_CD")]O_ALBUM ALBUM)
        {
            if (ALBUM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            ALBUM = Db_gsj.O_ALBUM.Single(t => t.ALBUM_CD == ALBUM.ALBUM_CD);
            ALBUM.ACTIVE = false;
            Db_gsj.Entry(ALBUM).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "ALBUM_CD")]O_ALBUM ALBUM)
        {
            if (ALBUM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            ALBUM = Db_gsj.O_ALBUM.Single(t => t.ALBUM_CD == ALBUM.ALBUM_CD);
            ALBUM.ACTIVE = true;
            Db_gsj.Entry(ALBUM).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}