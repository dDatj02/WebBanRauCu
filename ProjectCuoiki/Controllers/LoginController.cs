using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectCuoiki.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(userAccount user)
        {

            ProjectCuoiKiEntities projectCuoiKi = new ProjectCuoiKiEntities();
            int? userId = projectCuoiKi.userValid(user.userName, user.passWord).FirstOrDefault();

            string message = string.Empty;
            switch (userId.Value)
            {
                case 0:
                    message = "Username or password is incorrect.";
                    break;
                default:
                    FormsAuthentication.SetAuthCookie(user.userName, user.rememberMe);
                    if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                    {
                        return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                    }
                    else
                    {
                        var role = (from u in db.userAccounts
                                    join r in db.userRoles on u.userID equals r.userID
                                    where u.userName == user.userName
                                    select r.role).ToArray();

                        if (role[0] == "Admin")
                        {
                            return RedirectToAction("Index", "HomeAdmin", new { Area = "admin" });

                        }
                        return RedirectToAction("Index", "Default");
                    }
            }

            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
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