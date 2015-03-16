using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace W_GJS.Models
{
    public class ChangePasswordModel
    {
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        [StringLength(30, ErrorMessage = "Tối đa {1} tối thiểu là {2} ký tự", MinimumLength = 6)]
        public string USER_PASS_OLD { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        [RegularExpression(@"([a-zA-Z]+[\d]+|[\d]+[a-zA-Z]+)[^\s]*", ErrorMessage = "Vui lòng nhập có số và chữ hoặc kí tự,ví dụ :(kktt444,553jj@)")]
        [StringLength(30, ErrorMessage = "Tối đa {1} tối thiểu là {2} ký tự", MinimumLength = 6)]
        public string USER_PASS_NEW { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu nhập lại")]
        [StringLength(30, ErrorMessage = "Tối đa {1} tối thiểu là {2} ký tự", MinimumLength = 6)]
        [Compare("PassNew", ErrorMessage = "Mật khấu nhập lại không trùng")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [DataType(DataType.Password)]
        public string USER_PASS_CONFIRM { get; set; }

        public static bool changePassword(ChangePasswordModel MODEL, JsonResultChangePasswordModel jsonResult, GJSEntities gjs)
        {
            try
            {
                S_USER USER = gjs.S_USER.Single(t => t.USER_NAME == MODEL.USER_NAME);
                USER.USER_PASS = MODEL.USER_PASS_NEW;
                gjs.Entry(USER).State = EntityState.Modified;
                gjs.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool checkValidation(ChangePasswordModel MODEL, JsonResultChangePasswordModel jsonModel, GJSEntities Db_gsj)
        {

            if (String.IsNullOrEmpty(MODEL.USER_PASS_OLD))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập mật khẩu hiện tài.</li>";
                jsonModel.HasError = true;
            }
            else
            {
                S_USER USER = Db_gsj.S_USER.Single(t => t.USER_NAME == MODEL.USER_NAME);
                if (USER.USER_PASS != MODEL.USER_PASS_OLD)
                {
                    jsonModel.ErrorString += "<li>Mật khẩu hiện tại không đúng.</li>";
                    jsonModel.HasError = true;
                }
            }

            if (String.IsNullOrEmpty(MODEL.USER_PASS_NEW))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập mật khẩu mới.</li>";
                jsonModel.HasError = true;
            }
            else
            {
                if (MODEL.USER_PASS_NEW.Length < 6)
                {
                    jsonModel.ErrorString += "<li>Mật khẩu phải có từ 6 kí tự trở lên.</li>";
                    jsonModel.HasError = true;
                }
            }

            if (String.IsNullOrEmpty(MODEL.USER_PASS_CONFIRM))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập mật khẩu xác nhận.</li>";
                jsonModel.HasError = true;
            }

            // Re-password matching.
            if (MODEL.USER_PASS_NEW != MODEL.USER_PASS_CONFIRM)
            {
                jsonModel.ErrorString += "<li>Mật khẩu nhập lại phải khớp với mật khẩu mới.</li>";
                jsonModel.HasError = true;
            }



            return true;
        }
    }
}