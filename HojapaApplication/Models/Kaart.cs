using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HojapaApplication.Models
{
    public class Kaart
    {
        [Key]
        public int ID { get; set; }
        public string KaartId { get; set; }
        public int ReservatieFormId { get; set; }
        public int Optellen { get; set; }
        public System.DateTime CreateDatum { get; set; }
        public virtual ReservatieForm ReservatieForms { get; set; }
    }
}