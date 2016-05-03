using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class CountryData
    {
        public int Id
        {
            get { return Country.Id; }
            set { Country.Id = value; }
        }

        public string Title
        {
            get { return Country.Title; }
            set { Country.Title = value; }
        }

        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? Country?.Devices?.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal Country Country { get; set; }

        bool IncludeDependency { get; set; }

        public CountryData()
        {
            Country = new Country();
            IncludeDependency = true;
        }

        internal CountryData(Country country, bool includeDependency = true)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            Country = country;
            IncludeDependency = includeDependency;
        }
    }
}
