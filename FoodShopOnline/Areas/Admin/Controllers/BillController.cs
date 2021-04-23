using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class BillController : BaseController
    {
        private FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        // GET: Admin/Bill
        [HttpGet]
        public ActionResult Index()
        {
            var model = new OrderDAO().ListOrderDelivery();
            return View(model);
        }
        [HttpGet]
        public ActionResult ListOrderTakeAway()
        {
            var model = new OrderDAO().ListOrderTakeAway();
            return View(model);
        }
        [HttpGet]
        public ActionResult OrderDetail(int id)
        {
            var model = new OrderDAO().GetOrderDetailByID(id);
            return View(model);
        }
        //Xác nhận đơn hàng: Đặt giao hàng
        [HttpGet]
        public ActionResult XacNhanHoaDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = true;
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Bill");
        }
        [HttpGet]
        public ActionResult XacNhanGiaoHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.PaymentStatus = "Đã Thanh Toán";
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Bill");
        }
        //////////////////////////////////////////
        ///// Xác nhận đơn hàng: Đặt đến lấy
        [HttpGet]
        public ActionResult XacNhan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = true;
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListOrderTakeAway", "Bill");
        }
        [HttpGet]
        public ActionResult XacNhanNhanHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.PaymentStatus = "Đã Thanh Toán";
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListOrderTakeAway", "Bill");
        }
        public ActionResult DeleteTakeOrder(int id)
        {
            new OrderDAO().DeleteOrder(id);
            return RedirectToAction("ListOrderTakeAway", "Bill");
        }
        public ActionResult DeleteDeliveryOrder(int id)
        {
            new OrderDAO().DeleteOrder(id);
            return RedirectToAction("Index", "Bill");
        }
        //public ActionResult Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Admin/HoaDons/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.OrderDetails.RemoveRange(order.OrderDetails);
        //    db.SaveChanges();
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}