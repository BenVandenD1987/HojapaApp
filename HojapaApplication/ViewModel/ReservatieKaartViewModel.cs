using HojapaApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HojapaApplication.ViewModel
{
    public class ReservatieKaartViewModel
    {
        [Key]
        public List<Kaart> KaartItem { get; set; }
        public decimal KaartTotaal { get; set; }
    }
}