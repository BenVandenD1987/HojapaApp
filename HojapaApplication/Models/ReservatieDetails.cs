using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HojapaApplication.Models
{
    public class ReservatieDetails
    {
        public int ReservatieDetailsId { get; set; }
        public int ReservatieId { get; set; }
        public int ReservatieFormId { get; set; }
        public int Hoeveelheid { get; set; }
        public decimal UnitPrijs { get; set; }
        public virtual ReservatieForm ReservatieForms { get; set; }
        public virtual Reservatie Reservatie { get; set; }
    }
}
