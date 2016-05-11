using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class ScreenResolutionData
    {
        public int Id
        {
            get { return ScreenResolution.Id; }
            set { ScreenResolution.Id = value; }
        }

        public double Width
        {
            get { return ScreenResolution.Width; }
            set { ScreenResolution.Width = value; }
        }

        public double Heigdgt
        {
            get { return ScreenResolution.Heigdgt; }
            set { ScreenResolution.Heigdgt = value; }
        }

        public float Diagonal
        {
            get { return ScreenResolution.Diagonal; }
            set { ScreenResolution.Diagonal = value; }
        }

        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? ScreenResolution?.Devices?.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal ScreenResolution ScreenResolution { get; set; }

        bool IncludeDependency { get; set; }

        public ScreenResolutionData()
        {
            ScreenResolution = new ScreenResolution();
            IncludeDependency = true;
        }

        internal ScreenResolutionData(ScreenResolution screenResolution, bool includeDependency = true)
        {
            if (screenResolution == null)
                throw new ArgumentNullException(nameof(screenResolution));

            ScreenResolution = screenResolution;
            IncludeDependency = includeDependency;
        }
    }
}
