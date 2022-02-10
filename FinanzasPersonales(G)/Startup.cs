using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinanzasPersonales_G_.Startup))]
namespace FinanzasPersonales_G_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
