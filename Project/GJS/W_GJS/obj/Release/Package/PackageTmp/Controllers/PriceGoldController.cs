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
    public class PriceGoldController : Controller
    {
        //
        public GJSEntities Db_gsj;
        // GET: /CategoryNews/
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_PRICE_GOLD.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(O_PRICE_GOLD PRICE_GOLD)
        {
            
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                PRICE_GOLD.ACTIVE = true;
                PRICE_GOLD.STATUS = 0;
                PRICE_GOLD.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(PRICE_GOLD).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(PRICE_GOLD);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? PRICE_GOLD_CD)
        {
            if (PRICE_GOLD_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_PRICE_GOLD PRICE_GOLD_edit = Db_gsj.O_PRICE_GOLD.Single(t => t.PRICE_GOLD_CD == PRICE_GOLD_CD);
            if (PRICE_GOLD_edit == null)
                return HttpNotFound();
            return View(PRICE_GOLD_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(O_PRICE_GOLD PRICE_GOLD)
        {
            if ((int)Session[W_GJS.General.SessionConstants.LOGINED_ADMIN_ROLE_KEY] == 4)
            {
                PRICE_GOLD.GOLD_CODE = "";
                PRICE_GOLD.GOLD_NAME = "";
            }
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                O_PRICE_GOLD PRICE_GOLD_edit = Db_gsj.O_PRICE_GOLD.Single(t => t.PRICE_GOLD_CD == PRICE_GOLD.PRICE_GOLD_CD);
                PRICE_GOLD_edit.PRICE_GOLD_CD = PRICE_GOLD.PRICE_GOLD_CD;
                if ((int)Session[W_GJS.General.SessionConstants.LOGINED_ADMIN_ROLE_KEY] != 4)
                {
                    PRICE_GOLD_edit.GOLD_CODE = PRICE_GOLD.GOLD_CODE;
                    PRICE_GOLD_edit.GOLD_NAME = PRICE_GOLD.GOLD_NAME;
                }
                PRICE_GOLD_edit.PRICE_SALES = PRICE_GOLD.PRICE_SALES;
                PRICE_GOLD_edit.PRICE_BUY = PRICE_GOLD.PRICE_BUY;
                
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(PRICE_GOLD);
            }
        }


        public ActionResult Deactive([Bind(Include = "PRICE_GOLD_CD")]O_PRICE_GOLD PRICE_GOLD)
        {
            if (PRICE_GOLD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PRICE_GOLD = Db_gsj.O_PRICE_GOLD.Single(t => t.PRICE_GOLD_CD == PRICE_GOLD.PRICE_GOLD_CD);
            PRICE_GOLD.ACTIVE = false;
            Db_gsj.Entry(PRICE_GOLD).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "PRICE_GOLD_CD")]O_PRICE_GOLD PRICE_GOLD)
        {
            if (PRICE_GOLD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PRICE_GOLD = Db_gsj.O_PRICE_GOLD.Single(t => t.PRICE_GOLD_CD == PRICE_GOLD.PRICE_GOLD_CD);
            PRICE_GOLD.ACTIVE = true;
            Db_gsj.Entry(PRICE_GOLD).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}