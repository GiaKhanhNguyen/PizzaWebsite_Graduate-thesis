using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodShopOnline.Models
{
    [Serializable]
    public class PaymentModel
    {
        [Key]
        [Required(ErrorMessage = "Yêu cầu nhập Họ Tên")]
        public string CustomerName { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CustomerEmail { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Địa chỉ")]
        public string CustomerAddress { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Điện Thoại không tồn tại")]
        public string CustomerMobie { set; get; }

        public string City { set; get; }
        public string District { set; get; }
        public string Xa { set; get; }
        public string NumberHome { set; get; }
        public string NameRoad { set; get; }
    }
}