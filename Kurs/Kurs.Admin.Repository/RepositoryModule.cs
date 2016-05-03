using Ninject.Modules;

namespace Kurs.Admin.Repository
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IKursRepository>().To<KursRepository>();
        }
    }
}
