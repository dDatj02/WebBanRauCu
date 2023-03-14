using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCuoiki.Controllers
{
    public class ProductController : Controller
    {
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();

        // GET: Product
        public ActionResult Index(string meta)
        {
            var listitem = from i in db.products where i.typeproduct.meta == meta && i.hide==true orderby i.datebegin ascending select i;
            return View(listitem.ToList().Take(9));
        }

    }
}