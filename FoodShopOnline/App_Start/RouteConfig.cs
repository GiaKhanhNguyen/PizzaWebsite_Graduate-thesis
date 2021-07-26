using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                   name: "dat hang khong dang nhap",
                   url: "order-no-login",
                   defaults: new { controller = "Cart", action = "CustomerNoLogin", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                   name: "dang nhap dat hang",
                   url: "login-for-order",
                   defaults: new { controller = "Cart", action = "LoginForOrder", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                   name: "dat den lay",
                   url: "thong-tin-lay-hang",
                   defaults: new { controller = "Cart", action = "TakeAwayOrder", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                   name: "don hang da mua",
                   url: "don-hang-da-mua",
                   defaults: new { controller = "Order", action = "OldOrder", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                   name: "doi mat khau",
                   url: "doi-mat-khau",
                   defaults: new { controller = "User", action = "ChangePassword", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                   name: "thong tin tai khoan",
                   url: "thong-tin-tai-khoan",
                   defaults: new { controller = "User", action = "InformationAccount", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                   name: "theo doi don hang",
                   url: "theo-doi-don-hang",
                   defaults: new { controller = "Order", action = "FollowOrder", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
               name: "kiem tra don hang",
               url: "kiem-tra-don-hang",
               defaults: new { controller = "Order", action = "CheckOrder", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );
            routes.MapRoute(
               name: "quen mat khau",
               url: "quen-mat-khau",
               defaults: new { controller = "User", action = "ForgotPassword", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
           );
            routes.MapRoute(
                name: "hinh thuc dat hang",
                url: "hinh-thuc-dat-hang",
                defaults: new { controller = "Cart", action = "OrderKind", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );

            routes.MapRoute(
                name: "chi tiet",
                url: "chi-tiet/{alias}-{id}",
                defaults: new { controller = "ProductDetails", action = "MonAn", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );
            routes.MapRoute(
                name: "danh muc san pham",
                url: "san-pham/{alias}-{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );
            routes.MapRoute(
                name: "chuong trinh khuyen mai",
                url: "chuong-trinh-khuyen-mai",
                defaults: new { controller = "Menu", action = "KhuyenMai", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );
            routes.MapRoute(
               name: "dang ky",
               url: "dang-ky",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );
            routes.MapRoute(
                name: "dang nhap",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
             );
            routes.MapRoute(
                name: "gio hang",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                name: "thong tin thanh toan",
                url: "thong-tin-thanh-toan",
                defaults: new { controller = "Cart", action = "MethodPayment", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
             );
            routes.MapRoute(
                name: "thong tin dat hang",
                url: "thong-tin-dat-hang",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
             );
            routes.MapRoute(
                name: "loi dat hang",
                url: "dat-hang-that-bai",
                defaults: new { controller = "Cart", action = "OrderFail", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                name: "Payment Success",
                url: "dat-hang-thanh-cong",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                name: "Not LoggedIn",
                url: "chua-dang-nhap",
                defaults: new { controller = "Cart", action = "NotLoggedIn", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                name: "Lien He",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                name: "Cua Hang",
                url: "cua-hang",
                defaults: new { controller = "Menu", action = "CuaHang", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
            );
            routes.MapRoute(
           name: "AddItem",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }, namespaces: new[] { "FoodShopOnline.Controllers" }
        );

        }
    }
}
