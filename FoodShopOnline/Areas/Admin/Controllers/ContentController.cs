using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Content content)
        {


            return View();
        }
        public void SetViewBag(int? selectId = null)
        {
            var fun = new CategoryContentDAO();
            ViewBag.CategoryID = new SelectList(fun.ListAll(), "ID", "Name", selectId);
        }
    }
}