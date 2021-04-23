using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class CategoryController : Controller
    {
        private FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        // GET: Category
        public ActionResult Index(int id)
        {
            var category = new ProductCategoryDAO().ViewDetail(id);
            ViewBag.Category = category;
            var model = new ProductDAO().ListByCategoryID(id);
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDAO().ListAllProCate();
            return PartialView(model);
        }

        public ActionResult ChiTiet(int id)
        {
            //var sp = db.Products.Where(x => x.ID == int.Parse(id));
            //return View(sp);
            var model = new ProductDetailDAO().ViewDetailProduct(id);
            return View(model);
        }
        //Category Khuyến mãi
        //[HttpGet]
        //public ActionResult KhuyenMai(string sort)
        //{
        //    List<Product> list = new List<Product>();
        //    if (sort == "1" || sort == null)
        //    {
        //        list = db.Products.Where(x => x.CategoryID == 5).OrderBy(s => s.Price).ToList();
        //    }
        //    else if (sort == "-1")
        //    {
        //        list = db.Products.Where(x => x.CategoryID == 5).OrderByDescending(s => s.Price).ToList();
        //    }

        //    return View(list);
        //}
    }
}