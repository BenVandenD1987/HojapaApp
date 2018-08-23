using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace HojapaApplication.Models
{
    public class TestViewModel
    {
       public IEnumerable<Leden> tblLidTypes { get; set; }
      public IEnumerable<Leden> Sorterens { get; set; }
    }
}