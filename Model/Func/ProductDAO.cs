using Model.EnityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class ProductDAO
    {
        FoodShopOnlineDBContext db = null;
        public ProductDAO()
        {
            db = new FoodShopOnlineDBContext();
        }
        //CÁC HÀM THỰC HIỆN THAO TÁC THÊM, XÓA, SỬA CỦA TRANG SẢN PHẨM
        //hiển thị dữ liệu sản phẩm
        public IEnumerable<Product> ListAllPro(int page, int pagesize, string searchString)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Alias.Contains(searchString) || x.Description.Contains(searchString) || x.CategoryID.ToString().Contains(searchString));

            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pagesize);
        }
        //hàm lấy ra danh sách sản phẩm theo từng danh mục
        public List<Product> ListByCategoryID(int cateID)
        {
            return db.Products.Where(x => x.CategoryID == cateID).ToList();
        }
        //hàm thêm mới sản phẩm sử dụng Stored Procedure
        public bool Create(Product model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.Status = true;
                db.Products.Add(model);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }
        //hảm update sản phẩm sử dụng Stored Procedure
        public bool Update(Product entity)
        {
            
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name =  entity.Name;
                product.Alias = entity.Alias;
                product.CategoryID =  entity.CategoryID;
                product.Image =  entity.Image;
                product.Price = entity.Price;
                product.OriginalPrice = entity.OriginalPrice;
                product.Description = entity.Description;
                product.ModifiedDate = DateTime.Now;
                product.ModifiedBy = entity.ModifiedBy;
                //product.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
 
        }
        //hàm lấy chi tiết bản ghi thông tin sản phẩm
        public Product ViewDetailProduct(int id)
        {
            return db.Products.Find(id);
        }
        //hàm xóa thông tin một sản phẩm
        public void DeleteProduct(int id)
        {
            //var product = db.Products.Find(id);
            //db.Products.Remove(product);
            //db.SaveChanges();
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
        }
        public bool ChangeStatus(int id)
        {
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }
        //Hàm lấy danh sách khuyến mãi 
        public List<Product> ListKhuyenMai()
        {
            return db.Products.Where(x => x.CategoryID == 8).ToList();
        }

        public Product GetProductDetail(int id)
        {
            return db.Products.Where(s => s.ID == id).SingleOrDefault();
        }

        public List<Product> SearchProduct(string search)
        {
            return db.Products.Where(s => s.Name.Contains(search) || s.Description.Contains(search)).ToList();
        }

        public List<Product> ListRelateProducts(int id)
        {
            var product = db.Products.Find(id);
            return db.Products.Where(x =>x.ID != id && x.CategoryID == product.CategoryID).Take(4).ToList();
        }
    }
}
