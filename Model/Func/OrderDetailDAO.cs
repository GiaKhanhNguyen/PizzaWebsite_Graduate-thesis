using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class OrderDetailDAO
    {
        FoodShopOnlineDBContext db = null;
        public OrderDetailDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool InsertOrderNoLogin(OrderDetailsNoLogin detail)
        {
            try
            {
                db.OrderDetailsNoLogins.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //hàm hiển thị chi tiết đơn hàng sử dụng Stored Procedure
        public List<OrderDetail> ListOrderDetail()
        {
            //var list = db.Database.SqlQuery<OrderDetail>("SP_OrderDetail_ListAll").ToList();
            //return list;
            return db.OrderDetails.OrderBy(x => x.OrderID).ToList();
        }
    }
}
