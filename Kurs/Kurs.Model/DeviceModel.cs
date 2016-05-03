using System;
using System.Collections.Generic;
using Kurs.Model.Data;
using Kurs.Storage;
using System.Linq;

namespace Kurs.Model
{
    internal class DeviceModel : IDeviceModel
    {
        readonly KursDbEntities DbContext;

        public DeviceModel(KursDbEntities dbContext)
        {
            DbContext = dbContext;
        }

        #region Device region

        public bool Create(DeviceData device, int count)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (device.Device == null)
                throw new ArgumentException("Неверно создан объект", nameof(device));

            device.TotalCount = count;

            DbContext.Devices.Add(device.Device);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(DeviceData device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (device.Device == null)
                throw new ArgumentException("Неверно загружен объект", nameof(device));

            DbContext.Devices.Remove(device.Device);
            return DbContext.SaveChanges() > 0;
        }

        public IEnumerable<DeviceData> GetDevices(CategoryData category = null,
            ColorData color = null,
            CountryData country = null,
            DigitalCameraData digitalCamera = null,
            MakerData maker = null,
            OperatingSystemData operatingSystem = null,
            ProcessorData processor = null,
            ScreenResolutionData screenResolution = null,
            string reqestString = "")
        {
            var devices = DbContext.Devices.Select(it => it);

            if (category != null)
                devices = devices.Where(it => it.CategoryId == category.Id);

            if (color != null)
                devices = devices.Where(it => it.ColorId == color.Id);

            if (country != null)
                devices = devices.Where(it => it.CountryId == country.Id);

            if (digitalCamera != null)
                devices = devices.Where(it => it.DigitalCameraId == digitalCamera.Id);

            if (maker != null)
                devices = devices.Where(it => it.MakerId == maker.Id);

            if (operatingSystem != null)
                devices = devices.Where(it => it.OperatingSystemId == operatingSystem.Id);

            if (processor != null)
                devices = devices.Where(it => it.ProcessorId == processor.Id);

            if (screenResolution != null)
                devices = devices.Where(it => it.ScreenResolutionId == screenResolution.Id);

            if (!string.IsNullOrWhiteSpace(reqestString))
                devices = devices.Where(it => it.Info.Contains(reqestString) || it.Model.Contains(reqestString));

            return devices.ToArray().Select(it => new DeviceData(it));
        }

        public bool Update(DeviceData device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));
            if (device.Device == null)
                throw new ArgumentException("Неверно загружен объект", nameof(device));

