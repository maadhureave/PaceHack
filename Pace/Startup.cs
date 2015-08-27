using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pace.Startup))]
namespace Pace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
