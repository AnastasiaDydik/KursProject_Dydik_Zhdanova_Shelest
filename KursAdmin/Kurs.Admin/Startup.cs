using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kurs.Admin.Startup))]
namespace Kurs.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }
    }
}
