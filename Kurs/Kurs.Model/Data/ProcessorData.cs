using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class ProcessorData
    {
        public int Id
        {
            get { return Processor.Id; }
            set { Processor.Id = value; }
        }

        public string Title
        {
            get { return Processor.Title; }
            set { Processor.Title = value; }
        }

        public int Cores
        {
            get { return Processor.Cores; }
            set { Processor.Cores = value; }
        }

        public int Frequency
        {
            get { return Processor.Frequency; }
            set { Processor.Frequency = value; }
        }

        public IEnumerable<DeviceData> Devices
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
        }
    }
}
