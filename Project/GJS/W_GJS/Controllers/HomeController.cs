using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using W_GJS.General;
using W_GJS.Models;
namespace W_GJS.Controllers
{
    public class HomeController : Controller
    {

        public GJSEntities Db_gsj;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDisplay()
        {
           
            return View();
        }



        [HttpPost]
        public ActionResult ChangePassword(string USER_PASS_OLD, string USER_PASS_NEW, string USER_PASS_CONFIRM)
        {
            Db_gsj = new GJSEntities();
            ChangePasswordModel MODEL = new ChangePasswordModel();
            MODEL.USER_PASS_CONFIRM = USER_PASS_CONFIRM;
            MODEL.USER_PASS_NEW = USER_PASS_NEW;
            MODEL.USER_PASS_OLD = USER_PASS_OLD;
            if (!String.IsNullOrEmpty((String)Session[W_GJS.General.SessionConstants.LOGINED_USER_KEY]))
            {
                MODEL.USER_NAME = (string)Session[W_GJS.General.SessionConstants.LOGINED_USER_KEY];
            }
            JsonResultChangePasswordModel jsonModel = new JsonResultChangePasswordModel();
            jsonModel.HasError = false;
            ChangePasswordModel.checkValidation(MODEL, jsonModel, Db_gsj);
            if (!jsonModel.HasError) 
            {
                ChangePasswordModel.changePassword(MODEL, jsonModel, Db_gsj);
            }
            return Json(jsonModel);
        }

        [HttpPost]
        public ActionResult UserInformationUpdate(RegisterModel MODEL)
        {
            JsonResultRegisterModel jsonModel = new JsonResultRegisterModel();
            jsonModel.HasError = false;

            Db_gsj = new GJSEntities();
            if (!String.IsNullOrEmpty((String)Session[W_GJS.General.SessionConstants.LOGINED_USER_KEY]))
            {
                MODEL.USER_NAME = (string)Session[W_GJS.General.SessionConstants.LOGINED_USER_KEY];
            }
            RegisterModel.checkValidation(MODEL, jsonModel, Db_gsj, false);

            if (!jsonModel.HasError)
            {
                RegisterModel.UpdateInformation(MODEL, jsonModel, Db_gsj);
                
            }
            return Json(jsonModel);
        }

        public ActionResult UserInformation()
        {
            string USERNAME = (string)Session[W_GJS.General.SessionConstants.LOGINED_USER_KEY];
            if (String.IsNullOrEmpty(USERNAME))
            {
                return RedirectToAction("Index", "Home");
            }
            Db_gsj = new GJSEntities();
            return View(Db_gsj.S_USER.Single(t=>t.USER_NAME == USERNAME).O_USER_CUSTOMER.First().O_CUSTOMER);
        }
        public ActionResult SearchProducts(string keyword)
        {
            Db_gsj = new GJSEntities();
            SearchResultModel resultModel = new SearchResultModel();
            List<O_PRODUCT> listproduct = Db_gsj.O_PRODUCT.Where(t => t.ACTIVE == true && (
                t.O_CATEGORY_GRANULES.CATEGORY_GRANULES_CONTENT.Contains(keyword)
                || t.O_CATEGORY_GRANULES.CATEGORY_GRANULES_NAME.Contains(keyword)
                || t.O_CATEGORY_GRANULES.CATEGORY_GRANULES_WEIGHT.Contains(keyword)

                || t.O_CATEGORY_PRODUCT.CATEGORY_PRODUCT_CODE.Contains(keyword)
                || t.O_CATEGORY_PRODUCT.CATEGORY_PRODUCT_NAME.Contains(keyword)

                || t.O_CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_CODE.Contains(keyword)
                || t.O_CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_NAME.Contains(keyword)

                || t.PRODUCT_CODE.Contains(keyword)
                || t.PRODUCT_CONTENT.Contains(keyword)
                || t.PRODUCT_NAME.Contains(keyword)
                || t.PRODUCT_TITLE.Contains(keyword)
                || t.SIZE.Contains(keyword)
                || t.TAG_ALT.Contains(keyword)
                || t.URL_IMAGE.Contains(keyword)
                || t.WEIGHT.Contains(keyword)
                || t.CURRENCY.Contains(keyword))).ToList();
            if (Session[SessionConstants.LOGINED_USER_ROLE_KEY] != null
                && (int)Session[SessionConstants.LOGINED_USER_ROLE_KEY] == 1)
            {
                listproduct.Where(t => t.STATUS == 1);
            }
            resultModel.HtmlString = RenderPartialViewToString("ProductLazyList", listproduct);
            resultModel.NumberOfItemFound = listproduct.Count;
            return View(resultModel);
        }

