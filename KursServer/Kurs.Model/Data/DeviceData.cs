using Kurs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurs.Model.Data
{
    public class DeviceData
    {
        public int Id
        {
            get { return Device.Id; }
            set { Device.Id = value; }
        }

        public string Model
        {
            get { return Device.Model; }
            set { Device.Model = value; }
        }

        public Nullable<double> Heigth
        {
            get { return Device.Heigth; }
            set { Device.Heigth = value; }
        }


        public Nullable<double> Width
        {
            get { return Device.Width; }
            set { Device.Width = value; }
        }

        public decimal Price
        {
            get { return Device.Price; }
            set { Device.Price = value; }
        }

        public Nullable<int> Ram
        {
            get { return Device.Ram; }
            set { Device.Ram = value; }
        }

        public Nullable<int> Memory
        {
            get { return Device.Memory; }
            set { Device.Memory = value; }
        }

        public string Info
        {
            get { return Device.Info; }
            set { Device.Info = value; }
        }

        public string Image
        {
            get { return Device.Image; }
            set { Device.Image = value; }
        }

        public int TotalCount
        {
            get { return Device.TotalCount; }
            internal set { Device.TotalCount = value; }
        }

        public int FreeCount
        {
            get { return Device.FreeCount; }
            internal set { Device.FreeCount = value; }
        }

        internal Device Device { get; set; }

        public CategoryData Category
        {
            get { return IncludeDependency && Device != null && Device.Category != null ? new CategoryData(Device.Category, false) : null; }
            set { Device.Category = value.Category; }
        }

        public MakerData Maker
        {
            get { return IncludeDependency && Device != null && Device.Maker != null ? new MakerData(Device.Maker, false) : null; }
            set { Device.Maker = value.Maker; }
        }


        public ScreenResolutionData ScreenResolution
        {
            get { return IncludeDependency && Device != null && Device.ScreenResolution != null ? new ScreenResolutionData(Device.ScreenResolution, false) : null; }
            set { Device.ScreenResolution = value.ScreenResolution; }
        }

        public OperatingSystemData OperatingSystem
        {
            get { return IncludeDependency && Device != null && Device.OperatingSystem != null ? new OperatingSystemData(Device.OperatingSystem, false) : null; }
            set { Device.OperatingSystem = value.OperatingSystem; }
        }


        public ProcessorData Processor
        {
            get { return IncludeDependency && Device != null && Device.Processor != null ? new ProcessorData(Device.Processor, false) : null; }
            set { Device.Processor = value.Processor; }
        }

        public DigitalCameraData DigitalCamera
        {
            get { return IncludeDependency && IncludeDependency && Device != null && Device.DigitalCamera != null ? new DigitalCameraData(Device.DigitalCamera, false) : null; }
            set { Device.DigitalCamera = value.DigitalCamera; }
        }

        public ColorData Color
        {
            get { return IncludeDependency && Device != null && Device.Color != null ? new ColorData(Device.Color, false) : null; }
            set { Device.Color = value.Color; }
        }

        public CountryData Country
        {
            get { return IncludeDependency && Device != null && Device.Country != null ? new CountryData(Device.Country, false) : null; }
            set { Device.Country = value.Country; }
        }

        public IEnumerable<ReviewData> Reviews
        {
            get { return IncludeDependency && Device != null && Device.Reviews != null ? Device.Reviews.ToArray().Select(review => new ReviewData(review, false)) : null; }
        }

        bool IncludeDependency { get; set; }

        internal DeviceData(Device device, bool includeDependency = true)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            Device = device;
            IncludeDependency = includeDependency;
        }

        public DeviceData()
        {
            Device = new Device();
            IncludeDependency = true;
        }
    }
}
