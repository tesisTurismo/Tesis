using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tesis.Web.Startup))]
namespace Tesis.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
