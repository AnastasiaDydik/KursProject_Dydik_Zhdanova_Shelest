using System.Collections.Generic;

namespace Kurs.Admin.Repository
{
    public interface IKursRepository
    {
        #region Devices region

        IEnumerable<Device> Devices(int? cat = null, decimal? minPrice = null, decimal? maxPrice = null, string keyword = null);

        Device FindDeviceById(int id);

        bool Update(int id, Device device);

        Device Create(Device device);

        bool Delete(Device device);

        #endregion

        #region Categories region

        IEnumerable<Category> Categories { get; }

        Category FindCategoryById(int id);

        bool Update(int id, Category Category);

        Category Create(Category Category);

        bool Delete(Category Category);

        #endregion

        #region Colors region

        IEnumerable<Color> Colors { get; }

        Color FindColorById(int id);

        bool Update(int id, Color Color);

        Color Create(Color Color);

        bool Delete(Color Color);

        #endregion

        #region Consultants region

        IEnumerable<Consultant> Consultants { get; }

        Consultant FindConsultantById(int id);

        bool Update(int id, Consultant Consultant);

        Consultant Create(Consultant Consultant);

        bool Delete(Consultant Consultant);

        #endregion

        #region Countries region

        IEnumerable<Country> Countries { get; }

        Country FindCountryById(int id);

        bool Update(int id, Country Country);

        Country Create(Country Country);

        bool Delete(Country Country);

        #endregion

        #region DigitalCameras region

        IEnumerable<DigitalCamera> DigitalCameras { get; }

        DigitalCamera FindDigitalCameraById(int id);

        bool Update(int id, DigitalCamera DigitalCamera);

        DigitalCamera Create(DigitalCamera DigitalCamera);

        bool Delete(DigitalCamera DigitalCamera);

        #endregion

        #region Makers region

        IEnumerable<Maker> Makers { get; }

        Maker FindMakerById(int id);

        bool Update(int id, Maker Maker);

        Maker Create(Maker Maker);

        bool Delete(Maker Maker);

        #endregion

        #region OperatingSystems region

        IEnumerable<OperatingSystem> OperatingSystems { get; }

        OperatingSystem FindOperatingSystemById(int id);

        bool Update(int id, OperatingSystem OperatingSystem);

        OperatingSystem Create(OperatingSystem OperatingSystem);

        bool Delete(OperatingSystem OperatingSystem);

        #endregion

        #region Processors region

        IEnumerable<Processor> Processors { get; }

        Processor FindProcessorById(int id);

        bool Update(int id, Processor Processor);

        Processor Create(Processor Processor);

        bool Delete(Processor Processor);

        #endregion

        #region Reviews region

        IEnumerable<Review> Reviews { get; }

        Review FindReviewById(int id);

        bool Update(int id, Review Review);

        Review Create(Review Review);

        bool Delete(Review Review);

        #endregion

        #region ScreenResolutions region

        IEnumerable<ScreenResolution> ScreenResolutions { get; }

        ScreenResolution FindScreenResolutionById(int id);

        bool Update(int id, ScreenResolution ScreenResolution);

        ScreenResolution Create(ScreenResolution ScreenResolution);

        bool Delete(ScreenResolution ScreenResolution);

        #endregion


        #region Users region
        IEnumerable<User> Users { get; }

        User FindUserById(int id);

        User FindUserByName(string name);

        User Create(User user);

        bool Delete(User user);

        bool Update(int id, User user);
        #endregion


    }
}
