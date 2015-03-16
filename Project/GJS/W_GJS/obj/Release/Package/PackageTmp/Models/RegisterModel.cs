using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text.RegularExpressions;
using W_GJS.General;


namespace W_GJS.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [DataType(DataType.Text)]
        [Display(Name = "Tên đăng nhập")]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Text)]
        [Display(Name = "Mật khẩu")]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        public string USER_PASS { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [DataType(DataType.Text)]
        [Display(Name = "Nhập lại Mật khẩu")]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        public string USER_RE_PASS { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ của bạn")]
        [DataType(DataType.Text)]
        [Display(Name = "Họ")]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        public string FIRST_NAME { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
        [DataType(DataType.Text)]
        [Display(Name = "Tên")]
        [RegularExpression(@"\w\w*\b(?=(\s|\r|\n|$))", ErrorMessage = "Vui lòng không nhập kí tự đặt biệt")]
        public string LAST_NAME { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [DataType(DataType.Text)]
        [Display(Name = "Giới tính")]
        public string SEX { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [DataType(DataType.Text)]
        [Display(Name = "Địa chỉ")]
        public string ADDRESS { get; set; }

       
        public string PHONE_NUMBER { get; set; }

        public bool SUBSCRIBE { get; set; }

        public static bool Register(RegisterModel MODEL, JsonResultRegisterModel jsonModel, GJSEntities Db_gsj)
        {
            
            try
            {
                // store user entity: username, password, active = 1, create date = now, status = 3 (user normal always)
                S_USER USER = new S_USER();
                USER.ACTIVE = true;
                USER.STATUS = 3;
                USER.CREATEDATE = DateTime.Now;
                USER.USER_NAME = MODEL.USER_NAME;
                USER.USER_PASS = MODEL.USER_PASS;

                Db_gsj.Entry(USER).State = EntityState.Added;
                Db_gsj.SaveChanges();

                // store customer entity: email, first name, last name, sex, active = 1, create date = now
                O_CUSTOMER CUSTOMER = new O_CUSTOMER();
                CUSTOMER.ACTIVE = true;
                CUSTOMER.STATUS = 0;
                CUSTOMER.CREATEDATE = DateTime.Now;
                CUSTOMER.EMAIL = MODEL.EMAIL;
                CUSTOMER.PHONE = MODEL.PHONE_NUMBER;
                CUSTOMER.CUSTOMER_NAME = MODEL.FIRST_NAME + " " + MODEL.LAST_NAME;
                CUSTOMER.CUSTOMER_FIRST_NAME = MODEL.FIRST_NAME;
                CUSTOMER.CUSTOMER_LAST_NAME = MODEL.LAST_NAME;
                CUSTOMER.ADDRESS = MODEL.ADDRESS;
                CUSTOMER.SUBSCRIBE = MODEL.SUBSCRIBE;
                CUSTOMER.SEX = MODEL.SEX;
                Db_gsj.Entry(CUSTOMER).State = EntityState.Added;
                Db_gsj.SaveChanges();
                CUSTOMER.CUSTOMER_CODE = CodeConstants.CUSTOMER_CODE + CUSTOMER.CUSTOMER_CD.ToString();
                Db_gsj.Entry(CUSTOMER).State = EntityState.Modified;
                Db_gsj.SaveChanges();

                // link user and customer
                O_USER_CUSTOMER USER_CUSTOMER = new O_USER_CUSTOMER();
                USER_CUSTOMER.ACTIVE = true;
                USER_CUSTOMER.STATUS = 0;
                USER_CUSTOMER.CREATEDATE = DateTime.Now;
                USER_CUSTOMER.CUSTOMER_CD = CUSTOMER.CUSTOMER_CD; // Tu co???
                USER_CUSTOMER.USER_CD = USER.USER_CD; // Tu co???
                Db_gsj.Entry(USER_CUSTOMER).State = EntityState.Added;
                Db_gsj.SaveChanges();

                // link quyền
                O_USER_PST USER_PST = new O_USER_PST();
                USER_PST.ACTIVE = true;
                USER_PST.STATUS = 0;
                USER_PST.CREATEDATE = DateTime.Now;
                USER_PST.PST_CD = 3;
                USER_PST.USER_CD = USER.USER_CD; 
                Db_gsj.Entry(USER_PST).State = EntityState.Added;
                Db_gsj.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool checkValidation(RegisterModel MODEL, JsonResultRegisterModel jsonModel, GJSEntities Db_gsj)
        {
            if (String.IsNullOrEmpty(MODEL.USER_NAME))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập tên đăng nhập.</li>";
                jsonModel.HasError = true;
            }
            else
            {
                // Username must be not existing.
                S_USER USER_found = Db_gsj.S_USER.FirstOrDefault(t => t.USER_NAME == MODEL.USER_NAME);
                if (USER_found != null)
                {
                    jsonModel.ErrorString += "<li>Tên đăng nhập phải không được trùng.</li>";
                    jsonModel.HasError = true;
                }
            }

            
            if (String.IsNullOrEmpty(MODEL.EMAIL))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập email.</li>";
                jsonModel.HasError = true;
            }
            
            else
            {
                // Kiểm tra dạng email
                if (!Regex.IsMatch(MODEL.EMAIL, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                {
                    jsonModel.ErrorString += "<li>Email không hợp lệ, vui lòng nhập lại email.</li>";
                    jsonModel.HasError = true;
                }
                else
                {
                    // Email must be not existing.
                    O_CUSTOMER CUSTOMER_found = Db_gsj.O_CUSTOMER.FirstOrDefault(t => t.EMAIL == MODEL.EMAIL);
                    if (CUSTOMER_found != null)
                    {
                        jsonModel.ErrorString += "<li>Email phải không được trùng.</li>";
                        jsonModel.HasError = true;
                    }
                }
                
            }

            if (String.IsNullOrEmpty(MODEL.USER_PASS))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập password.</li>";
                jsonModel.HasError = true;
            }
            else
            {
                if (MODEL.USER_PASS.Length < 6)
                {
                    jsonModel.ErrorString += "<li>Mật khẩu phải có từ 6 kí tự trở lên.</li>";
                    jsonModel.HasError = true;
                }
            }

            // Re-password matching.
            if (MODEL.USER_PASS != MODEL.USER_RE_PASS)
            {
                jsonModel.ErrorString += "<li>Mật khẩu nhập lại phải chính xác.</li>";
                jsonModel.HasError = true;
            }

            if (String.IsNullOrEmpty(MODEL.PHONE_NUMBER))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập số điện thoại.</li>";
                jsonModel.HasError = true;
            }
            else
            {
                if (!Regex.IsMatch(MODEL.PHONE_NUMBER, @"^\d+$")) // Is number
                {
                    jsonModel.ErrorString += "<li>Vui lòng nhập số cho số điện thoại.</li>";
                    jsonModel.HasError = true;
                }
            }

            if (String.IsNullOrEmpty(MODEL.ADDRESS))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập địa chỉ.</li>";
                jsonModel.HasError = true;
            }

            if (String.IsNullOrEmpty(MODEL.FIRST_NAME))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập họ.</li>";
                jsonModel.HasError = true;
            }

            if (String.IsNullOrEmpty(MODEL.LAST_NAME))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập tên.</li>";
                jsonModel.HasError = true;
            }

            if (String.IsNullOrEmpty(MODEL.SEX))
            {
                jsonModel.ErrorString += "<li>Vui lòng chọn giới tính.</li>";
                jsonModel.HasError = true;
            }

            return true;
        }
    }
}