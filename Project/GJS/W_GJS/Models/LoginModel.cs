using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace W_GJS.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [DataType(DataType.Text)]
        [Display(Name = "Tên đăng nhập")]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        [Display(Name = "Mật khẩu")]
        public string USER_PASS { get; set; }

        public static JsonResultLoginModel Login(string username, string password, bool admin)
        {
            JsonResultLoginModel jsonModel = new JsonResultLoginModel();
            GJSEntities Db_gsj = new GJSEntities();
            //check exist user (ensure that no duplicate user)
            S_USER USER_found = null;
            if (!admin)
            {
                USER_found = Db_gsj.S_USER.FirstOrDefault(t => t.USER_NAME == username && (t.STATUS == 2 || t.STATUS == 3));
                if (USER_found == null)
                {
                    jsonModel.RoleOrFailed = 0;
                    jsonModel.ErrorString = "Tên tài khoản không đúng.";
                    return jsonModel;
                }
            }
            else
            {
                USER_found = Db_gsj.S_USER.FirstOrDefault(t => t.USER_NAME == username && (t.STATUS == 1 || t.STATUS == 4));
                if (USER_found == null)
                {
                    jsonModel.RoleOrFailed = 0;
                    jsonModel.ErrorString = "Tên tài khoản không đúng.";
                    return jsonModel;
                }

            }
            //check active
            if (USER_found.ACTIVE == false)
            {
                jsonModel.RoleOrFailed = 0;
                jsonModel.ErrorString = "tên tài khoản không đúng.";
                return jsonModel;
            }

            //compare password
            if (password != USER_found.USER_PASS)
            {
                jsonModel.RoleOrFailed = 0;
                jsonModel.ErrorString = "Nhập sai mật khẩu, vui lòng nhập lại.";
                return jsonModel;
            }


            jsonModel.RoleOrFailed = (int)USER_found.STATUS;
            return jsonModel;
            // 0: fail
            // 1: user normal login
            // 2: user catalog login
            // 3: admin login
        }

        public static void Logout()
        {
            // do nothing
        }
    }
}