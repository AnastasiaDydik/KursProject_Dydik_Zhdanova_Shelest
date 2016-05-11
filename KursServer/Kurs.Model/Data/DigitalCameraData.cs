using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class DigitalCameraData
    {
        public int Id
        {
            get { return DigitalCamera.Id; }
            set { DigitalCamera.Id = value; }
        }

        public int Height
        {
            get { return DigitalCamera.Height; }
            set { DigitalCamera.Height = value; }
        }
        public int Width
        {
            get { return DigitalCamera.Width; }
            set { DigitalCamera.Width = value; }
        }


        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? DigitalCamera?.Devices?.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal DigitalCamera DigitalCamera { get; set; }

        bool IncludeDependency { get; set; }

        public DigitalCameraData()
        {
            DigitalCamera = new DigitalCamera();
            IncludeDependency = true;
        }

        internal DigitalCameraData(DigitalCamera digitalCamera, bool includeDependency = true)
        {
            if (digitalCamera == null)
                throw new ArgumentNullException(nameof(digitalCamera));

            DigitalCamera = digitalCamera;
            IncludeDependency = includeDependency;
        }
    }
}
