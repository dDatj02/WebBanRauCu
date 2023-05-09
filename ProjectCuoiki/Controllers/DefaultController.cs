using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCuoiki.Controllers
{
    [Authorize(Roles = "User")]
    public class DefaultController : Controller
    {
        // GET: Default
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu()
        {
            var listitem =from item in db.typeproducts where item.hide==false orderby item.order ascending  select item;
            ViewBag.sanpham = "san-pham";
            return PartialView(listitem.ToList());
        }
        public ActionResult getSlideshow()
        {
            var listslide= from item in db.slideshows where item.hide==false orderby item.order ascending select item;
            return PartialView(listslide.ToList());
        }
        public ActionResult getFooter()
        {
           
            var item=db.footers.FirstOrDefault(i => i.hide == false);
            return PartialView(item);
        }
        public ActionResult getbannerTop()
        {
            var banneritem= db.banners.FirstOrDefault(i => i.hide == false& i.positionId==1);
            return PartialView(banneritem);  
        }
        public ActionResult getbannerMiddle()
        {
            var banneritem = db.banners.FirstOrDefault(i => i.hide == false & i.positionId==2);
            return PartialView(banneritem);
        }
        public ActionResult getbannerBottom()
        {
            var banneritem = db.banners.FirstOrDefault(i => i.hide == false & i.positionId == 3);
            return PartialView(banneritem);
        }
        public ActionResult getListNewProductByDate() { 

            var listitem=(from item in db.products where item.hide== false orderby item.datebegin ascending select item).Take(6);
            ViewBag.sp = "san-pham";
            
            return PartialView(listitem.ToList());
        }
        public ActionResult getNews()
        {
            var listitem = (from item in db.news where item.hide == false orderby item.datebegin ascending select item).Take(3);
            ViewBag.tintuc = "tin-tuc";
            return PartialView(listitem.ToList());
        }
    }
}