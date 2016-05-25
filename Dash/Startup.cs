using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dash.Startup))]
namespace Dash
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
