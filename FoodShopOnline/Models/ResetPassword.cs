using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodShopOnline.Models
{
    public class ResetPassword
    {
        [Key]

        [Display(Name ="Mật khẩu mới")]
        [Required]
        public string NewPassword { get; set; }

        [Display(Name ="Xác nhận mật khẩu")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}