using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
    public class NewsController : Controller
    {
        private ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();

        // GET: admin/News
        public ActionResult Index()
        {
            return View(db.news.ToList());
        }

        // GET: admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,title,datebegin,description,link,meta,hide,dateUpdate,img,content,author,order")] news n , HttpPostedFileBase image)
        {
            var path = "";
            var filename = "";
            n.datebegin = DateTime.Now;
            n.meta = Functions.convertToUnSign( n.title.Replace("?","") + n.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy")).Replace(" ", "-");
            if (n.hide == null)
            {
                n.hide = false;
            }
            if (image != null)
            {
                filename = image.FileName;
                filename = filename.Replace(".", n.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy" + "."));
                path = Path.Combine(Server.MapPath("~/Uploads/img/latest-news"), filename);
                image.SaveAs(path);
                n.img = filename;
            }
            else
            {
                n.img = "logo.png";
            }



            if (ModelState.IsValid)
            {
                db.news.Add(n);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(n);
        }

        // GET: admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,title,datebegin,description,link,meta,hide,dateUpdate,img,content,author,order")] news n,HttpPostedFileBase image)
        {


            var path = "";
            var filename = "";
            n.dateUpdate = DateTime.Now;
            n.meta = Functions.convertToUnSign(n.title.Replace("?", "") + n.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy")).Replace(" ", "-");
            if (n.hide == null)
            {
                n.hide = false;
            }
            if (image != null)
            {
                filename = image.FileName;
                filename = filename.Replace(".", n.datebegin.GetValueOrDefault().ToString("ss-mm-hh-dd-mm-yyyy" + "."));
                path = Path.Combine(Server.MapPath("~/Uploads/img/latest-news"), filename);
                image.SaveAs(path);
                n.img = filename;
            }
           
            if (ModelState.IsValid)
            {
                db.news.AddOrUpdate(n);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(n);
        }

        // GET: admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            news news = db.news.Find(id);
            db.news.Remove(news);
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
