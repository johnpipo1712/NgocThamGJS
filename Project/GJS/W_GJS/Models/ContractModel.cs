using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace W_GJS.Models
{
    public class ContractModel
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        public static bool checkValidation(ContractModel MODEL, JsonResultContactModel jsonModel, GJSEntities Db_gsj)
        {
            if (String.IsNullOrEmpty(MODEL.Name))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập họ tên.</li>";
                jsonModel.HasError = true;
            }

            if (String.IsNullOrEmpty(MODEL.Company))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập công ty.</li>";
                jsonModel.HasError = true;
            }

            

            if (String.IsNullOrEmpty(MODEL.Address))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập địa chỉ.</li>";
                jsonModel.HasError = true;
            }

            if (String.IsNullOrEmpty(MODEL.Phone))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập số điện thoại.</li>";
                jsonModel.HasError = true;
            }
            else
            {
                if (!Regex.IsMatch(MODEL.Phone, @"^\d+$")) // Is number
                {
                    jsonModel.ErrorString += "<li>Vui lòng nhập số cho số điện thoại.</li>";
                    jsonModel.HasError = true;
                }
            }

            if (String.IsNullOrEmpty(MODEL.Email))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập email.</li>";
                jsonModel.HasError = true;
            }
            else
            {
                // Kiểm tra dạng email
                if (!Regex.IsMatch(MODEL.Email, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                {
                    jsonModel.ErrorString += "<li>Email không hợp lệ, vui lòng nhập lại email.</li>";
                    jsonModel.HasError = true;
                }
            }

            if (String.IsNullOrEmpty(MODEL.Note))
            {
                jsonModel.ErrorString += "<li>Vui lòng nhập ghi chú.</li>";
                jsonModel.HasError = true;
            }

            return true;
        }
    }
}