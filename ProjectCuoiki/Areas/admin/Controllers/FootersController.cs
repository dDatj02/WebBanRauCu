using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCuoiki.Models;
using EntityState = System.Data.Entity.EntityState;

namespace ProjectCuoiki.Areas.admin.Controllers
{
    public class FootersController : Controller
    {
        private ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();

        // GET: admin/Footers
        public ActionResult Index()
        {
            return View(db.footers.ToList());
        }

        // GET: admin/Footers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            footer footer = db.footers.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // GET: admin/Footers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Footers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email,phone,address,hide,meta,order,datebegin,aboutus")] footer footer)
        {
            if (ModelState.IsValid)
            {
                db.footers.Add(footer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(footer);
        }

        // GET: admin/Footers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            footer footer = db.footers.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // POST: admin/Footers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email,phone,address,hide,meta,order,datebegin,aboutus")] footer footer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(footer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(footer);
        }

        // GET: admin/Footers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            footer footer = db.footers.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // POST: admin/Footers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            footer footer = db.footers.Find(id);
            db.footers.Remove(footer);
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
