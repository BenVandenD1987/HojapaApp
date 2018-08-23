using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HojapaApplication.Models;
using System.Data.Odbc;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace HojapaApplication.Controllers
{
    public class ReservatiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservaties
        public ActionResult Index()
        {
            return View(db.Reservaties.ToList());
        }

        // GET: Reservaties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservatie reservatie = db.Reservaties.Find(id);
            var reservatieDetails = db.ReservatieDetails.Where(x => x.ReservatieId == id );

            reservatie.ReservatieDetails = reservatieDetails.ToList();

            if (reservatie == null)
            {
                return HttpNotFound();
            }
            return View(reservatie);
        }

        // GET: Reservaties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservaties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservatieId,ReservatieDatum,Gebruikersnaam,Voornaam,Achternaam,Email,Totaal")] Reservatie reservatie)
        {
            if (ModelState.IsValid)
            {
                db.Reservaties.Add(reservatie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservatie);
        }

        // GET: Reservaties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservatie reservatie = db.Reservaties.Find(id);
            if (reservatie == null)
            {
                return HttpNotFound();
            }
            return View(reservatie);
        }

        // POST: Reservaties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservatieId,ReservatieDatum,Gebruikersnaam,Voornaam,Achternaam,Email,Totaal")] Reservatie reservatie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservatie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservatie);
        }

        // GET: Reservaties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservatie reservatie = db.Reservaties.Find(id);
            if (reservatie == null)
            {
                return HttpNotFound();
            }
            return View(reservatie);
        }

        // POST: Reservaties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservatie reservatie = db.Reservaties.Find(id);
            db.Reservaties.Remove(reservatie);
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

       /* private DataSet GetDataSet()
        {
            var conString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            string strConnString = conString.ConnectionString;

            SqlConnection conn = new SqlConnection(strConnString);
            conn.Open();
            string sql = "Select * FROM Reservaties";

            SqlDataAdapter ad = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            return ds;
        }
        */

            public ActionResult Reports(string ReportType)
        {

            DataSet1 DataSet1 = new DataSet1();
            DataTable reservatiesTable = DataSet1.Tables["Reservaties"];

            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");


            //localreport.DataSources.Add(new ReportDataSource("reservatiesTable", entities_data));

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else
            {
                fileNameExtension = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment:filename + reservaties_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }
    }
}

