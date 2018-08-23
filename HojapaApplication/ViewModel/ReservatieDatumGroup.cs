using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HojapaApplication.ViewModel
{
    public class ReservatieDatumGroup
    {
        [DataType(DataType.Date)]
        public DateTime? ReservatieDatum { get; set; }

        public int ReservatieTelling { get; set; }
    }
}