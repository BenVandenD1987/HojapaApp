using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HojapaApplication.Models;
using HojapaApplication.Controllers;

namespace HojapaApplication.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie Categorie = await db.Categories.FindAsync(id);
            if (Categorie == null)
            {
                return HttpNotFound();
            }
            return View(Categorie);
        }

        // GET: Categories/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] Categorie Categorie)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(Categorie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Categorie);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie Categorie = await db.Categories.FindAsync(id);
            if (Categorie == null)
            {
                return HttpNotFound();
            }
            return View(Categorie);
        }

        // POST: Categories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Categorie Categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Categorie).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Categorie);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie Categorie = await db.Categories.FindAsync(id);
            if (Categorie == null)
            {
                return HttpNotFound();
            }
            return View(Categorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categorie Categorie = await db.Categories.FindAsync(id);
            db.Categories.Remove(Categorie);
            await db.SaveChangesAsync();
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
