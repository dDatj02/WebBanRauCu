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
        public ActionResult Index(string meta ,int? page)
        {
            var listitem = from i in db.products where i.typeproduct.meta == meta && i.hide==false orderby i.datebegin ascending select i;
            ViewBag.sp = "san-pham";
            int NoOfRecordOnPage = 9;
            int NoOfPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(db.products.Count()) / Convert.ToDouble(NoOfRecordOnPage)));
            if (page == null)
            {
                page= 1;
            }
            ViewBag.NoOffPage = NoOfPage;
            ViewBag.CurrentPage = page;
         
              
                int NoOffRecordSkip = (int)(page - 1) * NoOfRecordOnPage;
                return View(listitem.Skip(NoOffRecordSkip).Take(9).ToList());
           
        }

    }
}