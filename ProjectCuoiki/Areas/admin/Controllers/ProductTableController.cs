using Microsoft.Ajax.Utilities;
using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCuoiki.help;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace ProjectCuoiki.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductTableController : Controller
    {
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();

      

        // GET: admin/ProductTable
        public ActionResult Index()
        {
            var listItem = from i in db.products select i;
            ViewBag.listType=from i in db.typeproducts select i;
            return View(listItem.ToList());
        }
        public ActionResult getProductByType(int? id)
        {
            if (id == null)
            {
                var listp = from i in db.products select i;
                return PartialView(listp.ToList());
            }
            var listItem =from i in db.products where i.idType==id select i;
            return PartialView(listItem.ToList());
        }
        // GET: admin/ProductTable/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: admin/ProductTable/Create
        public ActionResult CreateProduct()
        {
            ViewBag.listType=from i in db.typeproducts select i;
            return View();
        }

        // POST: admin/ProductTable/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateProduct([Bind(Include = "name,idType,image,price,quantity,order,description,hide")] product p , HttpPostedFileBase image)
        {
            var path = "";
            var filename = "";
            if (ModelState.IsValid) {
                p.datebegin = DateTime.Now;
                p.meta = Functions.convertToUnSign(p.name + p.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy")).Replace(" ","-");
                if (p.hide == null)
                {
                    p.hide = false;
                }
                if (image != null)
                {
                    filename= image.FileName;
                    filename = filename.Replace(".", p.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy"+"."));
                    path = Path.Combine(Server.MapPath("~/Uploads/img/products"),filename);
                    image.SaveAs(path);
                    p.imgname=filename;
                }
                else
                {
                    p.imgname = "logo.png";
                }
              
                db.products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
          
        }

        // GET: admin/ProductTable/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.listType = from i in db.typeproducts select i;
            var p = db.products.Find(id);
            return View(p);
        }

        // POST: admin/ProductTable/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,idType,image,imgname,price,quantity,order,description,hide")] product p, HttpPostedFileBase image)
        {
            var path = "";
            var filename = "";
            p.datebegin = DateTime.Now;
            p.meta = Functions.convertToUnSign(p.name + p.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy")).Replace(" ", "-");
            if (p.hide == null)
            {
                p.hide = false;
            }
            if (image != null)
            {
                filename = image.FileName;
                filename = filename.Replace(".", p.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy" + "."));
                path = Path.Combine(Server.MapPath("~/Uploads/img/products"), filename);
                image.SaveAs(path);
                p.imgname = filename;
            }
          
            if (ModelState.IsValid)
            {
                db.products.AddOrUpdate(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // GET: admin/ProductTable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product=db.products.Find(id);
            return View(product);
        }

        // POST: admin/ProductTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int? id)
        {
            if(id != null) { 
            var item = db.products.Find(id);
                var listItem = from i in db.imageProducts where i.idproduct == id select i;
            foreach(var i in listItem)
            {
                db.imageProducts.Remove(i);

            }
                db.products.Remove(item);
            
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View();
        }
    }
}
