using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCuoiki.help;
using ProjectCuoiki.Models;
using EntityState = System.Data.Entity.EntityState;

namespace ProjectCuoiki.Areas.admin.Controllers
{
    public class TypeproductsController : Controller
    {
        private ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();

        // GET: admin/Typeproducts
        public ActionResult Index()
        {
            return View(db.typeproducts.ToList());
        }

        // GET: admin/Typeproducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeproduct typeproduct = db.typeproducts.Find(id);
            if (typeproduct == null)
            {
                return HttpNotFound();
            }
            return View(typeproduct);
        }

        // GET: admin/Typeproducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Typeproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,order,hide")] typeproduct typeproduct)
        {
            typeproduct.datebegin=DateTime.Now;
            typeproduct.meta= Functions.convertToUnSign(typeproduct.name.Replace("?", "")).Replace(" ", "-");
            if (typeproduct.hide == null)
            {
                typeproduct.hide = false;
            }
            if (ModelState.IsValid)
            {
                db.typeproducts.Add(typeproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeproduct);
        }

        // GET: admin/Typeproducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeproduct typeproduct = db.typeproducts.Find(id);
            if (typeproduct == null)
            {
                return HttpNotFound();
            }
            return View(typeproduct);
        }

        // POST: admin/Typeproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,datebegin,order,hide")] typeproduct typeproduct)
        {
            if (typeproduct.hide == null)
            {
                typeproduct.hide = false;
            }
            if (ModelState.IsValid)
            {
                db.Entry(typeproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeproduct);
        }

        // GET: admin/Typeproducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeproduct typeproduct = db.typeproducts.Find(id);
            if (typeproduct == null)
            {
                return HttpNotFound();
            }
            return View(typeproduct);
        }

        // POST: admin/Typeproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            typeproduct typeproduct = db.typeproducts.Find(id);
            db.typeproducts.Remove(typeproduct);
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
