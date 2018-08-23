using HojapaApplication.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HojapaApplication.Models
{

    public partial class Reservatie
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ReservatieId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime ReservatieDatum { get; set; }

        [ScaffoldColumn(false)]
        public string Gebruikersnaam { get; set; }

        [Display(Name = "Voornaam")]
        [StringLength(160)]
        public string Voornaam { get; set; }

        [Display(Name = "Achternaam")]
        [StringLength(160)]
        public string Achternaam { get; set; }

        [Display(Name = "Email")]
        //[Required(ErrorMessageResourceName = "EmailIsVerplicht",
       // ErrorMessageResourceType = typeof(Resources.ResourceNL))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "EmailIsVerplicht")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Totaal { get; set; }
        public List<ReservatieDetails> ReservatieDetails { get; set; }



        public string ToString(Reservatie reservatie)
        {
            
            StringBuilder bob = new StringBuilder();

            bob.Append("<p>Reservatie Informatie: " + reservatie.ReservatieId);
            bob.Append("<p>Naam: " + reservatie.Voornaam + " " + reservatie.Achternaam + "<br>");
            bob.Append("Email: " + reservatie.Email);

            bob.Append("<br>").AppendLine();
            bob.Append("<Table>").AppendLine();
  
            string header = "<tr> <th>Item Name</th>" + "<th>Quantity</th>" + "<th>Price</th> <th></th> </tr>";
            bob.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var reservatiedetail in reservatie.ReservatieDetails)
                {
                    bob.Append("<tr>");
                    output = "<td>" + reservatiedetail.ReservatieForms.ReservatieFormName + "</td>" + "<td>" + reservatiedetail.Hoeveelheid + "</td>" + "<td>" + reservatiedetail.Hoeveelheid * reservatiedetail.UnitPrijs + "</td>";
                    bob.Append(output).AppendLine();
                    Console.WriteLine(output);
                    bob.Append("</tr>");
                }
            }
            catch (Exception ex)
            {
                output = "No items ordered.";
            }
            bob.Append("</Table>");
            bob.Append("<b>");

            string footer = String.Format("{0,-12}{1,12}\n",
                                          "Total", reservatie.Totaal);
            bob.Append(footer).AppendLine();
            bob.Append("</b>");
            
            return bob.ToString();
        }

   
    }
}