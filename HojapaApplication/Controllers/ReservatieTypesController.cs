using HojapaApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HojapaApplication.Controllers
{
    [Authorize]
    public class ReservatieTypesController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var Categories = storeDB.Categories.ToList();

            return View(Categories);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string Categorie)
        {

            var CategorieModel = storeDB.Categories.Include("ReservatieForms")
                .Single(g => g.Name == Categorie);

            return View(CategorieModel);
        }



        // GET: /Store/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var item = storeDB.ReservatieForms.Find(id);

            return View(item);
        }

        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult CategorieMenu()
        {
            var Categories = storeDB.Categories.ToList();

            return PartialView(Categories);
        }
    }
}