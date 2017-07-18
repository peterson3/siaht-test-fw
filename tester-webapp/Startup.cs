using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tester_webapp.Startup))]
namespace tester_webapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
