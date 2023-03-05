using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCuoiki.Controllers
{
    public class SingleProductController : Controller
    {
        // GET: SingleProduct4
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();

        public ActionResult Index(string meta)
        {
            var item= db.products.FirstOrDefault(i=>i.meta==meta);
            return View(item);
        }
    }
}