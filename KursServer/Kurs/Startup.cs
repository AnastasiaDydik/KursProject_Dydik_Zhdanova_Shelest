using Kurs.Model;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(Kurs.Startup))]

namespace Kurs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(WebApiConfig.Register());
            
            ConfigureAuth(app);
        }

        protected IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new ModelsModule());
            return kernel;
        }
    }
}
