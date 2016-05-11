namespace Kurs.Admin.Repository
{
    public class Maker
    {
        public int Id { get; set; }

        public string Title { get; set; }
/*
        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? Maker?.Devices?.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal Maker Maker { get; set; }

        bool IncludeDependency { get; set; }

        public MakerData()
        {
            Maker = new Maker();
            IncludeDependency = true;
        }

        internal MakerData(Maker maker, bool includeDependency = true)
        {
            if (maker == null)
                throw new ArgumentNullException(nameof(maker));

            Maker = maker;
            IncludeDependency = includeDependency;
        }
        */
    }
}
