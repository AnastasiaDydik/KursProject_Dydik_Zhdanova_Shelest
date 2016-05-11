using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class ColorData
    {
        public int Id
        {
            get { return Color.Id; }
            set { Color.Id = value; }
        }

        public string Title
        {
            get { return Color.Title; }
            set { Color.Title = value; }
        }

        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? Color?.Devices.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal Color Color { get; set; }

        bool IncludeDependency { get; set; }

        public ColorData()
        {
            Color = new Color();
        }

        internal ColorData(Color color, bool includeDependency = true)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            Color = color;

            IncludeDependency = includeDependency;
        }
    }
}
