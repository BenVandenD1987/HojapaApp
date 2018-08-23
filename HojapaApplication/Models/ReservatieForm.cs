using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Mvc.Html;

namespace HojapaApplication.Models
{
    public class ReservatieForm
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Categorie")]
        public int CategorieId { get; set; }

       [Display(Name = "ReservatieFormName")]
        public string ReservatieFormName { get; set; }

        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }


        [Display(Name = "Prijs")]
        public decimal Prijs { get; set; }


        /*public byte[] InternalImage { get; set; }

        [Display(Name = "Local file")]
        [NotMapped]
        public HttpPostedFileBase File
        {
            get
            {
                return null;
            }
            set
            {
                try
                {
                    MemoryStream target = new MemoryStream();
                    if (value.InputStream == null)
                        return;
                    value.InputStream.CopyTo(target);
                    InternalImage = target.ToArray();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
        }

        [Display(Name = "Picture", ResourceType = typeof(Resources.ResourceNL))]
        public string ItemPictureUrl { get; set; }*/

        public virtual Categorie Categorie { get; set; }
        public virtual List<ReservatieDetails> ReservatieDetails { get; set; }
        public virtual Productie Productie { get; set; }
        }
    }