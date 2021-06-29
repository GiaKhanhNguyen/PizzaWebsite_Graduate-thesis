using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodShopOnline.Common;
using Model.Func;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ChatController : Controller
    {
        // GET: Admin/Chat
        public ActionResult Chat()
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            var model = new UserDAO().ListUserChat();
            ViewBag.Time = DateTime.Now.ToString("hh:mm tt");
            if (session == null)
            {
                ViewBag.CurrentUserAdmin = "";
            }
            else
            {
                ViewBag.CurrentUserAdmin = session.UserName;
            }
            return View(model);
        }
    }
}