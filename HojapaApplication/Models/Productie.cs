
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace HojapaApplication.Models
{
    public class Productie
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductieNaam { get; set; }

        public string Auteur { get; set; }

        public string Regisseur { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        public int AantalToeschouwers { get; set; }

        public List<int> LedenIds { get; set; }

        public virtual List<Leden> LedenList { get; set; }

        public int ProductieStatusId { get; set; }

        [ForeignKey("ProductieStatusId")]
        public ProductieStatus Status { get; set; }

        public virtual List<ProductieStatus> StatusList { get; set; }

       public virtual ICollection<Leden> LedenCast { get; set; }

        public virtual string LedenDescription
        {
            get
            {
                string result = "";
                foreach (Leden t in LedenList)
                {
                    result += t.Naam + ", ";
                }
                if (result.EndsWith(", ")) result = result.Substring(0, result.Length - ", ".Length);

                return result;
            }
        }

        public virtual string ProductieStatusList
        {
            get
            {
                string result = "";
                foreach (ProductieStatus t in StatusList)
                {
                    result = t.Naam;
                }
                if (result.EndsWith(", ")) result = result.Substring(0, result.Length - ", ".Length);

                return result;
            }
        }


    }
}
