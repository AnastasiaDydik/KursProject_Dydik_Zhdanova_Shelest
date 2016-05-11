namespace Kurs.Admin.Repository
{
    public class DigitalCamera
    {
        public int Id { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        /*
        public IEnumerable<DeviceData> Devices
        {
            get
            {
                return IncludeDependency ? DigitalCamera?.Devices?.ToArray().Select(d => new DeviceData(d, false)) : null;
            }
        }

        internal DigitalCamera DigitalCamera { get; set; }

        bool IncludeDependency { get; set; }

        public DigitalCamera()
        {
            DigitalCamera = new DigitalCamera();
            IncludeDependency = true;
        }

        internal DigitalCamera(DigitalCamera digitalCamera, bool includeDependency = true)
        {
            if (digitalCamera == null)
                throw new ArgumentNullException(nameof(digitalCamera));

            DigitalCamera = digitalCamera;
            IncludeDependency = includeDependency;
        }*/
    }
}
