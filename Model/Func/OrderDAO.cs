
using Model.EnityFramework;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class OrderDAO
    {
        FoodShopOnlineDBContext db = null;
        // SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=FoodShopOnline;" + "Integrated Security=True");
        public OrderDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        //hàm order sản phẩm vào database
        public void Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            //return order.ID;
        }

        public void InsertCusNoLo(OrderNoLogin orderNoLogin)
        {
            db.OrderNoLogins.Add(orderNoLogin);
            db.SaveChanges();
            //return order.ID;
        }
        public List<Order> ListOrder()
        {
            return db.Orders.OrderByDescending(x=>x.ID).ToList();
        }
        public List<Order> ListOrderTakeAway()
        {
            return db.Orders.OrderBy(x => x.ID).Where(y=>y.OrderMethod == "Đặt đến lấy").ToList();
        }
        public List<Order> ListOrderDelivery()
        {
            return db.Orders.OrderBy(x => x.ID).Where(y=>y.OrderMethod == "Đặt giao hàng").ToList();
        }

        public List<OrderViewModel> GetOrderDetailByID(int id)
        {
            var data = (from a in db.Products join b in db.OrderDetails
                        on a.ID equals b.ProductID
                        where b.OrderID == id
                        select new OrderViewModel()
                        {
                            Product = a,
                            OrderDetail = b
                        });
            return data.ToList();
        }
        public bool CheckExistOrder(int? id)
        {
            var order = db.Orders.Find(id);
            if(order != null)
            {
                return true;
            }
            return false;
        }

        public Order GetOrderByID(int id)
        {
            var model = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            return model;
        }

        public List<OldOrderViewModel> GetOldOrder(int id)
        {
            var oldOrder = (from a in db.Orders join b in db.OrderDetails 
                            on a.ID equals b.OrderID join c in db.Products
                            on b.ProductID equals c.ID
                            where a.CustomerID == id
                            select new OldOrderViewModel()
                            {
                                Order = a,
                                OrderDetail = b,
                                Product = c
                            });
            return oldOrder.OrderByDescending(x=>x.Order.ID).ToList();
        }
        public List<ReportBenefit> ReportBenefit()
        {
            var list = db.Database.SqlQuery<ReportBenefit>("SP_ReportBenifit").ToList();
            return list;
        }

        public List<ReportProduct> ReportProduct()
        {
            var list = db.Database.SqlQuery<ReportProduct>("SP_ReportProduct").ToList();
            return list;
        }

        public List<ReportOrder> ReportOrder()
        {
            var list = db.Database.SqlQuery<ReportOrder>("SP_ReportOrder").OrderByDescending(x => x.YearOrder).ToList();
            return list;
        }
        
        public void DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            db.OrderDetails.RemoveRange(order.OrderDetails);
            db.SaveChanges();
            db.Orders.Remove(order);
            db.SaveChanges();
        }
    }
}
