using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class StudentController : Controller
    {
        // GET: Admin/Student
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        // Ten bien khai bao phai giong tren URL tra ve
        public ActionResult Submit(int ma, string ten)
        {
            ViewBag.Id = ma;
            ViewBag.Name = ten;
            return View("Index");
        }

        [HttpPost]
        public ActionResult Submit(FormCollection fc)
        {
            ViewBag.Id = fc["0"];
            ViewBag.Name = fc["1"];
            return View("Index");
        }
    }
}