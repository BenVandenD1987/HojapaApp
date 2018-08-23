using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HojapaApplication.Models
{
    public class ProductieStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Naam{get; set;}
    }
}