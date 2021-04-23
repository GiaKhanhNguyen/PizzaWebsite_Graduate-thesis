using FoodShopOnline.Models;
using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDAO().GetActiveContact();
            return View(model);
        }

        [HttpPost]
        public JsonResult Send(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new FeedBack();
                feedback.Name = model.Name;
                feedback.Address = model.Address;
                feedback.Email = model.Email;
                feedback.CreatedDate = DateTime.Now;
                feedback.Phone = model.Phone;
                feedback.Title = model.Title;
                feedback.Content = model.Content;
                feedback.Status = true;

                var id = new ContactDAO().InsertFeeBack(feedback);
                if (id > 0)
                {
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            return Json(new
            {
                status = false
            });
        }
    }
}