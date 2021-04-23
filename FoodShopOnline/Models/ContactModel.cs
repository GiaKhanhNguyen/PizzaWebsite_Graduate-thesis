using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodShopOnline.Models
{
   
    public class ContactModel
    {
        [Key]
        [Required(ErrorMessage = "Yêu cầu nhập Họ Tên")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Địa chỉ")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Điện Thoại không tồn tại")]
        public string Phone { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Tiêu Đề")]
        public string Title { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Nội Dung")]
        public string Content { set; get; }
    }
}