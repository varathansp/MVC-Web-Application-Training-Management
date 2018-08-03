using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PleaseStayWithUs.Startup))]
namespace PleaseStayWithUs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