        [ChildActionOnly]
        public ActionResult ProductLazyList(List<W_GJS.Models.O_PRODUCT> Model)
        {
            return PartialView(Model);
        }
        [ChildActionOnly]
        public ActionResult ProductLazyCatalogList(List<W_GJS.Models.O_PRODUCT> Model)
        {
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult CategoryProductListPaging(long CATEGORY_PRODUCT_CD, bool productMenu, long numberOfItemsInPage, long currentPage)
        {
            Db_gsj = new GJSEntities();
            ProductCategoryModel model = new ProductCategoryModel();

            List<W_GJS.Models.O_PRODUCT> productList = new List<O_PRODUCT>();
            if (productMenu)
            {
                productList = Db_gsj.O_PRODUCT.Where(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT_CD && t.STATUS == 1 && t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE).ToList();
            }
            else
	        {
                productList = Db_gsj.O_PRODUCT.Where(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT_CD && t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE).ToList();
	        }


            model.Identified = CATEGORY_PRODUCT_CD;
            model.TotalItems = productList.Count;
            model.CurrentPage = currentPage;
            model.TotalPages = productList.Count / numberOfItemsInPage;
            if (productList.Count % numberOfItemsInPage != 0)
            {
                model.TotalPages++;
            }
            model.ItemOrderFrom = (currentPage - 1) * numberOfItemsInPage;
            List<W_GJS.Models.O_PRODUCT> list = productList.Skip((int)model.ItemOrderFrom).Take((int)numberOfItemsInPage).ToList();
            model.ItemOrderTo = model.ItemOrderFrom + list.Count;
            model.ItemOrderFrom++;
            model.HtmlListString = RenderPartialViewToString("ProductLazyCatalogList", list);

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult SubCategoryProducDetailtListPaging(long CATEGORY_PRODUCT_DETAIL_CD, bool productMenu, long numberOfItemsInPage, long currentPage)
        {
            Db_gsj = new GJSEntities();
            ProductCategoryModel model = new ProductCategoryModel();
            List<W_GJS.Models.O_PRODUCT> productList = new List<O_PRODUCT>();

            if (productMenu)
            {
                productList = Db_gsj.O_PRODUCT.Where(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL_CD && t.ACTIVE == true && t.STATUS == 1).OrderByDescending(x => x.CREATEDATE).ToList();
            }
            else
            {
                productList = Db_gsj.O_PRODUCT.Where(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL_CD && t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE).ToList();
            }

            model.Identified = CATEGORY_PRODUCT_DETAIL_CD;
            model.TotalItems = productList.Count;
            model.CurrentPage = currentPage;
            model.TotalPages = productList.Count / numberOfItemsInPage;
            if (productList.Count % numberOfItemsInPage != 0)
            {
                model.TotalPages++;
            }
            model.ItemOrderFrom = (currentPage - 1) * numberOfItemsInPage;
            List<W_GJS.Models.O_PRODUCT> list = productList.Skip((int)model.ItemOrderFrom).Take((int)numberOfItemsInPage).ToList();
            model.ItemOrderTo = model.ItemOrderFrom + list.Count;
            model.ItemOrderFrom++;
            model.HtmlListString = RenderPartialViewToString("ProductLazyCatalogList", list);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CategoryProducDetailtListPaging(long CATEGORY_PRODUCT_DETAIL_CD, bool productMenu, long numberOfItemsInPage, long currentPage)
        {
            Db_gsj = new GJSEntities();
            ProductCategoryModel model = new ProductCategoryModel();
            List<W_GJS.Models.O_PRODUCT> productList = new List<O_PRODUCT>();

            if (productMenu)
            {
                productList = Db_gsj.O_PRODUCT.Where(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL_CD && t.ACTIVE == true && t.STATUS == 1).OrderByDescending(x => x.CREATEDATE).ToList();
            }
            else
            {
                productList = Db_gsj.O_PRODUCT.Where(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL_CD && t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE).ToList();
            }

            model.Identified = CATEGORY_PRODUCT_DETAIL_CD;
            model.TotalItems = productList.Count;
            model.CurrentPage = currentPage;
            model.TotalPages = productList.Count / numberOfItemsInPage;
            if (productList.Count % numberOfItemsInPage != 0)
            {
                model.TotalPages++;
            }
            model.ItemOrderFrom = (currentPage - 1) * numberOfItemsInPage;
            List<W_GJS.Models.O_PRODUCT> list = productList.Skip((int)model.ItemOrderFrom).Take((int)numberOfItemsInPage).ToList();
            model.ItemOrderTo = model.ItemOrderFrom + list.Count;
            model.ItemOrderFrom++;
            model.HtmlListString = RenderPartialViewToString("ProductLazyCatalogList", list);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult NewsHomePaging(long numberOfItemsInPage, long currentPage, string keyword)
        {
            Db_gsj = new GJSEntities();
            NewsHomeModel model = new NewsHomeModel();
            List<W_GJS.Models.O_NEWS> newsList = null;
            if (String.IsNullOrEmpty(keyword))
            {
                newsList = Db_gsj.O_NEWS.Where(t => t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE).ToList();
            }
            else
	        {
                newsList = Db_gsj.O_NEWS.Where(t => t.ACTIVE == true && (
                        t.IMAGE_NEWS.Contains(keyword)
                        || t.NEW_DESCRIPTIONS.Contains(keyword)
                        || t.NEWS_CONTENT.Contains(keyword)
                        || t.NEWS_TITLE.Contains(keyword)
                        || t.SOURCE_COPY.Contains(keyword)
                        || t.TAG_ALT.Contains(keyword)
                        || t.M_EMPLOYEE.EMPLOYEE_NAME.Contains(keyword)
                        || t.O_CATEGORY_NEWS.CATEGORY_NEWS_CODE.Contains(keyword)
                        || t.O_CATEGORY_NEWS.CATEGORY_NEWS_NAME.Contains(keyword))).OrderByDescending(x => x.CREATEDATE).ToList();
	        }
            model.TotalItems = newsList.Count;
            model.CurrentPage = currentPage;
            model.TotalPages = newsList.Count / numberOfItemsInPage;
            if (newsList.Count % numberOfItemsInPage != 0)
            {
                model.TotalPages++;
            }
            model.ItemOrderFrom = (currentPage - 1) * numberOfItemsInPage;
            List<W_GJS.Models.O_NEWS> list = newsList.Skip((int)model.ItemOrderFrom).Take((int)numberOfItemsInPage).ToList();
            model.ItemOrderTo = model.ItemOrderFrom + list.Count;
            model.ItemOrderFrom++;
            model.NewsList = list;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CategoryNewsPaging(long CATEGORY_NEWS_CD, long numberOfItemsInPage, long currentPage)
        {
            Db_gsj = new GJSEntities();
            CategoryNewsModel model = new CategoryNewsModel();
            List<W_GJS.Models.O_NEWS> newsList = Db_gsj.O_NEWS.Where(t => t.CATEGORY_NEWS_CD == CATEGORY_NEWS_CD && t.ACTIVE == true).ToList();
            newsList = newsList.Skip(1).ToList();
            model.TotalItems = newsList.Count;
            model.CurrentPage = currentPage;
            model.TotalPages = newsList.Count / numberOfItemsInPage;
            if (newsList.Count % numberOfItemsInPage != 0)
            {
                model.TotalPages++;
            }
            model.ItemOrderFrom = (currentPage - 1) * numberOfItemsInPage;
            List<W_GJS.Models.O_NEWS> list = newsList.Skip((int)model.ItemOrderFrom).Take((int)numberOfItemsInPage).ToList();
            model.ItemOrderTo = model.ItemOrderFrom + list.Count;
            model.ItemOrderFrom++;
            model.NewsList = list;

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult NewsList(List<W_GJS.Models.O_PRODUCT> Model)
        {
            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult GetNext9Products(int blockNumber)
        {
            Db_gsj = new GJSEntities();
            JsonModel jsonModel = new JsonModel();
            List<W_GJS.Models.O_PRODUCT> listproduct = new List<W_GJS.Models.O_PRODUCT>();
            int numItemsInBlock = 9;
            int totalItems = Db_gsj.O_PRODUCT.Where(t => t.ACTIVE == true && t.STATUS == 1).Count();
            // check if out of size.
            //if (totalItems / numItemsInBlock < blockNumber)
            //{
            //        jsonModel.NoMoreData = true;
            //}
            //else
            //{
                if (totalItems / numItemsInBlock < (blockNumber + 1))
                {
                    jsonModel.NoMoreData = true;
                }
                else
                {
                    jsonModel.NoMoreData = false;
                }
                // calc beginPos
                int beginPos = numItemsInBlock * blockNumber;
                listproduct = Db_gsj.O_PRODUCT.Where(t => t.ACTIVE == true && t.STATUS == 1).OrderByDescending(x => x.CREATEDATE).ToList().Skip(beginPos).Take(numItemsInBlock).ToList();
            //}

            
            jsonModel.HTMLString = RenderPartialViewToString("ProductLazyList", listproduct);

            return Json(jsonModel);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Db_gsj = new GJSEntities();

            JsonResultLoginModel jsonModel = LoginModel.Login(username, password, false);
            if (jsonModel.RoleOrFailed != 0) // failed
            {
                //store session if succeed
                S_USER user = Db_gsj.S_USER.Single(t => t.USER_NAME == username);
                Session[SessionConstants.LOGINED_CUSTOMER_CD_KEY] = user.O_USER_CUSTOMER.ToList()[0].CUSTOMER_CD;
                Session[SessionConstants.LOGINED_USER_KEY] = username;
                Session[SessionConstants.LOGINED_USER_ROLE_KEY] = jsonModel.RoleOrFailed;
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }

            return Json(jsonModel);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            // remove session
            Session[SessionConstants.LOGINED_USER_KEY] = null;
            Session[SessionConstants.LOGINED_USER_ROLE_KEY] = null;

            JsonResultLogoutModel jsonModel = new JsonResultLogoutModel();
            jsonModel.HasError = false;
            return Json(jsonModel);
        }

        public ActionResult Register(RegisterModel MODEL)
        {
            JsonResultRegisterModel jsonModel = new JsonResultRegisterModel();
            jsonModel.HasError = false;

            Db_gsj = new GJSEntities();

            RegisterModel.checkValidation(MODEL, jsonModel, Db_gsj, true);

            if (!jsonModel.HasError)
            {
                RegisterModel.Register(MODEL, jsonModel, Db_gsj);
                Session[SessionConstants.LOGINED_USER_KEY] = MODEL.USER_NAME;
                Session[SessionConstants.LOGINED_USER_ROLE_KEY] = 3;
                Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            return Json(jsonModel);
        }

        public ActionResult DiamondAssess()
        {
            return View();
        }

        public ActionResult ColoredStonesAssess()
        {
            return View();
        }

        public ActionResult LaserInscription()
        {
            return View();
        }

        public ActionResult News()
        {
            Db_gsj = new GJSEntities();
            SearchNewsResultModel resultModel = new SearchNewsResultModel();
            List<O_NEWS> list = Db_gsj.O_NEWS.Where(t => t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE).ToList();
            resultModel.ResultList = list;
            resultModel.IsSearching = false;
            resultModel.NumberOfItemFound = list.Count;
            return View(resultModel);
        }

        public ActionResult SearchNews(string keyword)
        {
            Db_gsj = new GJSEntities();
            SearchNewsResultModel resultModel = new SearchNewsResultModel();
            List<O_NEWS> list = Db_gsj.O_NEWS.Where(t => t.ACTIVE == true && (
                t.IMAGE_NEWS.Contains(keyword)
                || t.NEW_DESCRIPTIONS.Contains(keyword)
                || t.NEWS_CONTENT.Contains(keyword)
                || t.NEWS_TITLE.Contains(keyword)
                || t.SOURCE_COPY.Contains(keyword)
                || t.TAG_ALT.Contains(keyword)
                || t.M_EMPLOYEE.EMPLOYEE_NAME.Contains(keyword)
                || t.O_CATEGORY_NEWS.CATEGORY_NEWS_CODE.Contains(keyword)
                || t.O_CATEGORY_NEWS.CATEGORY_NEWS_NAME.Contains(keyword))).OrderByDescending(x => x.CREATEDATE).ToList();
            resultModel.ResultList = list;
            resultModel.IsSearching = true;
            resultModel.NumberOfItemFound = list.Count;
            resultModel.keyword = keyword;
            return View("News", resultModel);
        }

        public ActionResult NewsDetail([Bind(Include = "NEWS_CD")]O_NEWS NEWS)
        {
            Db_gsj = new GJSEntities();
            // Chưa xử lý: lúc active rồi thì hiện page 404
            return View(Db_gsj.O_NEWS.Single(t=>t.NEWS_CD == NEWS.NEWS_CD));
        }

        public ActionResult Category_News([Bind(Include = "CATEGORY_NEWS_CD")]O_CATEGORY_NEWS CATEGORY_NEWS)
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_NEWS.Where(t => t.CATEGORY_NEWS_CD == CATEGORY_NEWS.CATEGORY_NEWS_CD && t.ACTIVE == true).OrderByDescending(x => x.CREATEDATE).ToList());
        }

       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Recruitment()
        {
            ViewBag.Message = "Tuyển dụng.";

            return View();
        }

        public ActionResult AboutCompany()
        {
            ViewBag.Message = "Giới thiệu công ty.";

            return View();
        }

        public ActionResult AboutAchievements()
        {
            ViewBag.Message = "Thành tựu.";

            return View();
        }

        public ActionResult AboutDevelopment()
        {
            ViewBag.Messagetham = "Quá trình phát triển.";

            return View();
        }

        public ActionResult AboutLicense()
        {
            ViewBag.Message = "Công ty TNHH Ngọc thẩm.";

            return View();
        }

        public ActionResult AboutVision()
        {
            ViewBag.Message = "Tầm nhìn và phát triển.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductDetail([Bind(Include = "PRODUCT_CD")]O_PRODUCT PRODUCT)
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_PRODUCT.Single(t=>t.PRODUCT_CD == PRODUCT.PRODUCT_CD));
        }
        public ActionResult ProductDetailCatalog([Bind(Include = "PRODUCT_CD")]O_PRODUCT PRODUCT)
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.O_PRODUCT.Single(t => t.PRODUCT_CD == PRODUCT.PRODUCT_CD));
        }

        public ActionResult Product_Category([Bind(Include = "CATEGORY_PRODUCT_CD")]O_CATEGORY_PRODUCT CATEGORY_PRODUCT)
        {
            Db_gsj = new GJSEntities();
            ViewBag.Name = Db_gsj.O_CATEGORY_PRODUCT.Single(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT.CATEGORY_PRODUCT_CD).CATEGORY_PRODUCT_NAME;

            return View(Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Where(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT.CATEGORY_PRODUCT_CD && t.ACTIVE == true).ToList());
        }

        public ActionResult Product_Category_Detail([Bind(Include = "CATEGORY_PRODUCT_DETAIL_CD")]O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL)
        {
            Db_gsj = new GJSEntities();
            
            return View(Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_CD));
        }
        public ActionResult ProductList()
        {
            
            return View();
        }

        public ActionResult ShowAlbum()
        {

            return View();
        }

        public ActionResult Catalog()
        {
            return View();
        }
        public ActionResult CatalogDetail(long CATEGORY_PRODUCT_CD)
        {
            Db_gsj = new GJSEntities();
            ViewBag.Name = Db_gsj.O_CATEGORY_PRODUCT.Single(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT_CD).CATEGORY_PRODUCT_NAME;
            return View(Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Where(t => t.CATEGORY_PRODUCT_CD == CATEGORY_PRODUCT_CD && t.ACTIVE == true).ToList());
        }
        public ActionResult Catalog_Category_Detail([Bind(Include = "CATEGORY_PRODUCT_DETAIL_CD")]O_CATEGORY_PRODUCT_DETAIL CATEGORY_PRODUCT_DETAIL)
        {
            Db_gsj = new GJSEntities();

            return View(Db_gsj.O_CATEGORY_PRODUCT_DETAIL.Single(t => t.CATEGORY_PRODUCT_DETAIL_CD == CATEGORY_PRODUCT_DETAIL.CATEGORY_PRODUCT_DETAIL_CD));
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult ViewCart()
        {

            ViewBag.Where = "Xem giỏ hàng";

            O_ORDER ord = (O_ORDER)Session["Cart"];
            if (ord != null)
            {
                ViewData.Model = ord;
                ViewBag.Price_Total = O_ORDER.Price_Total(ord);
                return View();

            }
            else
                return RedirectToAction("ProductDisplay", "Home");
        }
        [HttpGet]
        public ActionResult AddCart([Bind(Include = "PRODUCT_CD,QUANTITY,SIZE")]O_PRODUCT pro)
        {
            O_ORDER ord = (O_ORDER)Session["Cart"];
            if (ord == null)
                ord = new O_ORDER();
            int ketqua = ord.AddCart(pro);
            Session.Add("Cart", ord);
            Response.Redirect(Request.UrlReferrer.AbsoluteUri);

            return RedirectToAction("ProductDisplay", "Home");
            
        }
        [HttpPost]
        public ActionResult AddCartQuantity([Bind(Include = "PRODUCT_CD,QUANTITY,SIZE")]O_PRODUCT pro)
        {
        
            O_ORDER ord = (O_ORDER)Session["Cart"];
            if (ord == null)
                ord = new O_ORDER();
            int ketqua = ord.AddCart(pro);
            Session.Add("Cart", ord);
            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("ProductDisplay", "Home");
            
        }
        [HttpPost]
        public ActionResult UpdateCart([Bind(Include = "PRODUCT_CD,QUANTITY")]O_PRODUCT pro)
        {

            O_ORDER ord = (O_ORDER)Session["Cart"];
            ord.UpdateCart(pro);
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteCart([Bind(Include = "PRODUCT_CD")]O_PRODUCT pro)
        {

            O_ORDER ord = (O_ORDER)Session["Cart"];
            if (ord.D_ORDER_DETAIL.Count == 1)
            {
                Session["Cart"] = null;
                return RedirectToAction("ProductDisplay", "Home");
            }
            else
            {
                ord.DeleteCart(pro);
                return RedirectToAction("Cart");
            }

        }
        public ActionResult DeleteAllCart()
        {
            Session["Cart"] = null;
            return RedirectToAction("ProductDisplay", "Home");

        }
        [HttpPost]
        public ActionResult Checkout()
        {
            O_ORDER ord = (O_ORDER)Session["Cart"];
            if (ord != null)
            {
                Db_gsj = new GJSEntities();
                O_ORDER ordcheckout = new O_ORDER();
                ordcheckout.ACTIVE = true;
                ordcheckout.STATUS = 0;
                ordcheckout.CREATEDATE = DateTime.Now;
                long LOGINED_CUSTOMER_CD = (long)Session[SessionConstants.LOGINED_CUSTOMER_CD_KEY];
                ordcheckout.CUSTOMER_CD = LOGINED_CUSTOMER_CD;
                ordcheckout.O_CUSTOMER = Db_gsj.O_CUSTOMER.Single(t => t.CUSTOMER_CD == LOGINED_CUSTOMER_CD);
                ordcheckout.PHONE = ordcheckout.O_CUSTOMER.PHONE;
                ordcheckout.EMAIL = ordcheckout.O_CUSTOMER.EMAIL;
                ordcheckout.DELIVERY_ADDRESS = ordcheckout.O_CUSTOMER.ADDRESS;
                Db_gsj.Entry(ordcheckout).State = EntityState.Added;
                Db_gsj.SaveChanges();
                ordcheckout.ORDER_CODE = CodeConstants.ORDER_CODE + ordcheckout.ORDER_CD.ToString();
                Db_gsj.Entry(ordcheckout).State = EntityState.Modified;
                Db_gsj.SaveChanges();
                foreach (var item in ord.D_ORDER_DETAIL)
                {
                    D_ORDER_DETAIL orddetail = new D_ORDER_DETAIL();
                    orddetail.STATUS = 0;
                    orddetail.CREATEDATE = DateTime.Now;
                    orddetail.ACTIVE = true;
                    orddetail.ORDER_CD = ordcheckout.ORDER_CD;
                    orddetail.PRODUCT_CD = item.PRODUCT_CD;
                    orddetail.QUANTITY = item.QUANTITY;
                    orddetail.SIZE = item.SIZE;
                    orddetail.PRICE = item.PRICE;
                    Db_gsj.Entry(orddetail).State = EntityState.Added;
                    Db_gsj.SaveChanges();
                }
                Session["Cart"] = null;
                return RedirectToAction("ProductDisplay", "Home");
            }
            else
            {
                return RedirectToAction("ProductDisplay", "Home");

            }
        }
        
        public ActionResult CheckoutComplete()
        {
            ViewBag.Where = "Thanh toán thành công";

            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult ChatAdmin()
        {
            return View();
        }
        public ActionResult Branches()
        {
            Db_gsj = new GJSEntities();
            var query = Db_gsj.O_CITIES.Where(t => t.ACTIVE == true).Select(t => new { t.CITIES_CD,t.CITIES_NAME} ).ToList();
            ViewBag.Cities = new SelectList(query.AsEnumerable(), "CITIES_CD", "CITIES_NAME",1);
            var queryD = Db_gsj.D_CITIES_DETAIL.Where(t => t.CITIES_CD == 1 && t.ACTIVE == true).ToList();
            ViewBag.DCities = new SelectList(queryD.AsEnumerable(), "CITIES_DETAIL_CD", "CITIES_DETAIL_NAME",1);

            return View(Db_gsj.O_BRANCH.Where(t => t.CITIES_CD == 1 && t.ACTIVE == true).ToList());
        }

        [HttpPost]
        public ActionResult Branches(string CITIES_CD, string CITIES_DETAIL_CD)
        {
            Db_gsj = new GJSEntities();
           
            return View();
        }

        public ActionResult GoogleMap(long CD)
        {
            Db_gsj = new GJSEntities();
            O_BRANCH branch = Db_gsj.O_BRANCH.Single(t => t.BRANCH_CD == CD);
            ViewBag.LATITUDE = branch.LATITUDE;
            ViewBag.LONGITUDE = branch.LONGITUDE;
            return View();
        }
        [HttpPost]

        public JsonResult GetDetailByCityAJAX(long? CITIES_CD)
 
        {
            Db_gsj = new GJSEntities();
            return Json(Db_gsj.D_CITIES_DETAIL.Where(t => t.ACTIVE == true && t.CITIES_CD == CITIES_CD).ToList().Select(t => new { t.CITIES_DETAIL_CD, t.CITIES_DETAIL_NAME }).ToArray());
 
        }

         [HttpPost]

        public JsonResult GetBrancheslByCityAJAX(long? CITIES_CD)
        {
            Db_gsj = new GJSEntities();
            return Json(Db_gsj.O_BRANCH.Where(t => t.ACTIVE == true && t.CITIES_CD == CITIES_CD).ToList().Select(t => new { BRANCH_CODE = Url.Action("GoogleMap", new { CD = t.BRANCH_CD }), t.BRANCH_NAME, t.ADDRESS, t.PHONE, t.LATITUDE, t.LONGITUDE }).ToArray());

        }

         [HttpPost]

         public JsonResult GetBrancheslByDCityAJAX(long? CITIES_DETAIL_CD)
         {
             Db_gsj = new GJSEntities();
             var blogs = from b in Db_gsj.O_BRANCH
                         where b.ACTIVE == true && b.CITIES_DETAIL_CD == CITIES_DETAIL_CD
                         select b; 
             return Json(Db_gsj.O_BRANCH.Where(t => t.ACTIVE == true && t.CITIES_DETAIL_CD == CITIES_DETAIL_CD).ToList().Select(t => new { BRANCH_CODE = Url.Action("GoogleMap", new {CD = t.BRANCH_CD}), t.BRANCH_NAME, t.ADDRESS, t.PHONE, t.LATITUDE, t.LONGITUDE }).ToArray());

         }
         public ActionResult ShowCategoryAlbum(long CATEGORY_ALBUM_CD)
         {
             Db_gsj = new GJSEntities();
             ViewBag.Name = Db_gsj.O_CATEGORY_ALBUM.Single(t => t.CATEGORY_ALBUM_CD == CATEGORY_ALBUM_CD).CATEGORY_ALBUM_NAME;
             return View(Db_gsj.O_ALBUM.Where(t => t.CATEGORY_ALBUM_CD == CATEGORY_ALBUM_CD).ToList());
         }

         [HttpPost]
         public ActionResult SendContract(ContractModel contract)
         {
             JsonResultContactModel jsonModel = new JsonResultContactModel();
             jsonModel.HasError = false;

             Db_gsj = new GJSEntities();

             ContractModel.checkValidation(contract, jsonModel, Db_gsj);

             if (!jsonModel.HasError)
             {
                 Process.ProcessSendMail.SendMail_Contract(contract);
                 //Response.Redirect(Request.UrlReferrer.AbsoluteUri);
             }
             return Json(jsonModel);
         }
      
    }
}