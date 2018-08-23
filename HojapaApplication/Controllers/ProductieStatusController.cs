using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HojapaApplication.Models;

namespace HojapaApplication.Controllers
{
   [Authorize]
    public class ProductieStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductieStatus
        public ActionResult Index()
        {
            return View(db.ProductieStatus.ToList());
        }

        // GET: ProductieStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductieStatus productieStatusModel = db.ProductieStatus.Find(id);
            if (productieStatusModel == null)
            {
                return HttpNotFound();
            }
            return View(productieStatusModel);
        }

        // GET: ProductieStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductieStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam")] ProductieStatus productieStatusModel)
        {
            if (ModelState.IsValid)
            {
                db.ProductieStatus.Add(productieStatusModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productieStatusModel);
        }

        // GET: ProductieStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductieStatus productieStatusModel = db.ProductieStatus.Find(id);
            if (productieStatusModel == null)
            {
                return HttpNotFound();
            }
            return View(productieStatusModel);
        }

        // POST: ProductieStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam")] ProductieStatus productieStatusModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productieStatusModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productieStatusModel);
        }

        // GET: ProductieStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductieStatus productieStatusModel = db.ProductieStatus.Find(id);
            if (productieStatusModel == null)
            {
                return HttpNotFound();
            }
            return View(productieStatusModel);
        }

        // POST: ProductieStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductieStatus productieStatusModel = db.ProductieStatus.Find(id);
            db.ProductieStatus.Remove(productieStatusModel);
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
