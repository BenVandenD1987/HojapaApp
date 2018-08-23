using HojapaApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HojapaApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<ApplicationDbContext>(new HojapaApplicationInitializer());

        }
        void Application_Error(object sender, EventArgs e)
        { // Attach break point here. 
            Exception TheError = Server.GetLastError(); //After execution of this line just check TheError variable. It will show you the details.
            Server.ClearError();
        }
    }
}
