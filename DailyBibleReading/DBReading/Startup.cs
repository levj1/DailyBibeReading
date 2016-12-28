using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DBReading.Startup))]
namespace DBReading
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
