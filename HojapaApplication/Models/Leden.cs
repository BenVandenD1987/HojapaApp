using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HojapaApplication.Models
{
    public class Leden
    {

        [Key]
        public int ID { get; set; }

        public string SoortLid { get; set; }

        [Required]
        public string Naam { get; set; }

        public string Voornaam { get; set; }

        public GeslachtType Geslacht { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Geboortedatum { get; set; }

        public string Adres { get; set; }

        public int Postcode { get; set; }

        public string Gemeente { get; set; }

        public int Nummer { get; set; }

        public string BusNummer { get; set; }

        public string Telefoon { get; set; }

        public string Gsm { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public virtual ICollection<LidType> LidTypes { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Productie> Producties { get; set; }

        public virtual List<Productie> ProductieList { get; set; }

        public List<int> ProductieIds { get; set; }






        public enum GeslachtType
        {
            Man,
            Vrouw
        }

    }
}
