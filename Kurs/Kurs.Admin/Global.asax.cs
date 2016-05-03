using Kurs.Admin.Repository;
using Ninject;
using Ninject.Web.Common;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Kurs.Admin
{
    public class MvcApplication : NinjectHttpApplication
    {

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            
            kernel.Load(new RepositoryModule());
            //kernel.Bind<IKursRepository>().To<Kur>();
            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            base.OnApplicationStarted();
        }
    }
}
