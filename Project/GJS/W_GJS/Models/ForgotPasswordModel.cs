using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace W_GJS.Models
{
    public class ForgotPasswordModel
    {
        public string USER_NAME { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Vui lòng nhập đúng Email")]
        [Display(Name = "Email")]
        public string EMAIL { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email nhập lại")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Vui lòng nhập đúng Email")]
        [Compare("Email",ErrorMessage="Email nhập không trùng")]
        [Display(Name = "Email nhập lại")]
        public string EMAIL_COMFIRM { get; set; }
    }
}