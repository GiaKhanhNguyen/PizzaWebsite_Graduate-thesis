using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace FoodShopOnline.Models
{
    public class ConfirmKey
    {
        [Key]
        [Display(Name ="Nhập mã xác thực")]
        public string Key { get; set; }

        public string BodyMail(string Email, string Name, string key, string userName)
        {
            StringBuilder mailbody = new StringBuilder();
            mailbody.Append("<html><head><title>Tạo mới mật khẩu</title></head>");
            mailbody.Append("<body>");
            mailbody.Append("<p>Chào bạn</p>: " + Name);
            mailbody.Append("<p>Thông tin tài khoản của bạn: </p>");
            mailbody.Append("<p><b>Tên đăng nhập: </b>" + userName);
            mailbody.Append("</p><p><b>Email: </b>" + Email);
            mailbody.Append("</p><p><b>Mã xác thực tài khoản: </b>" + key);
            mailbody.Append("<p> Chân thành cảm ơn!");
            return mailbody.ToString();
        }
    }
}