using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testApplicationNadina.Startup))]
namespace testApplicationNadina
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
