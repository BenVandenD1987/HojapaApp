using HojapaApplication.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HojapaApplication.Models {
   
    public class ApplicationUser : IdentityUser {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

   [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Leden> Leden { get; set; }
        public DbSet<Productie> Producties { get; set; }
        public DbSet<LidType> LidTypes { get; set; }
        public DbSet<ProductieStatus> ProductieStatus { get; set; }

        /*TEST*/

        public DbSet<ReservatieForm> ReservatieForms { get; set; }
        public DbSet<Kaart> Kaarten { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<ReservatieDetails> ReservatieDetails { get; set; }



        static ApplicationDbContext() {
            Database.SetInitializer<ApplicationDbContext>(new HojapaApplicationInitializer());
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}