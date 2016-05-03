namespace Kurs.Admin.Repository
{
    public class Processor
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Cores { get; set; }

        public int Frequency { get; set; }

        /*public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? Processor?.Devices?.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal Processor Processor { get; set; }

        bool IncludeDependency { get; set; }

        public ProcessorData()
        {
            Processor = new Processor();
            IncludeDependency = true;
        }

        internal ProcessorData(Processor processor, bool includeDependency = true)
        {
            if (processor == null)
                throw new ArgumentNullException(nameof(processor));

            Processor = processor;
            IncludeDependency = includeDependency;
        }*/
    }
}
