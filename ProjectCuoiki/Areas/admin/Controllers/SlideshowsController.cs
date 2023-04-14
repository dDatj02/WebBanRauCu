using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCuoiki.help;
using ProjectCuoiki.Models;
using EntityState = System.Data.Entity.EntityState;

namespace ProjectCuoiki.Areas.admin.Controllers
{
    public class SlideshowsController : Controller
    {
        private ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();

        // GET: admin/Slideshows
        public ActionResult Index()
        {
            return View(db.slideshows.ToList());
        }

        // GET: admin/Slideshows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slideshow slideshow = db.slideshows.Find(id);
            if (slideshow == null)
            {
                return HttpNotFound();
            }
            return View(slideshow);
        }

        // GET: admin/Slideshows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Slideshows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,img,link,meta,order,hide,datebegin,text,title")] slideshow s, HttpPostedFileBase image)
        {
            var path = "";
            var filename = "";
            s.datebegin = DateTime.Now;
            s.meta = Functions.convertToUnSign(s.name + s.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy")).Replace(" ", "-");
            if (s.hide == null)
            {
                s.hide = false;
            }
            if (image != null)
            {
                filename = image.FileName;
                filename = filename.Replace(".",s.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy" + "."));
                path = Path.Combine(Server.MapPath("~/Uploads/img/slide"), filename);
                image.SaveAs(path);
                s.name = filename;
            }
            if (ModelState.IsValid)
            {
                db.slideshows.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s);
        }

        // GET: admin/Slideshows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slideshow slideshow = db.slideshows.Find(id);
            if (slideshow == null)
            {
                return HttpNotFound();
            }
            return View(slideshow);
        }

        // POST: admin/Slideshows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,img,link,meta,order,hide,datebegin,text,title")] slideshow slideshow , HttpPostedFileBase image)
        {
            var path = "";
            var filename = "";
            slideshow.datebegin = DateTime.Now;
            slideshow.meta = Functions.convertToUnSign(slideshow.name + slideshow.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy")).Replace(" ", "-");
            if (slideshow.hide == null)
            {
                slideshow.hide = false;
            }
            if (image != null)
            {
                filename = image.FileName;
                filename = filename.Replace(".", slideshow.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy" + "."));
                path = Path.Combine(Server.MapPath("~/Uploads/img/slide"), filename);
                image.SaveAs(path);
                slideshow.name = filename;
            }
            if (ModelState.IsValid)
            {
                db.Entry(slideshow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slideshow);
        }

        // GET: admin/Slideshows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slideshow slideshow = db.slideshows.Find(id);
            if (slideshow == null)
            {
                return HttpNotFound();
            }
            return View(slideshow);
        }

        // POST: admin/Slideshows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            slideshow slideshow = db.slideshows.Find(id);
            db.slideshows.Remove(slideshow);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
