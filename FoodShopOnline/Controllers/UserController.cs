using Facebook;
using FoodShopOnline.Common;
using FoodShopOnline.Models;
using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class UserController : Controller
    {
        FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        //public object IdentityManager { get; private set; }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var func = new UserDAO();
                if (func.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (func.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.GroupID = "MEMBER";
                    user.Phone = "01234567890";
                    user.CreatedBy = "CUSTOMER";
                    user.ModifiedBy = "CUSTOMER";
                    user.ModifiedDate = DateTime.Now;
                    user.Name = model.Name;
                    user.Avatar = "/content/client/images/user.png";
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;

                    if (ModelState.IsValid)
                    {
                        ConfirmKey key = new ConfirmKey();

                        Session["User"] = user;
                        string confirmKey = new Random().Next(100000, 1000000).ToString();
                        string subject = "Mã xác thực đăng ký tài khoản Cửa Hàng Royal Pizza";
                        Session[user.Email] = confirmKey;
                        SendEmail(user.Email, subject, key.BodyMail(user.Email, user.Name, confirmKey, user.UserName));
                        return RedirectToAction("ConfirmAccount", "User");
                    }

                    //int result = func.insert(user.UserName, user.Password, user.GroupID, user.Name, user.Address,
                    //    user.Email, user.Phone, user.Status);
                    //if (result > 0)
                    //{
                    //    ViewBag.Success = "Đăng kí thành công";
                    //    model = new RegisterModel();
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("", "Đăng kí không thành công");
                    //}
                }
            }
            return View(model);
            //return RedirectToAction("ConfirmAccount", "User");
        }

        public ActionResult ConfirmAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmAccount(string confirmKey)
        {
            User user = (User)Session["User"];
            if (confirmKey == (string)Session[user.Email])
            {
                Session[user.Email] = null;
                Session["User"] = null;
                //db.Users.Add(user);
                //db.SaveChanges();
                new UserDAO().insert(user);
                // ViewBag.Success = "Đăng kí thành công";
                TempData["DangKyMess"] = "alert('Đăng kí thành công')";
                return RedirectToAction("Login", "User");
            }
            TempData["DangKyMess"] = "alert('Sai mã xác thực')";
            return RedirectToAction("ConfirmAccount", "User");
        }

        public void SendEmail(string address, string subject, string body)
        {
            string email = "nguyenhoanggiakhanh1999@gmail.com";
            string password = "Giakhanh456";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.User_Session] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
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
                    //if (Session[CommonConstants.CartSession] != null && Session[CommonConstants.OrderLogin] != null)
                    //{
                    //    Session[CommonConstants.OrderLogin] = null;
                    //    return Redirect("/thong-tin-dat-hang");
                    //}
                    return Redirect("/");
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



        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user infomation, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string username = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                user.Email = email;
                user.UserName = email;
                user.Status = true;
                user.CreatedBy = "CUSTOMER";
                user.CreatedDate = DateTime.Now;
                user.Name = firstname + " " + middlename + " " + lastname;
                var resultInsert = new UserDAO().insertForFacebook(user);

                if (resultInsert > 0)
                {
                    var usersession = new UserLogin();

                    usersession.UserName = user.Name;
                    usersession.UserID = user.ID;

                    Session.Add(CommonConstants.User_Session, usersession);
                    return Redirect("/");
                }
            }
            return Redirect("/");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            User user = db.Users.Where(x => x.Email == email).FirstOrDefault();
            ConfirmKey bodymail = new ConfirmKey();
            if (user != null)
            {

                //ForgotPassword mail = new ForgotPassword();
                //string bodymail = mail.BodyMail_LayLaiMatKhau(model.Email, t.Password);
                //string ThongBao = mail.Send("Lấy lại mật khẩu", bodymail, model.Email, true, true);
                //ViewBag.ThongBao = ThongBao;
                string subject = "Mã xác thực tạo mới mật khẩu tài khoản Royal' Pizza";
                Session["User"] = user;
                string confirmKey = new Random().Next(100000, 1000000).ToString();
                Session[user.Email] = confirmKey;
                SendEmail(user.Email, subject, bodymail.BodyMail(user.Email, user.Name, confirmKey, user.UserName));
                return RedirectToAction("ConfirmKeyPassword", "User");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmKeyPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmKeyPassword(string key)
        {
            User user = (User)Session["User"];
            if (key == (string)Session[user.Email])
            {
                return RedirectToAction("ResetPassword", "User");
            }
            TempData["DangKyMess"] = "alert('Mã xác thực không đúng')";
            return RedirectToAction("ConfirmKeyPassword", "User");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPassword model)
        {
            User user = (User)Session["User"];
            if (user != null && ModelState.IsValid)
            {
                user.Password = Encryptor.MD5Hash(model.NewPassword);
                new UserDAO().NewPassword(user);
                TempData["DangKyMess"] = "alert('Đổi mật khẩu thành công')";
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpGet]
        public ActionResult InformationAccount()
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            var model = new UserDAO().ViewDetailUser(session.UserID);

            return View(model);
        }
        [HttpPost]
        public ActionResult InformationAccount(User model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.User_Session];
                if (model.Gender == "1")
                {
                    model.Gender = "Nam";
                }
                else
                {
                    model.Gender = "Nữ";
                }

                string imgpath = Server.MapPath((string)session.Avatar);
                string fileimgpath = imgpath;
                FileInfo fi = new FileInfo(fileimgpath);
                if (fi.Exists)
                {
                    fi.Delete();
                }
                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("/Data/Avatar/"), filename);
                    file.SaveAs(filepath);
                }
                model.Avatar = "/Data/Avatar/" + file.FileName;
                var User = new UserDAO();
                var result = User.UpdateAccount(model, session.UserID);
                if (result)
                {
                    ModelState.AddModelError("", "Sửa thông tin tài khoản thành công");
                    return Redirect("/thong-tin-tai-khoan");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa thông tin tài khoản không Thành công. Xin kiểm tra lại");
                }
            }
            return Redirect("/thong-tin-tai-khoan");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword password)
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            var user = new UserDAO().ViewDetailUser(session.UserID);
            password.OldPassword = Encryptor.MD5Hash(password.OldPassword);
            if (ModelState.IsValid)
            {
                if (password.OldPassword == user.Password)
                {
                    if(password.NewPassword == password.ConfirmPassword)
                    {
                        password.NewPassword = Encryptor.MD5Hash(password.NewPassword);
                        new UserDAO().ChangePassword(password.NewPassword, session.UserID);
                        ModelState.AddModelError("", "Đổi mật khẩu thành công");
                        return Redirect("/thong-tin-tai-khoan");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Xác nhận mật khẩu không đúng");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu tài khoản cũ không đúng");
                }
            }
            ModelState.AddModelError("", "Mật khẩu tài khoản cũ không đúng hoặc xác nhận mật khẩu không đúng");
            return View();
        }
    }
}