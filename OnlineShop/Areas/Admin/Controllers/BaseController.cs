using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = (UserLogin)Session[CommonConstans.USER_SESSION];
            if(sess==null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}