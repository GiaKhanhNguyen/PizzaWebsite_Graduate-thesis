using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EnityFramework;
using Model.Func;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var ProCate = new ProductCategoryDAO();
            var model = ProCate.ListAllProCate();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        //hàm thêm mới danh muc sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory collection)
        {
            try
            {
                bool k = new ProductCategoryDAO().Create(collection);
                if (k != false)
                {
                    SetAlert("Thêm danh mục sản phẩm thành công", "success");
                }
                else
                {
                    return RedirectToAction("Index");
                }

                return View(collection);
            }
            catch
            {
                ModelState.AddModelError("", "Xin điền đủ thông tin danh mục sản phẩm");
                return View();
            }
        }

        //hàm sửa thông tin sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken] //Khi form Admin được Post hiển thị lên
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var result = new ProductCategoryDAO().Update(productCategory);
                if (result)
                {
                    SetAlert("Cập nhật thông tin danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin danh mục sản phẩm thất bại");
                }
            }
            return View("Index");
        }
        public ActionResult Edit(int id)
        {
            var productCategory = new ProductCategoryDAO().ViewDetail(id);
            return View(productCategory);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductCategoryDAO().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new ProductCategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}