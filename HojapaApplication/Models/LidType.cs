using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HojapaApplication.Models
{
    public partial class LidType

    {
        [Key]
        public int ID { get; set; }

        public string Naam { get; set; }

        public bool isActive { get; set; }

        public virtual ICollection<Leden> Leden { get; set; }

    }
}