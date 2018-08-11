using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OpHomeSecurity.Web.Startup))]
namespace OpHomeSecurity.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
