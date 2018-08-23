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
    public class LidTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LidType
        public ActionResult Index()
        {
            return View(db.LidTypes.ToList());
        }

        // GET: LidType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LidType lidType = db.LidTypes.Find(id);
            if (lidType == null)
            {
                return HttpNotFound();
            }
            return View(lidType);
        }

        // GET: LidType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LidType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Naam,isActive")] LidType lidType)
        {
            if (ModelState.IsValid)
            {
                db.LidTypes.Add(lidType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lidType);
        }

        // GET: LidType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LidType lidType = db.LidTypes.Find(id);
            if (lidType == null)
            {
                return HttpNotFound();
            }
            return View(lidType);
        }

        // POST: LidType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naam,isActive")] LidType lidType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lidType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lidType);
        }

        // GET: LidType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LidType lidType = db.LidTypes.Find(id);
            if (lidType == null)
            {
                return HttpNotFound();
            }
            return View(lidType);
        }

        // POST: LidType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LidType lidType = db.LidTypes.Find(id);
            db.LidTypes.Remove(lidType);
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
