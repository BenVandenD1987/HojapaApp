using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HojapaApplication.Models;

namespace HojapaApplication.Controllers
{
    [Authorize]
    public class ProductieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Productie
        public ActionResult Index()
        {
            var producties = from r in db.Producties
                        select r;


            return View(producties.ToList());
        }

        // GET: Productie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productie productieModels = db.Producties.Find(id);
            if (productieModels == null)
            {
                return HttpNotFound();
            }
            return View(productieModels);
        }

        // GET: Productie/Create
        public ActionResult Create()
        {

            ViewBag.LedenList = db.Leden.ToList<Leden>();
            ViewBag.SelectedLeden = new List<int>();
            ViewBag.Status = db.ProductieStatus.ToList<ProductieStatus>();
            ViewBag.ProductieStatusId = new SelectList(db.ProductieStatus, "Id", "Naam");

            return View();
}


// POST: Productie/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductieNaam,Auteur,Regisseur,Datum,AantalToeschouwers,ProductieStatusId")] Productie productieModels, int[] ledenIds)
        {

            //var list = (from r in db.Leden select r.Naam).Distinct();
            // ViewBag.AllData = db.Leden.Select(x => x.Naam);

            if (ModelState.IsValid)
            {
                if (productieModels.LedenList == null) productieModels.LedenList = new List<Leden>();
                foreach (int templateId in ledenIds)
                {
                    Leden t = db.Leden.Find(templateId);
                    productieModels.LedenList.Add(t);
                }
                db.Producties.Add(productieModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LedenList = db.Leden.ToList<Leden>();
            ViewBag.SelectedLeden = productieModels.LedenList.Select(t => t.ID).ToList<int>();
            ViewBag.Status = db.ProductieStatus.ToList<ProductieStatus>();
            ViewBag.ProductieStatusId = new SelectList(db.ProductieStatus, "Id", "Naam", productieModels.ProductieStatusId);
            return View(productieModels);
        }

        // GET: Productie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productie productieModels = db.Producties.Find(id);
            if (productieModels == null)
            {
                return HttpNotFound();
            }
            return View(productieModels);
        }

        // POST: Productie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductieNaam,Auteur,Regisseur,Datum,AantalToeschouwers")] Productie productieModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productieModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productieModels);
        }

        // GET: Productie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productie productieModels = db.Producties.Find(id);
            if (productieModels == null)
            {
                return HttpNotFound();
            }
            return View(productieModels);
        }

        // POST: Productie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productie productieModels = db.Producties.Find(id);
            db.Producties.Remove(productieModels);
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
