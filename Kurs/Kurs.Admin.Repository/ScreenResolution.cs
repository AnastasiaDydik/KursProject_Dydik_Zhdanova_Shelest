namespace Kurs.Admin.Repository
{
    public class ScreenResolution
    {
        public int Id { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public float Diagonal { get; set; }

        /*  public IEnumerable<DeviceData> Devices
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
          }*/
    }
}
