using Kurs.Model.Data;
using System.Collections.Generic;

namespace Kurs.Model
{
    public interface IDeviceModel
    {
        #region Device region

        IEnumerable<DeviceData> GetDevices
            (
                CategoryData category = null,
                ColorData color = null,
                CountryData country = null,
                DigitalCameraData digitalCamera = null,
                MakerData maker = null,
                OperatingSystemData operatingSystem = null,
                ProcessorData processor = null,
                ScreenResolutionData screenResolution = null,
                string reqestString = ""
            );

        bool Create(DeviceData device, int count);

        DeviceData FindDeviceById(int id);

        bool Delete(DeviceData device);

        bool Update(DeviceData device);

        #endregion

        #region Color region

        IEnumerable<ColorData> GetColors();

        bool Create(ColorData color);

        bool Update(ColorData color);

        bool Delete(ColorData color);

        ColorData FindColorById(int id);

        #endregion

        #region Country region

        IEnumerable<CountryData> GetCountries();

        bool Create(CountryData country);

        bool Update(CountryData country);

        bool Delete(CountryData country);

        CountryData FindCountryById(int id);

        #endregion

        #region Category region

        IEnumerable<CategoryData> GetCategories();

        bool Create(CategoryData category);

        bool Update(CategoryData category);

        bool Delete(CategoryData category);

        CategoryData FindCategoryById(int id);

        #endregion

        #region Digital camera region

        IEnumerable<DigitalCameraData> GetDigitalCameras();

        bool Create(DigitalCameraData digitalCamera);

        bool Update(DigitalCameraData digitalCamera);

        bool Delete(DigitalCameraData digitalCamera);

        DigitalCameraData FindDigitalCameraById(int id);

        #endregion

        #region Maker region

        IEnumerable<MakerData> GetMakers();

        bool Create(MakerData maker);

        bool Update(MakerData maker);

        bool Delete(MakerData maker);

        MakerData FindMakerById(int id);

        #endregion

        #region Operating system region

        IEnumerable<OperatingSystemData> GetOperatingSystems();

        bool Create(OperatingSystemData operatingSystem);

        bool Update(OperatingSystemData operatingSystem);

        bool Delete(OperatingSystemData operatingSystem);

        OperatingSystemData FindOperatingSystemById(int id);

        #endregion

        #region Processor region

        IEnumerable<ProcessorData> GetProcessors();

        bool Create(ProcessorData processor);

        bool Update(ProcessorData processor);

        bool Delete(ProcessorData processor);

        ProcessorData FindProcessorById(int id);

        #endregion

        #region Review region

        IEnumerable<ReviewData> GetReviews();

        bool Create(ReviewData review);

        bool Update(ReviewData review);

        bool Delete(ReviewData review);

        ReviewData FindReviewById(int id);

        #endregion

        #region Screen resolution region

        IEnumerable<ScreenResolutionData> GetScreenResolutions();

        bool Create(ScreenResolutionData screenResolution);

        bool Update(ScreenResolutionData screenResolution);

        bool Delete(ScreenResolutionData screenResolution);

        ScreenResolutionData FindScreenResolutionById(int id);

        #endregion
    }
}
