using Model.Dao;
using Model.EF;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(User model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName,Encrypter.MD5Hash(model.Password));
                if (result==1)
                {
                    var user = dao.GetById(model.UserName);
                    var usrerSession = new UserLogin();
                    usrerSession.UserName = user.UserName;
                    usrerSession.UserID = user.ID;

                    Session.Add(CommonConstans.USER_SESSION, usrerSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result==0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại !!!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa !!!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng !!!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng !!!");
                }
            }
          
            return View("Index");
        }
    }
}