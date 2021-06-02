using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodShopOnline.Models;
using Model.EnityFramework;
using System.IO;
using FoodShopOnline.Common;

namespace FoodShopOnline.Controllers
{
    public class HomeController : Controller
    {
        private FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        
        // GET: Home
        [HttpGet]
        public ActionResult Index(string de, string size)
        {
            var model = db.Products.OrderBy(s => s.ID).Take(4);
            // ViewBag.monan = monan;
            ViewBag.Slides = new SlideDAO().ListAllSlide();
            ViewBag.category = new ProductCategoryDAO().ListAllProCate();
            ViewBag.KM = db.Products.Where(x => x.CategoryID == 8).OrderBy(y => y.ID).ToList();
            ViewBag.SP = db.Products.OrderBy(k => k.ID).ToList();
            ViewBag.KhaiVi = db.Products.OrderByDescending(s => s.ID).Where(x=>x.CategoryID == 2).Take(4);
            ViewBag.MyY = db.Products.OrderByDescending(s => s.ID).Where(x => x.CategoryID == 3).Take(4);
            ViewBag.Com = db.Products.OrderByDescending(s => s.ID).Where(x => x.CategoryID == 4).Take(4);
            ViewBag.Salad = db.Products.OrderByDescending(s => s.ID).Where(x => x.CategoryID == 5).Take(4);
            ViewBag.ThucUong = db.Products.OrderByDescending(s => s.ID).Where(x => x.CategoryID == 6).Take(4);

            if(de == "day")
            {
                Session[CommonConstants.DoughKind] = "Dày xốp";
            }
            else if (de == "mong")
            {
                Session[CommonConstants.DoughKind] = "vừa";
            }
            else
            {                
                Session[CommonConstants.DoughKind] = "Mỏng giòn";
            }
            if (size == "7")
            {
                Session[CommonConstants.SizeProduct] = "Nhỏ 7''";
            }
            else if (size == "9")
            {
                Session[CommonConstants.SizeProduct] = "Lớn 12''";
            }
            else
            {       
                Session[CommonConstants.SizeProduct] = "Vừa 9''";
            }
            return View(model);

        }
        //RenderAction thanh Menu trong trang chủ --- (_Layout)
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDAO().ListByGroupID(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDAO().ListByGroupID(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDAO().GetFooter();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Chat()
        {
            return PartialView();
        }
        public ActionResult Search(string search)
        {
            List<Product> list = new List<Product>();
            ViewBag.Search = search;
            if (ModelState.IsValid)
            {
                var product = new ProductDAO().SearchProduct(search);
                list = product;  
            }
            return View(list);
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoadMoreProduct(int size)
        {
            var model = db.Products.Where(x=>x.CategoryID == 1).OrderBy(p => p.ID).Skip(size).Take(4);
            int modelCount = db.Products.Where(x=>x.CategoryID == 1).Count();
            if (model.Any())
            {
                string modelString = RenderRazorViewToString("LoadMoreProduct", model);
                return Json(new { ModelString = modelString, ModelCount = modelCount });
            }
            return Json(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoadMoreKhaiVi(int size)
        {
            var model = db.Products.Where(x=>x.CategoryID == 2).OrderByDescending(p => p.ID).Skip(size).Take(4);
            int modelCount = db.Products.Where(x=>x.CategoryID == 2).Count();
            if (model.Any())
            {
                string modelString = RenderRazorViewToString("LoadMoreKhaiVi", model);
                return Json(new { ModelString = modelString, ModelCount = modelCount });
            }
            return Json(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoadMoreMyY(int size)
        {
            var model = db.Products.Where(x => x.CategoryID == 3).OrderByDescending(p => p.ID).Skip(size).Take(4);
            int modelCount = db.Products.Where(x => x.CategoryID == 3).Count();
            if (model.Any())
            {
                string modelString = RenderRazorViewToString("LoadMoreMyY", model);
                return Json(new { ModelString = modelString, ModelCount = modelCount });
            }
            return Json(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoadMoreCom(int size)
        {
            var model = db.Products.Where(x => x.CategoryID == 4).OrderByDescending(p => p.ID).Skip(size).Take(4);
            int modelCount = db.Products.Where(x => x.CategoryID == 4).Count();
            if (model.Any())
            {
                string modelString = RenderRazorViewToString("LoadMoreCom", model);
                return Json(new { ModelString = modelString, ModelCount = modelCount });
            }
            return Json(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoadMoreSalad(int size)
        {
            var model = db.Products.Where(x => x.CategoryID == 5).OrderByDescending(p => p.ID).Skip(size).Take(4);
            int modelCount = db.Products.Where(x => x.CategoryID == 5).Count();
            if (model.Any())
            {
                string modelString = RenderRazorViewToString("LoadMoreSalad", model);
                return Json(new { ModelString = modelString, ModelCount = modelCount });
            }
            return Json(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoadMoreThucUong(int size)
        {
            var model = db.Products.Where(x => x.CategoryID == 6).OrderByDescending(p => p.ID).Skip(size).Take(4);
            int modelCount = db.Products.Where(x => x.CategoryID == 6).Count();
            if (model.Any())
            {
                string modelString = RenderRazorViewToString("LoadMoreThucUong", model);
                return Json(new { ModelString = modelString, ModelCount = modelCount });
            }
            return Json(model);
        }
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext =
                     new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}