using System.Collections.Generic;
using System.Linq;
using Kurs.Storage;

namespace Kurs.Model.Data
{
    public class OperatingSystemData
    {
        public int Id
        {
            get { return OperatingSystem.Id; }
            set { OperatingSystem.Id = value; }
        }

        public string Title
        {
            get { return OperatingSystem.Title; }
            set { OperatingSystem.Title = value; }
        }

        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? OperatingSystem?.Devices?.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }


        internal OperatingSystem OperatingSystem { get; set; }

        bool IncludeDependency { get; set; }

        public OperatingSystemData()
        {
            OperatingSystem = new OperatingSystem();
            IncludeDependency = true;
        }

        internal OperatingSystemData(OperatingSystem operatingSystem, bool includeDependency = true)
        {
            OperatingSystem = operatingSystem;
            IncludeDependency = includeDependency;
        }
    }
}
