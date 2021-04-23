using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        // GET: Admin/Contact
        public ActionResult Index()
        {
            var model = new ContactDAO().GetFeedBack();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            new ContactDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}