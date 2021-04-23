using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pagesize = 10)
        {
            var ProModel = new ProductDAO();
            var model = ProModel.ListAllPro(page, pagesize, searchString);
            return View(model);
        }
        //hàm hiện thị trang sản phẩm sao khi thêm mới
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        //hàm thêm mới sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product collection)
        {
            try
            {
                var product = new ProductDAO();
                bool k = product.Create(collection);
                if (k != false)
                {
                    SetAlert("Thêm sản phẩm thành công", "success");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
                return View(collection);
            }
            catch
            {
                ModelState.AddModelError("", "Xin điền đủ thông tin sản phẩm");
                return View();
            }
        }
        //hàm sửa thông tin sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken] //Khi form Admin được Post hiển thị lên
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var Func = new ProductDAO();
                var result = Func.Update(product);
                if (result)
                {
                    SetAlert("Cập nhật thông tin sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin sản phẩm thất bại");
                }
            }
            return View("Index");
        }
        public ActionResult Edit(int id)
        {
            var product = new ProductDAO().ViewDetailProduct(id);
            return View(product);
        }
        //
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDAO().DeleteProduct(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}