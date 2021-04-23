using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: Admin/Report
        public ActionResult Index()
        {
            ViewBag.Benefit = new OrderDAO().ReportBenefit();
            ViewBag.Product = new OrderDAO().ReportProduct();
            ViewBag.Order = new OrderDAO().ReportOrder();
            return View();
        }
    }
}