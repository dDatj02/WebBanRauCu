using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCuoiki.Models;

namespace ProjectCuoiki.Controllers
{
    public class NewsController : Controller
    {
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();
        // GET: News
        public ActionResult Index(int? id,string meta)
        {
            var item= db.news.FirstOrDefault(n=>n.id== id&&n.meta==meta&&n.hide==true);
            return View(item);
        }
    }
}