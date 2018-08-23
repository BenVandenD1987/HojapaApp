using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HojapaApplication.Models
{
    
    public class Categorie
    {
        [Key]
        [DisplayName("Categorie ID")]
        public int ID { get; set; }

        [DisplayName("Categorie")]
        public string Name { get; set; }

        public virtual ICollection<ReservatieForm> ReservatieForms { get; set; }
    }
}