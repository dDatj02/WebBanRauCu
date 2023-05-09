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

        public JsonResult ListName(string q)
        {
            List<productSearch> allSearch = db.products.Where(x => x.name.Contains(q)).Select(x=>new productSearch
            {
                id= x.id,
                name = x.name,
            }).ToList();
            return new JsonResult { Data= allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = Search1(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.sp = "san-pham";
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        public List<productSearchModel> Search1(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.products.Where(x => x.name == keyword).Count();
            var model = (from a in db.products
                         join b in db.typeproducts
                         on a.idType equals b.id
                         where a.name.Contains(keyword)
                         select new
                         {
                             ID = a.id,
                             Name = a.name,
                             Description = a.description,
                             Link = a.link,
                             MetaTitle = a.meta,
                             typeProductMeta = b.meta,
                             CreatedDate = a.datebegin,
                             Price = a.price,
                             Images = a.imgname,
                         }).AsEnumerable().Select(x => new productSearchModel()
                         {
                             id = x.ID,
                             name = x.Name,
                             description = x.Description,
                             link = x.Link,
                             meta = x.MetaTitle,
                             typeProductMeta = x.typeProductMeta,
                             datebegin = x.CreatedDate,
                             price = x.Price,
                             imgname = x.Images
                         });
            model.OrderByDescending(x => x.datebegin).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}