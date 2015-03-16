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
    public class AdministratorController : Controller
    {
        //
        // GET: /BRANCH/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            JsonResultForgotPasswordModel model = new JsonResultForgotPasswordModel();
            
            return View(model);
            
        }
       
        public ActionResult ForgotPasswordEmailSuccessful()
        {
            return View();
        }

        public ActionResult ChangePasswordEmailSuccessful()
        {
            return View();
        }

        public ActionResult ChangePassword(string hash)
        {
           
            Db_gsj = new GJSEntities();
            M_EMPLOYEE ADMIN = Db_gsj.M_EMPLOYEE.Single(t => t.EMPLOYEE_CD == 1);
            if (Process.HashMD5(ADMIN.EMAIL) != hash)
            {
                return RedirectToAction("Index", "Home");
            }
            JsonResultChangePasswordModel model = new JsonResultChangePasswordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(string password_new, string password_confirm)
        {

            

            JsonResultChangePasswordModel model = new JsonResultChangePasswordModel();
            model.HasError = false;

            if (String.IsNullOrEmpty(password_new))
            {
                model.ErrorString += "<li>Vui lòng nhập mật khẩu mới.</li>";
                model.HasError = true;
            }

            if (String.IsNullOrEmpty(password_confirm))
            {
                model.ErrorString += "<li>Vui lòng nhập mật khẩu xác nhận.</li>";
                model.HasError = true;
            }

            if (password_new != password_confirm)
            {
                model.ErrorString += "<li>Mật khẩu xác nhận không khớp.</li>";
                model.HasError = true;
            }

            if (!model.HasError)
            {
                Db_gsj = new GJSEntities();
                S_USER ADMIN = Db_gsj.S_USER.Single(t => t.USER_CD == 1);
                ADMIN.USER_PASS = password_new;
                Db_gsj.Entry(ADMIN).State = EntityState.Modified;
                Db_gsj.SaveChanges();
                return RedirectToAction("ChangePasswordEmailSuccessful");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            Db_gsj = new GJSEntities();

            JsonResultForgotPasswordModel jsonModel = new JsonResultForgotPasswordModel();
            jsonModel.HasError = false;
            M_EMPLOYEE ADMIN = Db_gsj.M_EMPLOYEE.Single(t => t.EMPLOYEE_CD == 1);
            if (ADMIN.EMAIL != email)
            {
                jsonModel.ErrorString = "Email không hợp lệ, vui lòng nhập email khác!";
                jsonModel.HasError = true;
            }
            else
            {
                string controllerUrl = Url.Action("ChangePassword", "Administrator", null, Request.Url.Scheme);
                string hashEmail = Process.HashMD5(email);
                string url = controllerUrl + "?hash=" + hashEmail;

                Process.ProcessSendMail.SendMail_ForgotPassword(email, url);
                return RedirectToAction("ForgotPasswordEmailSuccessful");
            }
            return View(jsonModel);
        }

        public ActionResult Login()
        {
            return View(new JsonResultLoginModel());
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Db_gsj = new GJSEntities();

            JsonResultLoginModel jsonModel = LoginModel.Login(username, password, true);
            if (jsonModel.RoleOrFailed == 1 || jsonModel.RoleOrFailed == 4) // admin logined
            {
                //store session if succeed
                Session[SessionConstants.LOGINED_ADMIN_KEY] = username;
                Session[SessionConstants.LOGINED_ADMIN_ROLE_KEY] = jsonModel.RoleOrFailed;
                return RedirectToAction("Index");
            }

            return View(jsonModel);
        }

        public ActionResult Logout()
        {
            // remove session
            Session[SessionConstants.LOGINED_ADMIN_KEY] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FormSample()
        {
            return View();
        }
	}
}