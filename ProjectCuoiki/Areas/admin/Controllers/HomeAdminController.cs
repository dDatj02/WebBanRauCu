using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCuoiki.Areas.admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: admin/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}