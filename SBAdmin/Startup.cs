using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SBAdmin.Startup))]
namespace SBAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
