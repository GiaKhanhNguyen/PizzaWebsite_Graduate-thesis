using FoodShopOnline.Common;
using FoodShopOnline.Models;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [HttpGet]
        public ActionResult CheckOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOrder(int? id)
        {
            bool check = new OrderDAO().CheckExistOrder(id);
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    if (check == true)
                    {
                        Session["OrderID"] = id;
                        return Redirect("/theo-doi-don-hang");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đơn hàng không tồn tại");
                        //TempData["AlertMessage"] = "Đơn hàng không tồn tại hihi";
                        //TempData["AlertType"] = "alert-danger";
                        //TempData["CheckOrder"] = "alert('Đơn hàng không tồn tại')";
                    }
                }
            }
            return Redirect("/kiem-tra-don-hang");
        }

        public ActionResult FollowOrder()
        {
            int id = (int)Session["OrderID"];
            ViewBag.Order = new OrderDAO().GetOrderByID(id);
            var model = new OrderDAO().GetOrderDetailByID(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult OldOrder()
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            var model = new OrderDAO().GetOldOrder(session.UserID);
            ViewBag.ListOrder = new OrderDAO().ListOrder();
            return View(model);
        }
        [HttpPost]
        public ActionResult OldOrder(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new OrderDAO().GetOrderDetailByID(id);
                //tạo mới đối tượng trong cart item

                var list = new List<CartItem>();
                foreach (var subitem in model)
                {
                    var item = new CartItem();
                    item.Product = subitem.Product;
                    item.Quantity = subitem.OrderDetail.Quantity;
                    item.Dough = subitem.OrderDetail.Dough;
                    item.Size = subitem.OrderDetail.Size;
                    list.Add(item);
                    //gắn vào session
                    Session[CommonConstants.CartSession] = list;
                }

                return Redirect("/gio-hang");
            }
            return View();
        }
    }
}