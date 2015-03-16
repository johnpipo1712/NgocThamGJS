using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}