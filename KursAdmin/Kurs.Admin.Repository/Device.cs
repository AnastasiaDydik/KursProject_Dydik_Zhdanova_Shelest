using System;

namespace Kurs.Admin.Repository
{
    public class Device
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public Nullable<double> Heigth { get; set; }


        public Nullable<double> Width { get; set; }

        public decimal Price { get; set; }

        public Nullable<int> Ram { get; set; }

        public Nullable<int> Memory { get; set; }

        public string Info { get; set; }

        public string Image { get; set; }

        public int TotalCount { get; set; }

        public int FreeCount { get; set; }

        public int CategoryId { get; set; }

        public int? ColorId { get; set; }

        public int MakerId { get; set; }

        public int? ScreenResolutionId { get; set; }

        public int? ProcessorId { get; set; }

        public int? OperatingSystemId { get; set; }

        public int? DigitalCameraId { get; set; }

        public int CountryId { get; set; }

        public int? MaterialId { get; set; }




/*
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
        }*/
    }
}
