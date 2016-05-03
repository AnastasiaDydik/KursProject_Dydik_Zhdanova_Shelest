using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class MakerData
    {
        public int Id
        {
            get { return Maker.Id; }
            set { Maker.Id = value; }
        }

        public string Title
        {
            get { return Maker.Title; }
            set { Maker.Title = value; }
        }

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

    }
}
