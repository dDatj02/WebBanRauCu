using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectCuoiki.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeAdminController : Controller
    {
        // GET: admin/Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogoutAdmin()
        {
            FormsAuthentication.SignOut();
            var myCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            myCookie.Domain = "mysite.com";
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Response.Cookies.Add(myCookie);
            return RedirectToAction("Index");
        }
    }
}