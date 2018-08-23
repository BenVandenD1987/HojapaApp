using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HojapaApplication.Models
{
    public partial class ReservatieKaart
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        [Key]
        string ReservatieKaartsId { get; set; }
        public const string CartSessionKey = "KaartId";

        public static ReservatieKaart GetCart(HttpContextBase context)
        {
            var kaart = new ReservatieKaart();
            kaart.ReservatieKaartsId = kaart.GetCartId(context);
            return kaart;
        }


        public static ReservatieKaart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int AddToCart(ReservatieForm ReservatieForms)
        {

            var kaartItem = storeDB.Kaarten.SingleOrDefault(
                c => c.KaartId == ReservatieKaartsId
                && c.ReservatieFormId == ReservatieForms.ID);

            if (kaartItem == null)
            {

                kaartItem = new Kaart
                {
                    ReservatieFormId = ReservatieForms.ID,
                    KaartId = ReservatieKaartsId,
                    Optellen = 1,
                    CreateDatum = DateTime.Now
                };
                storeDB.Kaarten.Add(kaartItem);
            }
            else
            {

                kaartItem.Optellen++;
            }

            storeDB.SaveChanges();

            return kaartItem.Optellen;
        }

        public int RemoveFromCart(int id)
        {




            var kaartItem = storeDB.Kaarten.Single(
                cart => cart.KaartId == ReservatieKaartsId
                && cart.ReservatieFormId == id);


            int itemCount = 0;

            if (kaartItem != null)
            {
                if (kaartItem.Optellen > 1)
                {
                    kaartItem.Optellen--;
                    itemCount = kaartItem.Optellen;
                }
                else
                {
                    storeDB.Kaarten.Remove(kaartItem);
                }

                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Kaarten.Where(
                cart => cart.KaartId == ReservatieKaartsId);

            foreach (var kaartItem in cartItems)
            {
                storeDB.Kaarten.Remove(kaartItem);
            }

            storeDB.SaveChanges();
        }

        public List<Kaart> GetCartItems()
        {
            return storeDB.Kaarten.Where(
                cart => cart.KaartId == ReservatieKaartsId).ToList();
        }

        public int GetCount()
        {

            int? count = (from cartItems in storeDB.Kaarten
                          where cartItems.KaartId == ReservatieKaartsId
                          select (int?)cartItems.Optellen).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {

            decimal? total = (from cartItems in storeDB.Kaarten
                              where cartItems.KaartId == ReservatieKaartsId
                              select (int?)cartItems.Optellen *
                              cartItems.ReservatieForms.Prijs).Sum();

            return total ?? decimal.Zero;
        }

        public Reservatie CreateOrder(Reservatie reservatie)
        {
            decimal orderTotal = 0;
            reservatie.ReservatieDetails = new List<ReservatieDetails>();

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new ReservatieDetails
                {
                    ReservatieFormId = item.ReservatieFormId,
                    ReservatieId = reservatie.ReservatieId,
                    UnitPrijs = item.ReservatieForms.Prijs,
                    Hoeveelheid = item.Optellen
                };

                orderTotal += (item.Optellen * item.ReservatieForms.Prijs);
                reservatie.ReservatieDetails.Add(orderDetail);
                storeDB.ReservatieDetails.Add(orderDetail);

            }

            reservatie.Totaal = orderTotal;
            storeDB.SaveChanges();
            EmptyCart();
            return reservatie;
        }


        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {

                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }


        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Kaarten.Where(
                c => c.KaartId == ReservatieKaartsId);

            foreach (Kaart item in shoppingCart)
            {
                item.KaartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}