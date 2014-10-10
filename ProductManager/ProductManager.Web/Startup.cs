using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductManager.Web.Startup))]
namespace ProductManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
