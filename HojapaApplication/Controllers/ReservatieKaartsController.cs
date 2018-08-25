using HojapaApplication.Models;
using HojapaApplication.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HojapaApplication.Controllers
{
    public class ReservatieKaartsController : Controller
    {

        ApplicationDbContext storeDB = new ApplicationDbContext();
        //
        // GET: /ReservatieKaart/
        public ActionResult Index()
        {
            var cart = ReservatieKaart.GetCart(this.HttpContext);


            var viewModel = new ReservatieKaartViewModel
            {
                KaartItem = cart.GetCartItems(),
                KaartTotaal = cart.GetTotal()
            };

            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id)
        {

            var addedItem = storeDB.ReservatieForms
                .Single(item => item.ID == id);


            var cart = ReservatieKaart.GetCart(this.HttpContext);

            int count = cart.AddToCart(addedItem);


            var results = new ReservatieKaartRemoveViewModel
            {
                Bericht = Server.HtmlEncode(addedItem.ReservatieFormName) +
                    " is toegevoegd aan uw kaart.",
                KaartTotaal = cart.GetTotal(),
                KaartOptelling = cart.GetCount(),
                ReservatieFormTelling = count,
                DeleteId = id
            };
            return Json(results);


        }
        //
        // /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {

            var cart = ReservatieKaart.GetCart(this.HttpContext);


            string itemName = storeDB.ReservatieForms
                .Single(item => item.ID == id).ReservatieFormName;


            int itemCount = cart.RemoveFromCart(id);


            var results = new ReservatieKaartRemoveViewModel
            {
                Bericht = "Een  " + Server.HtmlEncode(itemName) +
                    " is verwijderd.",
                KaartTotaal = cart.GetTotal(),
                KaartOptelling = cart.GetCount(),
                ReservatieFormTelling = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ReservatieKaart.GetCart(this.HttpContext);

            ViewData["KaartOptelling"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}