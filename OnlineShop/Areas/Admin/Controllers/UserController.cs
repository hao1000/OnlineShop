using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // Hiển thị danh sách User
        // GET: Admin/User
        public ActionResult Index(int page=1,int pageSize=3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
   
        // Tạo User , nếu thành công thì move màn hình Index
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                user.Password = Encrypter.MD5Hash(user.Password);
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm User không thành công !!!");
                }
            }
            //View mac dinh la Create.cshtml
            return View("Index");
        }

        // Màn hình khởi tạo mới User (Hiện form tạo User)
        public ActionResult Create()
        {
            //View mac dinh la Create.cshtml
            return View();
        }
      //  [HttpPost]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if(!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = Encrypter.MD5Hash(user.Password);
                    var result = dao.Update(user);
                    if (result)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật User không thành công !!!");
                    }
                }
            }
            
            //View mac dinh la Create.cshtml
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index") ;
        }

    }
}