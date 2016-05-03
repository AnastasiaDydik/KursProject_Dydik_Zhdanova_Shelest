namespace Kurs.Admin.Repository
{
    public class OperatingSystem
    {
        public int Id { get; set; }

        public string Title { get; set; }

       /* public IEnumerable<DeviceData> Devices
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
        }*/
    }
}