            return DbContext.SaveChanges() > 0;
        }

        public DeviceData FindDeviceById(int id)
        {
            var device = DbContext.Devices.SingleOrDefault(it => it.Id == id);
            return device != null ? new DeviceData(device) : null;
        }

        #endregion

        #region Country region

        public bool Create(CountryData country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            if (country.Country == null)
                throw new ArgumentException("Неверно создан объект", nameof(country));

            DbContext.Countries.Add(country.Country);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(CountryData country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            if (country.Country == null)
                throw new ArgumentException("Неверно загружен объект", nameof(country));

            DbContext.Countries.Remove(country.Country);
            return DbContext.SaveChanges() > 0;
        }

        public IEnumerable<CountryData> GetCountries()
        {
            return DbContext.Countries.ToArray().Select(it => new CountryData(it));
        }

        public CountryData FindCountryById(int id)
        {
            var country = DbContext.Countries.SingleOrDefault(it => it.Id == id);
            return country != null ? new CountryData(country) : null;
        }

        public bool Update(CountryData country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            if (country.Country == null)
                throw new ArgumentException("Неверно загружен объект", nameof(country));

            return DbContext.SaveChanges() > 0;
        }

        #endregion

        #region Digital camera region

        public bool Create(DigitalCameraData digitalCamera)
        {
            if (digitalCamera == null)
                throw new ArgumentNullException(nameof(digitalCamera));

            if (digitalCamera.DigitalCamera == null)
                throw new ArgumentException("Неверно создан объект", nameof(digitalCamera));

            DbContext.DigitalCameras.Add(digitalCamera.DigitalCamera);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(DigitalCameraData digitalCamera)
        {
            if (digitalCamera == null)
                throw new ArgumentNullException(nameof(digitalCamera));

            if (digitalCamera.DigitalCamera == null)
                throw new ArgumentException("Неверно загружен объект", nameof(digitalCamera));

            DbContext.DigitalCameras.Remove(digitalCamera.DigitalCamera);
            return DbContext.SaveChanges() > 0;
        }

        public IEnumerable<DigitalCameraData> GetDigitalCameras()
        {
            return DbContext.DigitalCameras.ToArray().Select(camera => new DigitalCameraData(camera));
        }

        public DigitalCameraData FindDigitalCameraById(int id)
        {
            var camera = DbContext.DigitalCameras.SingleOrDefault(it => it.Id == id);
            return camera != null ? new DigitalCameraData(camera) : null;
        }

        public bool Update(DigitalCameraData digitalCamera)
        {
            if (digitalCamera == null)
                throw new ArgumentNullException(nameof(digitalCamera));
            if (digitalCamera.DigitalCamera == null)
                throw new ArgumentException("Неверно загружен объект", nameof(digitalCamera));

            return DbContext.SaveChanges() > 0;
        }

        #endregion

        #region Maker region

        public bool Create(MakerData maker)
        {
            if (maker == null)
                throw new ArgumentNullException(nameof(maker));

            if (maker.Maker == null)
                throw new ArgumentException("Неверно создан объект", nameof(maker));

            DbContext.Makers.Add(maker.Maker);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(MakerData maker)
        {
            if (maker == null)
                throw new ArgumentNullException(nameof(maker));

            if (maker.Maker == null)
                throw new ArgumentException("Неверно загружен объект", nameof(maker));

            DbContext.Makers.Remove(maker.Maker);
            return DbContext.SaveChanges() > 0;
        }

        public MakerData FindMakerById(int id)
        {
            var maker = DbContext.Makers.SingleOrDefault(it => it.Id == id);
            return maker != null ? new MakerData(maker) : null;
        }

        public IEnumerable<MakerData> GetMakers()
        {
            return DbContext.Makers.ToArray().Select(it => new MakerData(it));
        }

        public bool Update(MakerData maker)
        {
            if (maker == null)
                throw new ArgumentNullException(nameof(maker));
            if (maker.Maker == null)
                throw new ArgumentException("Неверно загружен объект", nameof(maker));

            return DbContext.SaveChanges() > 0;
        }


        #endregion

        #region Processor region

        public bool Create(ProcessorData processor)
        {
            if (processor == null)
                throw new ArgumentNullException(nameof(processor));

            if (processor.Processor == null)
                throw new ArgumentException("Неверно создан объект", nameof(processor));

            DbContext.Processors.Add(processor.Processor);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(ProcessorData processor)
        {
            if (processor == null)
                throw new ArgumentNullException(nameof(processor));

            if (processor.Processor == null)
                throw new ArgumentException("Неверно загружен объект", nameof(processor));

            DbContext.Processors.Remove(processor.Processor);
            return DbContext.SaveChanges() > 0;
        }

        public ProcessorData FindProcessorById(int id)
        {
            var processor = DbContext.Processors.SingleOrDefault(it => it.Id == id);
            return processor != null ? new ProcessorData(processor) : null;
        }

        public IEnumerable<ProcessorData> GetProcessors()
        {
            return DbContext.Processors.ToArray().Select(it => new ProcessorData(it));
        }

        public bool Update(ProcessorData processor)
        {
            if (processor == null)
                throw new ArgumentNullException(nameof(processor));
            if (processor.Processor == null)
                throw new ArgumentException("Неверно загружен объект", nameof(processor));

            return DbContext.SaveChanges() > 0;
        }

        #endregion

        #region Screen resolution region

        public bool Create(ScreenResolutionData screenResolution)
        {
            if (screenResolution == null)
                throw new ArgumentNullException(nameof(screenResolution));

            if (screenResolution.ScreenResolution == null)
                throw new ArgumentException("Неверно создан объект", nameof(screenResolution));

            DbContext.ScreenResolutions.Add(screenResolution.ScreenResolution);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(ScreenResolutionData screenResolution)
        {
            if (screenResolution == null)
                throw new ArgumentNullException(nameof(screenResolution));

            if (screenResolution.ScreenResolution == null)
                throw new ArgumentException("Неверно загружен объект", nameof(screenResolution));

            DbContext.ScreenResolutions.Remove(screenResolution.ScreenResolution);
            return DbContext.SaveChanges() > 0;
        }

        public ScreenResolutionData FindScreenResolutionById(int id)
        {
            var screen = DbContext.ScreenResolutions.SingleOrDefault(it => it.Id == id);
            return screen != null ? new ScreenResolutionData(screen) : null;
        }

        public IEnumerable<ScreenResolutionData> GetScreenResolutions()
        {
            return DbContext.ScreenResolutions.ToArray().Select(it => new ScreenResolutionData(it));
        }

        public bool Update(ScreenResolutionData screenResolution)
        {
            if (screenResolution == null)
                throw new ArgumentNullException(nameof(screenResolution));
            if (screenResolution.ScreenResolution == null)
                throw new ArgumentException("Неверно загружен объект", nameof(screenResolution));

            return DbContext.SaveChanges() > 0;
        }

        #endregion

        #region Review region

        public bool Create(ReviewData review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            if (review.Review == null)
                throw new ArgumentException("Неверно создан объект", nameof(review));

            DbContext.Reviews.Add(review.Review);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(ReviewData review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            if (review.Review == null)
                throw new ArgumentException("Неверно загружен объект", nameof(review));

            DbContext.Reviews.Remove(review.Review);
            return DbContext.SaveChanges() > 0;
        }

        public ReviewData FindReviewById(int id)
        {
            var review = DbContext.Reviews.SingleOrDefault(it => it.Id == id);
            return review != null ? new ReviewData(review) : null;
        }

        public IEnumerable<ReviewData> GetReviews()
        {
            return DbContext.Reviews.ToArray().Select(it => new ReviewData(it));
        }

        public bool Update(ReviewData review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));
            if (review.Review == null)
                throw new ArgumentException("Неверно загружен объект", nameof(review));

            return DbContext.SaveChanges() > 0;
        }

        #endregion

        #region Operation system region

        public bool Create(OperatingSystemData operatingSystem)
        {
            if (operatingSystem == null)
                throw new ArgumentNullException(nameof(operatingSystem));

            if (operatingSystem.OperatingSystem == null)
                throw new ArgumentException("Неверно создан объект", nameof(operatingSystem));

            DbContext.OperatingSystems.Add(operatingSystem.OperatingSystem);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(OperatingSystemData operatingSystem)
        {
            if (operatingSystem == null)
                throw new ArgumentNullException(nameof(operatingSystem));

            if (operatingSystem.OperatingSystem == null)
                throw new ArgumentException("Неверно загружен объект", nameof(operatingSystem));

            DbContext.OperatingSystems.Remove(operatingSystem.OperatingSystem);
            return DbContext.SaveChanges() > 0;
        }

        public OperatingSystemData FindOperatingSystemById(int id)
        {
            var os = DbContext.OperatingSystems.SingleOrDefault(it => it.Id == id);
            return os != null ? new OperatingSystemData(os) : null;
        }

        public bool Update(OperatingSystemData operatingSystem)
        {
            if (operatingSystem == null)
                throw new ArgumentNullException(nameof(operatingSystem));
            if (operatingSystem.OperatingSystem == null)
                throw new ArgumentException("Неверно загружен объект", nameof(operatingSystem));

            return DbContext.SaveChanges() > 0;
        }

        public IEnumerable<OperatingSystemData> GetOperatingSystems()
        {
            return DbContext.OperatingSystems.ToArray().Select(it => new OperatingSystemData(it));
        }

        #endregion

        #region Category region

        public bool Create(CategoryData category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (category.Category == null)
                throw new ArgumentException("Неверно создан объект", nameof(category));

            DbContext.Categories.Add(category.Category);
            return DbContext.SaveChanges() > 0;
        }

        public IEnumerable<CategoryData> GetCategories()
        {
            return DbContext.Categories.ToArray().Select(it => new CategoryData(it));
        }

        public bool Update(CategoryData category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            if (category.Category == null)
                throw new ArgumentException("Неверно загружен объект", nameof(category));

            return DbContext.SaveChanges() > 0;
        }

        CategoryData IDeviceModel.FindCategoryById(int id)
        {
            var category = DbContext.Categories.SingleOrDefault(it => it.Id == id);
            return category != null ? new CategoryData(category) : null;
        }

        public bool Delete(CategoryData category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            if (category.Category == null)
                throw new ArgumentException("Неверно загружен объект", nameof(category));

            DbContext.Categories.Remove(category.Category);
            return DbContext.SaveChanges() > 0;
        }

        #endregion

        #region Color region

        public bool Create(ColorData color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            if (color.Color == null)
                throw new ArgumentException("Неверно создан объект", nameof(color));

            DbContext.Colors.Add(color.Color);
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(ColorData color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            if (color.Color == null)
                throw new ArgumentException("Неверно загружен объект", nameof(color));

            DbContext.Colors.Remove(color.Color);
            return DbContext.SaveChanges() > 0;
        }

        public ColorData FindColorById(int id)
        {
            var color = DbContext.Colors.SingleOrDefault(it => it.Id == id);
            return color != null ? new ColorData(color) : null;
        }

        public IEnumerable<ColorData> GetColors()
        {
            return DbContext.Colors.ToArray().Select(it => new ColorData(it));
        }

        public bool Update(ColorData color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));
            if (color.Color == null)
                throw new ArgumentException("Неверно загружен объект", nameof(color));

            return DbContext.SaveChanges() > 0;
        }

        #endregion
    }
}
