using FoodShopOnline.Models;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.EnityFramework;
using FoodShopOnline.Common;
using Microsoft.Ajax.Utilities;
using PayPal.Api;

namespace FoodShopOnline.Controllers
{
    public class CartController : Controller
    {
        private Payment payment;
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDAO().ViewDetailProduct(productId);
            var cart = Session[CommonConstants.CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng trong cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    item.Dough = Session[CommonConstants.DoughKind].ToString(); 
                    item.Size = Session[CommonConstants.SizeProduct].ToString();
                    list.Add(item);
                }
                //Gán vào session
                Session[CommonConstants.CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng trong cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                item.Dough = Session[CommonConstants.DoughKind].ToString();
                item.Size = Session[CommonConstants.SizeProduct].ToString();
                var list = new List<CartItem>();
                list.Add(item);
                //gắn vào session
                Session[CommonConstants.CartSession] = list;
            }
            //return Json(new { Product.ID = productId }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        //Phương thức giao hàng "đặt giao hàng"
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel model)
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            if (model != null)
            {
                model.CustomerAddress = model.NumberHome + "," + model.NameRoad + "," + model.Xa + "," + model.District + "," + model.City;
                Session[CommonConstants.OrderSession] = model;
                return Redirect("/thong-tin-thanh-toan");
            }
            return View();
        }
        //Phương thức giao hàng "đặt đến lấy"
        [HttpGet]
        public ActionResult TakeAwayOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TakeAwayOrder(PaymentModel model, string takeaway)
        {
            if (model != null)
            {
                if (takeaway == "1")
                {
                    model.CustomerAddress = "273 An Dương Vương, Quận 5, Hồ Chí Minh";
                }
                else if (takeaway == "2")
                {
                    model.CustomerAddress = "105 Bà Huyện Thanh Quan, Quận 3, Hồ Chí Minh";
                }
                else if (takeaway == "3")
                {
                    model.CustomerAddress = "04 Tôn Đức Thắng, Quận 1, Hồ Chí Minh";
                }
                else
                {
                    model.CustomerAddress = "20 Ngô Thời Nhiệm, Quận 3, Hồ Chí Minh";
                }
                Session[CommonConstants.OrderSession] = model;
                return Redirect("/thong-tin-thanh-toan");
            }
            return View();
        }
        //Chọn phương thức đặt hàng
        [HttpGet]
        public ActionResult OrderKind()
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            // Session[CommonConstants.OrderLogin] = "OrderLogin";
            //kiểm tra chuyển hướng khi chưa đăng nhập của 2 phương thức đặt hàng
            return View();
        }
        [HttpPost]
        public ActionResult OrderKind(string orderKind)
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            if (session != null)
            {
                if (orderKind == "DatLay")
                {
                    Session[CommonConstants.OrderKind] = orderKind;
                    return Redirect("/thong-tin-lay-hang");
                }
                else
                {
                    Session[CommonConstants.OrderKind] = orderKind;
                    return Redirect("/thong-tin-dat-hang");
                }
            }
            else
            {
                if (orderKind == "DatLay")
                {
                    Session[CommonConstants.OrderKind] = orderKind;
                    return Redirect("/login-for-order");
                }
                else
                {
                    Session[CommonConstants.OrderKind] = orderKind;
                    return Redirect("/login-for-order");
                }
            }
        }
        //Chọn phương thức thanh toán và hoàn tất quá trình đặt hàng
        [HttpGet]
        public ActionResult MethodPayment()
        {
            var cartDetail = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cartDetail != null)
            {
                list = (List<CartItem>)cartDetail;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult MethodPayment(string paymentMethod)
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            var model = new PaymentModel();
            model = (PaymentModel)Session[CommonConstants.OrderSession];
            string orderKind = Session[CommonConstants.OrderKind].ToString();
            //Đặt hàng cần đăng nhập - khách hàng thường xuyên
            if (session != null)
            {
                var order = new Model.EnityFramework.Order();
                order.CreatedDate = DateTime.Now;
                order.CustomerID = session.UserID;
                order.CustomerName = model.CustomerName;
                order.CustomerEmail = model.CustomerEmail;
                order.CustomerMobile = model.CustomerMobie;
                order.CustomerAddress = model.CustomerAddress;
                order.CreatedBy = "CUSTOMER";

                if (orderKind == "DatLay")
                {
                    order.OrderMethod = "Đặt Đến Lấy";
                }
                else
                {
                    order.OrderMethod = "Đặt Giao Hàng";
                }
                if (paymentMethod == "0")
                {
                    order.PaymentMethod = "Thanh Toán Bằng Tiền Mặt Khi Nhận Hàng";
                }
                else
                {
                    order.PaymentMethod = "Thanh Toán qua PayPal";
                    return RedirectToAction("PaymentWithPayPal", "Cart");
                }
                order.Status = false;
                order.PaymentStatus = "Chưa Thanh Toán";

                try
                {
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    var detail = new OrderDetailDAO();
                    decimal total = 0;

                    foreach (var item in cart)
                    {
                        total += (item.Product.Price * item.Quantity);
                    }
                    order.Total = total;
                    new OrderDAO().Insert(order);
                    total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = order.ID;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Dough = item.Dough;
                        orderDetail.Size = item.Size;
                        detail.Insert(orderDetail);

                        total += (item.Product.Price * item.Quantity);
                    }

                    // model.CustomerAddress = model.CustomerAddress;

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/content/client/template/neworder.html"));

                    content = content.Replace("{{OrderID}}", order.ID.ToString());
                    content = content.Replace("{{CustomerName}}", model.CustomerName);
                    content = content.Replace("{{Phone}}", model.CustomerMobie);
                    content = content.Replace("{{Address}}", model.CustomerAddress);
                    content = content.Replace("{{Email}}", order.CustomerEmail);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    content = content.Replace("{{OrderMethod}}", order.OrderMethod);

                    new MailHelper().SendMail(order.CustomerEmail, "Đơn Hàng Pizza", content);

                    Session["OrderID"] = order.ID;
                }
                catch (Exception)
                {
                    return Redirect("/dat-hang-that-bai");
                }
                return Redirect("/dat-hang-thanh-cong");
            }
            //đặt hàng không đăng nhập - khách hàng không thường xuyên
            else
            {
                var orderNoLogin = new OrderNoLogin();
                orderNoLogin.CreatedDate = DateTime.Now;
                orderNoLogin.CustomerName = model.CustomerName;
                orderNoLogin.CustomerEmail = model.CustomerEmail;
                orderNoLogin.CustomerMobile = model.CustomerMobie;
                orderNoLogin.CustomerAddress = model.CustomerAddress;
                orderNoLogin.CreatedBy = "CUSTOMERNOLOGIN";

                if (orderKind == "DatLay")
                {
                    orderNoLogin.OrderMethod = "Đặt Đến Lấy";
                }
                else
                {
                    orderNoLogin.OrderMethod = "Đặt Giao Hàng";
                }
                if (paymentMethod == "0")
                {
                    orderNoLogin.PaymentMethod = "Thanh Toán Bằng Tiền Mặt Khi Nhận Hàng";
                }
                else
                {
                    orderNoLogin.PaymentMethod = "Thanh Toán qua PayPal";
                    return RedirectToAction("PaymentWithPayPal", "Cart");
                }
                orderNoLogin.Status = false;
                orderNoLogin.PaymentStatus = "Chưa Thanh Toán";

                try
                {
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    var detail = new OrderDetailDAO();
                    decimal total = 0;

                    foreach (var item in cart)
                    {
                        total += (item.Product.Price * item.Quantity);
                    }
                    orderNoLogin.Total = total;
                    new OrderDAO().InsertCusNoLo(orderNoLogin);
                    total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetailsNoLogin();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = orderNoLogin.ID;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Dough = item.Dough;
                        orderDetail.Size = item.Size;
                        detail.InsertOrderNoLogin(orderDetail);

                        total += (item.Product.Price * item.Quantity);
                    }
                    model.CustomerAddress = model.CustomerAddress;

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/content/client/template/neworder.html"));

                    content = content.Replace("{{OrderID}}", orderNoLogin.ID.ToString());
                    content = content.Replace("{{CustomerName}}", model.CustomerName);
                    content = content.Replace("{{Phone}}", model.CustomerMobie);
                    content = content.Replace("{{Address}}", model.CustomerAddress);
                    content = content.Replace("{{Email}}", orderNoLogin.CustomerEmail);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    content = content.Replace("{{OrderMethod}}", orderNoLogin.OrderMethod);

                    new MailHelper().SendMail(orderNoLogin.CustomerEmail, "Đơn Hàng Pizza", content);

                    Session["OrderID"] = orderNoLogin.ID;
                }
                catch (Exception)
                {
                    return Redirect("/dat-hang-that-bai");
                }
                return Redirect("/dat-hang-thanh-cong");
            }
        }

        public ActionResult PaymentWithPayPal()
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            var model = new PaymentModel();
            model = (PaymentModel)Session[CommonConstants.OrderSession];
            string orderKind = Session[CommonConstants.OrderKind].ToString();
            //Đặt hàng cần đăng nhập - khách hàng thường xuyên
            if (session != null)
            {
                var order = new Model.EnityFramework.Order();
                order.CreatedDate = DateTime.Now;
                order.CustomerID = session.UserID;
                order.CustomerName = model.CustomerName;
                order.CustomerEmail = model.CustomerEmail;
                order.CustomerMobile = model.CustomerMobie;
                order.CustomerAddress = model.CustomerAddress;
                order.CreatedBy = "CUSTOMER";

                if (orderKind == "DatLay")
                {
                    order.OrderMethod = "Đặt Đến Lấy";
                }
                else
                {
                    order.OrderMethod = "Đặt Giao Hàng";
                }
                order.PaymentMethod = "Thanh Toán qua PayPal";
                APIContext apiContext = Configuration.GetAPIContext();

                try
                {
                    string payerId = Request.Params["PayerID"];

                    if (string.IsNullOrEmpty(payerId))
                    {
                        string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cart/PaymentWithPayPal?";
                        var guid = Convert.ToString((new Random()).Next(100000));
                        var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                        var links = createdPayment.links.GetEnumerator();
                        string paypalRedirectUrl = null;
                        while (links.MoveNext())
                        {
                            Links lnk = links.Current;
                            if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                paypalRedirectUrl = lnk.href;
                            }
                        }
                        // saving the paymentID in the key guid
                        Session.Add(guid, createdPayment.id);

                        return Redirect(paypalRedirectUrl);
                    }
                    else
                    {
                        var guid = Request.Params["guid"];
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            return Redirect("/dat-hang-that-bai");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Error" + ex.Message);
                    return Redirect("/dat-hang-that-bai");
                }
                order.Status = false;
                order.PaymentStatus = "Chưa Thanh Toán";

                try
                {
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    var detail = new OrderDetailDAO();
                    decimal total = 0;

                    foreach (var item in cart)
                    {
                        total += (item.Product.Price * item.Quantity);
                    }
                    order.Total = total;
                    new OrderDAO().Insert(order);
                    total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = order.ID;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Dough = item.Dough;
                        orderDetail.Size = item.Size;
                        detail.Insert(orderDetail);

                        total += (item.Product.Price * item.Quantity);
                    }

                    // model.CustomerAddress = model.CustomerAddress;

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/content/client/template/neworder.html"));

                    content = content.Replace("{{OrderID}}", order.ID.ToString());
                    content = content.Replace("{{CustomerName}}", model.CustomerName);
                    content = content.Replace("{{Phone}}", model.CustomerMobie);
                    content = content.Replace("{{Address}}", model.CustomerAddress);
                    content = content.Replace("{{Email}}", order.CustomerEmail);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    content = content.Replace("{{OrderMethod}}", order.OrderMethod);

                    new MailHelper().SendMail(order.CustomerEmail, "Đơn Hàng Pizza", content);

                    Session["OrderID"] = order.ID;
                }
                catch (Exception)
                {
                    return Redirect("/dat-hang-that-bai");
                }
                return Redirect("/dat-hang-thanh-cong");
            }
            else
            {
                var orderNoLogin = new OrderNoLogin();
                orderNoLogin.CreatedDate = DateTime.Now;
                orderNoLogin.CustomerName = model.CustomerName;
                orderNoLogin.CustomerEmail = model.CustomerEmail;
                orderNoLogin.CustomerMobile = model.CustomerMobie;
                orderNoLogin.CustomerAddress = model.CustomerAddress;
                orderNoLogin.CreatedBy = "CUSTOMERNOLOGIN";

                if (orderKind == "DatLay")
                {
                    orderNoLogin.OrderMethod = "Đặt Đến Lấy";
                }
                else
                {
                    orderNoLogin.OrderMethod = "Đặt Giao Hàng";
                }
                orderNoLogin.PaymentMethod = "Thanh Toán qua PayPal";
                APIContext apiContext = Configuration.GetAPIContext();

                try
                {
                    string payerId = Request.Params["PayerID"];

                    if (string.IsNullOrEmpty(payerId))
                    {
                        string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cart/PaymentWithPayPal?";
                        var guid = Convert.ToString((new Random()).Next(100000));
                        var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                        var links = createdPayment.links.GetEnumerator();
                        string paypalRedirectUrl = null;
                        while (links.MoveNext())
                        {
                            Links lnk = links.Current;
                            if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                paypalRedirectUrl = lnk.href;
                            }
                        }
                        // saving the paymentID in the key guid
                        Session.Add(guid, createdPayment.id);

                        return Redirect(paypalRedirectUrl);
                    }
                    else
                    {
                        var guid = Request.Params["guid"];
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            return Redirect("/dat-hang-that-bai");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Error" + ex.Message);
                    return Redirect("/dat-hang-that-bai");
                }
                orderNoLogin.Status = false;
                orderNoLogin.PaymentStatus = "Chưa Thanh Toán";

                try
                {
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    var detail = new OrderDetailDAO();
                    decimal total = 0;

                    foreach (var item in cart)
                    {
                        total += (item.Product.Price * item.Quantity);
                    }
                    orderNoLogin.Total = total;
                    new OrderDAO().InsertCusNoLo(orderNoLogin);
                    total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetailsNoLogin();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = orderNoLogin.ID;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Dough = item.Dough;
                        orderDetail.Size = item.Size;
                        detail.InsertOrderNoLogin(orderDetail);

                        total += (item.Product.Price * item.Quantity);
                    }
                    model.CustomerAddress = model.CustomerAddress;

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/content/client/template/neworder.html"));

                    content = content.Replace("{{OrderID}}", orderNoLogin.ID.ToString());
                    content = content.Replace("{{CustomerName}}", model.CustomerName);
                    content = content.Replace("{{Phone}}", model.CustomerMobie);
                    content = content.Replace("{{Address}}", model.CustomerAddress);
                    content = content.Replace("{{Email}}", orderNoLogin.CustomerEmail);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    content = content.Replace("{{OrderMethod}}", orderNoLogin.OrderMethod);

                    new MailHelper().SendMail(orderNoLogin.CustomerEmail, "Đơn Hàng Pizza", content);

                    Session["OrderID"] = orderNoLogin.ID;
                }
                catch (Exception)
                {
                    return Redirect("/dat-hang-that-bai");
                }
                return Redirect("/dat-hang-thanh-cong");
            }
        }

        [HttpGet]
        public ActionResult LoginForOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginForOrder(LoginModel model)
        {
            string orderKind = (string)Session[CommonConstants.OrderKind];
            if (ModelState.IsValid)
            {
                var Func = new UserDAO();
                var result = Func.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = Func.GetUserByUserName(model.UserName);
                    var usersession = new UserLogin();

                    usersession.UserName = user.UserName;
                    usersession.UserID = user.ID;
                    usersession.Avatar = user.Avatar;

                    Session.Add(CommonConstants.User_Session, usersession);
                    if (orderKind == "DatLay")
                    {
                        return Redirect("/thong-tin-lay-hang");
                    }
                    else
                    {
                        return Redirect("/thong-tin-dat-hang");
                    }
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return View(model);
        }

        public ActionResult CustomerNoLogin()
        {
            string orderKind = Session[CommonConstants.OrderKind].ToString();
            if (orderKind == "DatLay")
            {
                return Redirect("/thong-tin-lay-hang");
            }
            else
            {
                return Redirect("/thong-tin-dat-hang");
            }
        }
        public ActionResult Success()
        {
            Session[CommonConstants.CartSession] = null;
            ViewBag.OrderID = Session["OrderID"];
            return View();
        }
        public ActionResult OrderFail()
        {
            return View();
        }
        public ActionResult NotLoggedIn()
        {
            return View();
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var cart = (List<CartItem>)Session[CommonConstants.CartSession];
            decimal total = 0;

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };
            foreach (var item in cart)
            {
                itemList.items.Add(new Item()
                {
                    name = item.Product.Name,
                    currency = "USD",
                    price = ((int)item.Product.Price / 23000).ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = "sku"
                });
                total += ((int)item.Product.Price / 23000) * item.Quantity;
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = total.ToString()
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = total.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = new Random().Next(100000).ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }


        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }
    }
}