using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EnityFramework;
using System.Web.Mvc;
using Model.Func;

namespace FoodShopOnline.Controllers
{
    public class ProductDetailsController : Controller
    {
        // GET: ProductDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MonAn(int id)
        {
            var monan = new ProductDAO().GetProductDetail(id);
            ViewBag.monan = monan;
            ViewBag.RelateProducts = new ProductDAO().ListRelateProducts(id);
            return View();
        }

        public ActionResult AddtoCart(int id)
        {
            return View();
        }
     
    }
}