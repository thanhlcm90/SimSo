using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimSo.Startup))]
namespace SimSo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
