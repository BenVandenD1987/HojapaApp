using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HojapaApplication.Startup))]
namespace HojapaApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
