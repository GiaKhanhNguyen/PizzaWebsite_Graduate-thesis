using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class ProductDetailDAO
    {
        FoodShopOnlineDBContext db = null;
        public ProductDetailDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        public List<Product> ViewDetailProduct(int id)
        {
            return db.Products.Where(x => x.ID == id).ToList();
        }
    }
}
