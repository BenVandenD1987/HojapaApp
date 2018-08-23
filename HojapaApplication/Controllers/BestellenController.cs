using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HojapaApplication.Configuration;
using HojapaApplication.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WebForms;

namespace HojapaApplication.Controllers
{
    //[Authorize]
    public class BestellenController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        AppConfigurations appConfig = new AppConfigurations();

        //public List<String> CreditCardTypes { get { return appConfig.CreditCardType; } }

        //
        // GET: /Checkout/Bevestiging
        public ActionResult Bevestiging()
        {
            //ViewBag.CreditCardTypes = CreditCardTypes;
            var previousOrder = storeDB.Reservaties.FirstOrDefault(x => x.Gebruikersnaam == User.Identity.Name);

            if (previousOrder == null)
                return View(previousOrder);
            else
                return View();
        }

        //
        // POST: /Checkout/Bevestiging
        [HttpPost]
        public async Task<ActionResult> Bevestiging(FormCollection values)
        {
            //ViewBag.CreditCardTypes = CreditCardTypes;
            //string result = values[9];

            var order = new Reservatie();
            TryUpdateModel(order);
            //order.CreditCard = result;

            try
            {
                //order.Gebruikersnaam = User.Identity.Name;
                //order.Email = User.Identity.Name;
                //var currentUserId = User.Identity.GetUserId();

               // if (/*order.SaveInfo && */!order.Gebruikersnaam.Equals("guest@guest.com"))
               // {

                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                    var ctx = store.Context;
                    var currentUser = manager.FindById(User.Identity.GetUserId());

                    /*currentUser.Address = order.Address;
                    currentUser.City = order.City;
                    currentUser.Country = order.Country;
                    currentUser.State = order.State;
                    currentUser.Phone = order.Phone;
                    currentUser.PostalCode = order.PostalCode;*/
                    //currentUser.FirstName = order.Voornaam;


                    await ctx.SaveChangesAsync();

                    await storeDB.SaveChangesAsync();
               // }



                storeDB.Reservaties.Add(order);
                await storeDB.SaveChangesAsync();

                var cart = ReservatieKaart.GetCart(this.HttpContext);
                order = cart.CreateOrder(order);



                BestellenController.SendOrderMessage(order.Voornaam, "New Order: " + order.ReservatieId, order.ToString(order), appConfig.OrderEmail);

                return RedirectToAction("Complete",
                    new { id = order.ReservatieId });

            }
            catch
            {

                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {

            bool isValid = storeDB.Reservaties.Any(
                o => o.ReservatieId == id /*&& o.Gebruikersnaam == User.Identity.Name*/);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        private static RestResponse SendOrderMessage(String toName, String subject, String body, String destination)
        {
            RestClient client = new RestClient();

            AppConfigurations appConfig = new AppConfigurations();
            client.BaseUrl = new System.Uri("http://www.vanden-dries.be");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              appConfig.EmailApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                appConfig.DomainForApiKey, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", appConfig.FromName + " <" + appConfig.FromEmail + ">");
            request.AddParameter("to", toName + " <" + destination + ">");
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            IRestResponse executor = client.Execute(request);
            return executor as RestResponse;
        }

        private DataSet GetDataSet(int reportId)
        {
            MySqlConnection connection = null;
            string connstring = string.Format("Server=hojapa.be;user id=u0105_hojapa;password=Hojbes1995+;persist security info=True;database=u0105_hojapa");
            connection = new MySqlConnection(connstring);
            connection.Open();
            ApplicationDbContext db = new ApplicationDbContext();
            string query = String.Format("SELECT Voornaam,Achternaam,Email,Hoeveelheid,UnitPrijs,ReservatieFormName,Datum FROM ((Reservaties INNER JOIN ReservatieDetails ON Reservaties.ReservatieId = ReservatieDetails.ReservatieId) INNER JOIN ReservatieForms ON ReservatieDetails.ReservatieFormId = ReservatieForms.ID) WHERE Reservaties.ReservatieId = @reportId");

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.Add(new MySqlParameter("@ReportId", reportId));
            MySqlDataAdapter ad = new MySqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            ad.Fill(ds);

            return ds;
        }

        public ActionResult Reports(string ReportType, int reportId)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
            DataSet ds = GetDataSet(reportId);

            localreport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
            localreport.DataSources.Add(new ReportDataSource("DataSet2", ds.Tables[0]));
            localreport.DataSources.Add(new ReportDataSource("DataSet3", ds.Tables[0]));

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