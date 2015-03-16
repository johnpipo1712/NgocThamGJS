using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using W_GJS.Models;
using W_GJS.General;
namespace W_GJS.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_PRODUCT.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Db_gsj = new GJSEntities();
            var query = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.ToList();
            ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_DETAIL_CD", "CATEGORY_PRODUCT_DETAIL_NAME", 1);
            var queryG = Db_gsj.O_CATEGORY_GRANULES.ToList();
            ViewBag.DropdownlistG = new SelectList(queryG.AsEnumerable(), "CATEGORY_GRANULES_CD", "CATEGORY_GRANULES_NAME", 1);
      
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
   
        public ActionResult Create(O_PRODUCT PRODUCT
            , String file1
            , String file2
            , String file3
            , String file4
            , String file5)
        {

            if (ModelState.IsValid)
            {
                bool checkImage = false;
                checkImage = Process.CheckExtensionImg(PRODUCT.URL_IMAGE);
                if (file1 != "")
                {
                    checkImage = Process.CheckExtensionImg(file1);
                }
                if (file2 != "")
                {
                    checkImage = Process.CheckExtensionImg(file2);
                }
                if (file3 != "")
                {
                    checkImage = Process.CheckExtensionImg(file3);
                }
                if (file4 != "")
                {
                    checkImage = Process.CheckExtensionImg(file4);

                }
                if (file5 != "")
                {
                    checkImage = Process.CheckExtensionImg(file5);
                }
                if (checkImage == false)
                {
                    Db_gsj = new GJSEntities();
                    PRODUCT.ACTIVE = true;
                    PRODUCT.STATUS = 0;
                    PRODUCT.CREATEDATE = DateTime.Now;
                    PRODUCT.O_CATEGORY_PRODUCT = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == PRODUCT.CATEGORY_PRODUCT_DETAIL_CD).O_CATEGORY_PRODUCT;
                    PRODUCT.CATEGORY_PRODUCT_CD = PRODUCT.CATEGORY_PRODUCT_CD;
                    Db_gsj.Entry(PRODUCT).State = EntityState.Added;
                    Db_gsj.SaveChanges();
                    if (file1 != "")
                    {
                        D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                        dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                        dproudct.STATUS = 0;
                        dproudct.ACTIVE = true;
                        dproudct.CREATEDATE = DateTime.Now;
                        dproudct.URL_IMAGE = file1;
                        Db_gsj.Entry(dproudct).State = EntityState.Added;
                        Db_gsj.SaveChanges();
                    }
                    if (file2 != "")
                    {
                        D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                        dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                        dproudct.STATUS = 0;
                        dproudct.ACTIVE = true;
                        dproudct.CREATEDATE = DateTime.Now;
                        dproudct.URL_IMAGE = file2;
                        Db_gsj.Entry(dproudct).State = EntityState.Added;
                        Db_gsj.SaveChanges();
                    }
                    if (file3 != "")
                    {
                        D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                        dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                        dproudct.STATUS = 0;
                        dproudct.ACTIVE = true;
                        dproudct.CREATEDATE = DateTime.Now;
                        dproudct.URL_IMAGE = file3;
                        Db_gsj.Entry(dproudct).State = EntityState.Added;
                       
                        Db_gsj.SaveChanges();
                    }
                    if (file4 != "")
                    {
                        D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                        dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                        dproudct.STATUS = 0;
                        dproudct.ACTIVE = true;
                        dproudct.CREATEDATE = DateTime.Now;
                        dproudct.URL_IMAGE = file4;
                        Db_gsj.Entry(dproudct).State = EntityState.Added;
                       
                        Db_gsj.SaveChanges();
                    }
                    if (file5 != "")
                    {
                        D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                        dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                        dproudct.STATUS = 0;
                        dproudct.ACTIVE = true;
                        dproudct.CREATEDATE = DateTime.Now;
                        dproudct.URL_IMAGE = file5;
                        Db_gsj.Entry(dproudct).State = EntityState.Added;
                       
                        Db_gsj.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    Db_gsj = new GJSEntities();
                    ModelState.AddModelError("", "Vui lòng kiểm tra các đường dẫn hình ảnh");
                    var query = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.ToList();
                    ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_DETAIL_CD", "CATEGORY_PRODUCT_DETAIL_NAME", PRODUCT.CATEGORY_PRODUCT_DETAIL_CD);
                    var queryG = Db_gsj.O_CATEGORY_GRANULES.ToList();
                    ViewBag.DropdownlistG = new SelectList(queryG.AsEnumerable(), "CATEGORY_GRANULES_CD", "CATEGORY_GRANULES_NAME", PRODUCT.CATEGORY_GRANULES_CD);
    
                    return View(PRODUCT);
                }
            }
            else
            {
                Db_gsj = new GJSEntities();
                var query = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.ToList();
                ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_DETAIL_CD", "CATEGORY_PRODUCT_DETAIL_NAME", PRODUCT.CATEGORY_PRODUCT_DETAIL_CD);
                var queryG = Db_gsj.O_CATEGORY_GRANULES.ToList();
                ViewBag.DropdownlistG = new SelectList(queryG.AsEnumerable(), "CATEGORY_GRANULES_CD", "CATEGORY_GRANULES_NAME", PRODUCT.CATEGORY_GRANULES_CD);
    
                return View(PRODUCT);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? PRODUCT_CD)
        {
            if (PRODUCT_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            O_PRODUCT PRODUCT_edit = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT_CD);
            var query = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.ToList();
            ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_DETAIL_CD", "CATEGORY_PRODUCT_DETAIL_NAME", PRODUCT_edit.CATEGORY_PRODUCT_DETAIL_CD);
            var queryG = Db_gsj.O_CATEGORY_GRANULES.ToList();
            ViewBag.DropdownlistG = new SelectList(queryG.AsEnumerable(), "CATEGORY_GRANULES_CD", "CATEGORY_GRANULES_NAME", PRODUCT_edit.CATEGORY_GRANULES_CD);
      
            if (PRODUCT_edit == null)
                return HttpNotFound();
            return View(PRODUCT_edit);

        }

        [HttpPost]
        [ValidateInput(false)]
   
        public ActionResult Edit(O_PRODUCT PRODUCT
            , String file1
            , String file2
            , String file3
            , String file4
            , String file5)
        {
            if (ModelState.IsValid)
            {
                 bool checkImage = false;
                checkImage = Process.CheckExtensionImg(PRODUCT.URL_IMAGE);
                if (file1 != "")
                {
                    checkImage = Process.CheckExtensionImg(file1);
                }
                if (file1 != "")
                {
                    checkImage = Process.CheckExtensionImg(file2);
                }
                if (file1 != "")
                {
                    checkImage = Process.CheckExtensionImg(file3);
                }
                if (file1 != "")
                {
                    checkImage = Process.CheckExtensionImg(file4);

                }
                if (file1 != "")
                {
                    checkImage = Process.CheckExtensionImg(file5);
                }
                if (checkImage == false)
                {
                Db_gsj = new GJSEntities();
                O_PRODUCT PRODUCT_edit = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT.PRODUCT_CD);
                PRODUCT_edit.PRODUCT_CODE = PRODUCT.PRODUCT_CODE;
                PRODUCT_edit.PRODUCT_NAME = PRODUCT.PRODUCT_NAME;
                PRODUCT_edit.CATEGORY_GRANULES_CD = PRODUCT.CATEGORY_GRANULES_CD;
                PRODUCT_edit.CATEGORY_PRODUCT_DETAIL_CD = PRODUCT.CATEGORY_PRODUCT_DETAIL_CD;
                PRODUCT_edit.O_CATEGORY_PRODUCT = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == PRODUCT.CATEGORY_PRODUCT_DETAIL_CD).O_CATEGORY_PRODUCT;
                PRODUCT_edit.CATEGORY_PRODUCT_CD = PRODUCT_edit.O_CATEGORY_PRODUCT.CATEGORY_PRODUCT_CD;
                PRODUCT_edit.QUANTITY_GRANULES = PRODUCT.QUANTITY_GRANULES;
                PRODUCT_edit.CURRENCY = PRODUCT.CURRENCY;
                PRODUCT_edit.PRICE_PROMOTION = PRODUCT.PRICE_PROMOTION;
                PRODUCT_edit.PRODUCT_CONTENT = PRODUCT.PRODUCT_CONTENT;
                PRODUCT_edit.PRODUCT_TITLE = PRODUCT.PRODUCT_TITLE;
                PRODUCT_edit.URL_IMAGE = PRODUCT.URL_IMAGE;
                PRODUCT_edit.WAGES = PRODUCT.WAGES;
                PRODUCT_edit.WEIGHT = PRODUCT.WEIGHT;
                PRODUCT_edit.PRICE = PRODUCT.PRICE;
                PRODUCT_edit.QUANTITY = PRODUCT.QUANTITY;
               
                Db_gsj.D_PRODUCT_DETAIL.RemoveRange(PRODUCT_edit.D_PRODUCT_DETAIL);
               Db_gsj.SaveChanges();

                if (file1 != "")
                {
                    D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                    dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                    dproudct.STATUS = 0;
                    dproudct.ACTIVE = true;
                    dproudct.CREATEDATE = DateTime.Now;
                    dproudct.URL_IMAGE = file1;
                    Db_gsj.Entry(dproudct).State = EntityState.Added;
                         
                    Db_gsj.SaveChanges();


                }
                if (file2 != "")
                {
                    D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                    dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                    dproudct.STATUS = 0;
                    dproudct.ACTIVE = true;
                    dproudct.CREATEDATE = DateTime.Now;
                    dproudct.URL_IMAGE = file2;
                    Db_gsj.Entry(dproudct).State = EntityState.Added;
                          
                    Db_gsj.SaveChanges();
                }
                if (file3 != "")
                {
                    D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                    dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                    dproudct.STATUS = 0;
                    dproudct.ACTIVE = true;
                    dproudct.CREATEDATE = DateTime.Now;
                    dproudct.URL_IMAGE = file3;
                    Db_gsj.Entry(dproudct).State = EntityState.Added;
                         
                    Db_gsj.SaveChanges();
                }
                if (file4 != "")
                {
                    D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                    dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                    dproudct.STATUS = 0;
                    dproudct.ACTIVE = true;
                    dproudct.CREATEDATE = DateTime.Now;
                    dproudct.URL_IMAGE = file4;
                    Db_gsj.Entry(dproudct).State = EntityState.Added;
                         
                    Db_gsj.SaveChanges();
                }
                if (file5 != "")
                {
                    D_PRODUCT_DETAIL dproudct = new D_PRODUCT_DETAIL();
                    dproudct.PRODUCT_CD = PRODUCT.PRODUCT_CD;
                    dproudct.STATUS = 0;
                    dproudct.ACTIVE = true;
                    dproudct.CREATEDATE = DateTime.Now;
                    dproudct.URL_IMAGE = file5;
                    Db_gsj.Entry(dproudct).State = EntityState.Added;
                         
                    Db_gsj.SaveChanges();
                }
                return RedirectToAction("Index");
                }
                else
                {
                    Db_gsj = new GJSEntities();
                    ModelState.AddModelError("", "Vui lòng kiểm tra các đường dẫn hình ảnh");
                    var query = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.ToList();
                    ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_DETAIL_CD", "CATEGORY_PRODUCT_DETAIL_NAME", PRODUCT.CATEGORY_PRODUCT_DETAIL_CD);
                    var queryG = Db_gsj.O_CATEGORY_GRANULES.ToList();
                    ViewBag.DropdownlistG = new SelectList(queryG.AsEnumerable(), "CATEGORY_GRANULES_CD", "CATEGORY_GRANULES_NAME", PRODUCT.CATEGORY_GRANULES_CD);
    
                    return View(PRODUCT);
                }
            }
            else
            {
                Db_gsj = new GJSEntities();
                var query = Db_gsj.O_CATEGORY_PRODUCT_DETAIL.ToList();
                ViewBag.Dropdownlist = new SelectList(query.AsEnumerable(), "CATEGORY_PRODUCT_DETAIL_CD", "CATEGORY_PRODUCT_DETAIL_NAME", PRODUCT.CATEGORY_PRODUCT_DETAIL_CD);
                var queryG = Db_gsj.O_CATEGORY_GRANULES.ToList();
                ViewBag.DropdownlistG = new SelectList(queryG.AsEnumerable(), "CATEGORY_GRANULES_CD", "CATEGORY_GRANULES_NAME", PRODUCT.CATEGORY_GRANULES_CD);
    
                return View(PRODUCT);
            }
        }


        public ActionResult Deactive([Bind(Include = "PRODUCT_CD")]O_PRODUCT PRODUCT)
        {
            if (PRODUCT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PRODUCT = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT.PRODUCT_CD);
            PRODUCT.ACTIVE = false;
            Db_gsj.Entry(PRODUCT).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "PRODUCT_CD")]O_PRODUCT PRODUCT)
        {
            if (PRODUCT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            PRODUCT = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT.PRODUCT_CD);
            PRODUCT.ACTIVE = true;
            Db_gsj.Entry(PRODUCT).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult CheckNewAjax(long? PRODUCT_CD,String CHECK)
        {
            Db_gsj = new GJSEntities();
            if(CHECK == "true")
            {
                O_PRODUCT Product = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT_CD);
                Product.CREATEDATE = DateTime.Now;
                O_PRODUCT_NEW PRODUCTNEW = new O_PRODUCT_NEW();
                PRODUCTNEW.O_PRODUCT = Product;
                PRODUCTNEW.PRODUCT_CD = Product.PRODUCT_CD;
                PRODUCTNEW.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(PRODUCTNEW).State = EntityState.Added;
                Db_gsj.Entry(Product).State = EntityState.Modified;
                Db_gsj.SaveChanges();
            }
            else
            {
                List<O_PRODUCT_NEW> PRODUCTNEW = Db_gsj.O_PRODUCT_NEW.Where(t=>t.PRODUCT_CD == PRODUCT_CD).ToList();
                Db_gsj.O_PRODUCT_NEW.RemoveRange(PRODUCTNEW);
                Db_gsj.SaveChanges();
            }
            return Json(1);
        }
        public JsonResult CheckHightlightAjax(long? PRODUCT_CD, String CHECK)
        {
            Db_gsj = new GJSEntities();
            if (CHECK == "true")
            {
                O_PRODUCT Product = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT_CD);
                O_PRODUCT_HIGHLIGHTS PRODUCTHIGHLIGHTS = new O_PRODUCT_HIGHLIGHTS();
                PRODUCTHIGHLIGHTS.O_PRODUCT = Product;
                PRODUCTHIGHLIGHTS.PRODUCT_CD = Product.PRODUCT_CD;
                Db_gsj.Entry(PRODUCTHIGHLIGHTS).State = EntityState.Added;
                Db_gsj.SaveChanges();
            }
            else
            {
                List<O_PRODUCT_HIGHLIGHTS> PRODUCTHIGHLIGHTS = Db_gsj.O_PRODUCT_HIGHLIGHTS.Where(t => t.PRODUCT_CD == PRODUCT_CD).ToList();
                Db_gsj.O_PRODUCT_HIGHLIGHTS.RemoveRange(PRODUCTHIGHLIGHTS);
                Db_gsj.SaveChanges();
            }
            return Json(1);
        }
        public JsonResult CheckSellingAjax(long? PRODUCT_CD, String CHECK)
        {
            Db_gsj = new GJSEntities();
            if (CHECK == "true")
            {
                O_PRODUCT Product = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT_CD);
                O_PRODUCT_SELLING PRODUCTSELLING = new O_PRODUCT_SELLING();
                PRODUCTSELLING.O_PRODUCT = Product;
                PRODUCTSELLING.PRODUCT_CD = Product.PRODUCT_CD;
                Db_gsj.Entry(PRODUCTSELLING).State = EntityState.Added;
                Db_gsj.SaveChanges();
            }
            else
            {
                List<O_PRODUCT_SELLING> PRODUCTSELLING = Db_gsj.O_PRODUCT_SELLING.Where(t => t.PRODUCT_CD == PRODUCT_CD).ToList();
                Db_gsj.O_PRODUCT_SELLING.RemoveRange(PRODUCTSELLING);
                Db_gsj.SaveChanges();
            }
            return Json(1);
        }
        public JsonResult CheckDisplayAjax(long? PRODUCT_CD, String CHECK)
        {
            Db_gsj = new GJSEntities();
            if (CHECK == "true")
            {
                O_PRODUCT Product = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT_CD);
                Product.STATUS = 1;
                Db_gsj.Entry(Product).State = EntityState.Modified;
                Db_gsj.SaveChanges();
            }
            else
            {
                O_PRODUCT Product = Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT_CD);
                Product.STATUS = 0;
                Db_gsj.Entry(Product).State = EntityState.Modified;
                Db_gsj.SaveChanges();
            }
            return Json(1);
        }
    }
}