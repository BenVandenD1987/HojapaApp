using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HojapaApplication.ViewModel
{
    public class ReservatieKaartRemoveViewModel
    {
        public string Bericht { get; set; }
        public decimal KaartTotaal { get; set; }
        public int KaartOptelling { get; set; }
        public int ReservatieFormTelling { get; set; }
        public int DeleteId { get; set; }
    }
}