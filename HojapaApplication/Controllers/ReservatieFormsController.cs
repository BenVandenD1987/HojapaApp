using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using HojapaApplication.Models;

namespace HojapaApplication.Controllers
{
    public class ReservatieFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Prijs" ? "price_desc" : "Prijs";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.ReservatieForms
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ReservatieFormName.ToUpper().Contains(searchString.ToUpper())
                                       || s.Categorie.Name.ToUpper().Contains(searchString.ToUpper()) || s.Productie.ProductieNaam.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.ReservatieFormName);
                    break;
                case "Prijs":
                    items = items.OrderBy(s => s.Prijs);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.Prijs);
                    break;
                default:
                    items = items.OrderBy(s => s.ReservatieFormName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));



        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReservatieForm item = await db.ReservatieForms.FindAsync(id);

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategorieId = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(ReservatieForm item)
        {
            if (ModelState.IsValid)
            {
                db.ReservatieForms.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategorieId = new SelectList(db.Categories, "ID", "Name", item.CategorieId);
            return View(item);
        }

        // GET: Items/Edit/5
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservatieForm item = await db.ReservatieForms.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorieId = new SelectList(db.Categories, "ID", "Name", item.CategorieId);
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(ReservatieForm item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategorieId = new SelectList(db.Categories, "ID", "Name", item.CategorieId);
            return View(item);
        }

        // GET: Items/Delete/5
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservatieForm item = await db.ReservatieForms.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReservatieForm item = await db.ReservatieForms.FindAsync(id);
            db.ReservatieForms.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /*public async Task<ActionResult> RenderImage(int id)
        {
            ReservatieForm item = await db.ReservatieForms.FindAsync(id);

            byte[] photoBack = item.InternalImage;

            return File(photoBack, "image/png");
        }*/

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
