using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var item= db.news.FirstOrDefault(n=>n.meta==meta&&n.hide==false);
            return View(item);
        }
        public ActionResult getRecentPost()
        {
            var listItem = from i in db.news orderby i.datebegin ascending select i;
            return PartialView(listItem.Take(5).ToList());

                   
        }
    }
}