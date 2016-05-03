using Kurs.Storage;
using Ninject.Modules;

namespace Kurs.Model
{
    public class ModelsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<KursDbEntities>().To<KursDbEntities>();
            Bind<IDeviceModel>().To<DeviceModel>();
        }
    }
}
