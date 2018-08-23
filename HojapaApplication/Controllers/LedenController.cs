using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HojapaApplication.Models;
using System.Data.Entity.Infrastructure;
using System.IO;
using PagedList;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel.Composition.Primitives;
using ClosedXML.Excel;

namespace HojapaApplication.Controllers
{
    [Authorize]
    public class LedenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Leden
        public ActionResult Index(string Sorting_Order, string Search_Data, string SoortLid)
        {
            //ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
            //ViewBag.SortingLidType = String.IsNullOrEmpty(Sorting_Order) ? "Sorting_LidType" : "";
            //ViewBag.AllData = String.IsNullOrEmpty(Sorting_Order) ? "All_Data" : "";
            ViewBag.AllData = (from r in db.Leden select r.Naam).Distinct();
            ViewBag.SoortLid = (from r in db.LidTypes select r.Naam).Distinct();

            //var leden = from r in db.Leden select r;

            var leden = from r in db.Leden
                        orderby r.Naam
                        where r.SoortLid == SoortLid || SoortLid == null || SoortLid == ""
                        select r;


            if (!String.IsNullOrEmpty(Search_Data))
            {
                leden = leden.Where(s => s.Naam.Contains(Search_Data)
                                       || s.Voornaam.Contains(Search_Data));
            }

            switch (Sorting_Order)
            {
                // case "Name_Description":
                //leden = leden.OrderByDescending(stu => stu.Naam);
                // break;
                case "Date_Enroll":
                //   leden = leden.OrderBy(stu => stu.Geboortedatum);
                //   break;
                // case "Sorting_LidType":
                //  leden = leden.OrderByDescending(stu => stu.SoortLid);
                // break;
                case "AllData":
                    leden = leden.OrderBy(stu => stu.Naam);
                    break;
                // case "SoortLid":
                //    leden = leden.OrderBy(stu => stu.Naam);
                //   break;
                default:
                    leden = leden.OrderBy(stu => stu.Naam);
                    break;


            }


            return View(leden.ToList());
        }



        // GET: Leden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //LedenModels ledenModels = db.Leden.Find(id);
            Leden ledenModels = db.Leden.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            if (ledenModels == null)
            {
                return HttpNotFound();
            }
            return View(ledenModels);
        }

        // GET: Leden/Create
        public ActionResult Create()


        {
            ViewBag.LidTypes = db.LidTypes.Where(x => x.isActive);
            // ViewBag.LidTypes = db.LidTypes.Select(x => x.Naam);
            var model = new Leden();
            return View(model);
        }

        // POST: Leden/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SoortLid,Naam,Voornaam,Geslacht,Geboortedatum,Adres,Postcode,Gemeente,Nummer,BusNummer,Telefoon,Gsm,Email,Website")] Leden ledenModels, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var avatar = new Models.File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        ledenModels.Files = new List<Models.File> { avatar };
                    }
                    db.Leden.Add(ledenModels);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(ledenModels);
        }


        // GET: Leden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //LedenModels ledenModels = db.Leden.Find(id);
            Leden ledenModels = db.Leden.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            if (ledenModels == null)
            {
                return HttpNotFound();
            }
            return View(ledenModels);
        }

        // POST: Leden/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SoortLid,Naam,Voornaam,Geslacht,Geboortedatum,Adres,Postcode,Gemeente,Nummer,BusNummer,Telefoon,Gsm,Email,Website")] int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ledenModels = db.Leden.Find(id);
            if (TryUpdateModel(ledenModels))
            {

                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (ledenModels.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(ledenModels.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new Models.File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        ledenModels.Files = new List<Models.File> { avatar };
                    }
                    db.Entry(ledenModels).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(ledenModels);
        }

        // GET: Leden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leden ledenModels = db.Leden.Find(id);
            if (ledenModels == null)
            {
                return HttpNotFound();
            }
            return View(ledenModels);
        }

        // POST: Leden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leden ledenModels = db.Leden.Find(id);
            db.Leden.Remove(ledenModels);
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


        [HttpPost]
        public FileResult Export()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("CustomerId"),
                                            new DataColumn("ContactName"),
                                            new DataColumn("City"),
                                            new DataColumn("Country") });

            var ledenListExcel = from lid in db.Leden.Take(10)
                                 select lid;

            foreach (var lid in ledenListExcel)
            {
                dt.Rows.Add(lid.ID, lid.Naam);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }

}



