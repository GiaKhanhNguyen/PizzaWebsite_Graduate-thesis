using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class ProductCategoryDAO
    {
        FoodShopOnlineDBContext db = null;
        public ProductCategoryDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        //hiển thị dữ liệu danh mục sản phẩm
        public List<ProductCategory> ListAllProCate()
        {
            return db.ProductCategories.OrderBy(x => x.ID).ToList();
        }
        public ProductCategory ViewDetail(int id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Create(ProductCategory model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Update(ProductCategory entity)
        {

            try
            {
                var productCategory = db.ProductCategories.Find(entity.ID);
                productCategory.Name = entity.Name;
                productCategory.Alias = entity.Alias;
                productCategory.DisplayOrder = entity.DisplayOrder;
                productCategory.Icon = entity.Icon;
                productCategory.SeoTitile = entity.SeoTitile;
                productCategory.ModifiedDate = DateTime.Now;
                productCategory.ModifiedBy = entity.ModifiedBy;
                productCategory.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ChangeStatus(int id)
        {
            var productCategory = db.ProductCategories.Find(id);
            productCategory.Status = !productCategory.Status;
            db.SaveChanges();
            return productCategory.Status;
        }

        public void Delete(int id)
        {
            var productCategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productCategory);
            db.SaveChanges();
        }
    }
}
