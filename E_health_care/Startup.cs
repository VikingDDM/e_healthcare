using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_health_care.Startup))]
namespace E_health_care
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
