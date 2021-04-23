using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class SystemController : BaseController
    {
        // GET: Admin/System
        public ActionResult MenuGroups()
        {
            var model = new MenuDAO().ListMenuGroup();
            return View(model);
        }

        public ActionResult Menu()
        {
            var model = new MenuDAO().ListMenu();
            return View(model);
        }

        public ActionResult Slide()
        {
            var model = new SlideDAO().ListAllSlide();
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateSlide()
        {
            return View();
        }
        //hàm thêm mới slide
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSlide(Slide collection)
        {
            try
            {
                bool k = new SlideDAO().Create(collection);
                if (k != false)
                {
                    SetAlert("Thêm Slide thành công", "success");
                }
                else
                {
                    return RedirectToAction("Slide");
                }
                return Redirect("Slide");
            }
            catch
            {
                ModelState.AddModelError("", "Xin điền đủ thông tin");
                return View();
            }
        }

        //hàm sửa slide
        [HttpPost]
        [ValidateAntiForgeryToken] //Khi form Admin được Post hiển thị lên
        public ActionResult EditSlide(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var Func = new SlideDAO();
                var result = Func.EditSlide(slide);
                if (result)
                {
                    SetAlert("Cập nhật thông tin slide thành công", "success");
                    return RedirectToAction("Slide", "System");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin slide thất bại");
                }
            }
            return View("Slide");
        }
        public ActionResult EditSlide(int id)
        {
            var slide = new SlideDAO().ViewDetailSlide(id);
            return View(slide);
        }

        public ActionResult DeleteSilde(int id)
        {
            new SlideDAO().DeleteSlide(id);
            return RedirectToAction("Slide", "System");
        }
    }
}