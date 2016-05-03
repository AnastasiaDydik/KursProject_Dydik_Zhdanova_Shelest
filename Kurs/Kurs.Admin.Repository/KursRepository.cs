using RestSharp;
using System;
using System.Collections.Generic;

namespace Kurs.Admin.Repository
{
    public class KursRepository : IKursRepository, IDisposable
    {
        IRestClient RestClient;

        public KursRepository()
        {
            RestClient = new RestClient("http://kursproj-001-site1.itempurl.com/");

        }

        public static KursRepository Create()
        {
            return new KursRepository();
        }

        #region Devices region
        public IEnumerable<Device> Devices(int? cat = null, decimal? minPrice = null, decimal? maxPrice = null, string keyword = null)
        {
            var request = new RestRequest("api/Devices/?cat={cat}&minPrice={minPrice}&maxPrice={maxPrice}&keyword={keyword}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("cat", cat.HasValue ? cat.Value.ToString() : string.Empty);
            request.AddUrlSegment("minPrice", minPrice.HasValue ? minPrice.Value.ToString() : string.Empty);
            request.AddUrlSegment("maxPrice", maxPrice.HasValue ? maxPrice.Value.ToString() : string.Empty);
            request.AddUrlSegment("keyword", keyword != null ? keyword : string.Empty);
            var responce = RestClient.Execute<List<Device>>(request);
            return responce.Data;
        }

        public Device FindDeviceById(int id)
        {
            var request = new RestRequest("api/Devices/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Device>(request);

            return responce.Data;
        }

        public bool Update(int id, Device device)
        {
            var request = new RestRequest("api/Devices/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(device);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Device Create(Device device)
        {
            var request = new RestRequest("api/Devices", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(device);

            var responce = RestClient.Execute<Device>(request);

            return responce.Data;
        }

        public bool Delete(Device device)
        {
            var request = new RestRequest("api/Devices/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", device.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                device = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Categories Region

        public IEnumerable<Category> Categories
        {
            get
            {
                var request = new RestRequest("api/Categories", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<Category>>(request);
                return responce.Data;
            }
        }

        public Category FindCategoryById(int id)
        {
            var request = new RestRequest("api/Categories/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Category>(request);

            return responce.Data;
        }

        public bool Update(int id, Category Category)
        {
            var request = new RestRequest("api/Categories/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(Category);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Category Create(Category Category)
        {
            var request = new RestRequest("api/Categories", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Category);

            var responce = RestClient.Execute<Category>(request);

            return responce.Data;
        }

        public bool Delete(Category Category)
        {
            var request = new RestRequest("api/Categories/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", Category.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                Category = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Colors region

        public IEnumerable<Color> Colors
        {
            get
            {
                var request = new RestRequest("api/Colors", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<Color>>(request);
                return responce.Data;
            }
        }

        public Color FindColorById(int id)
        {
            var request = new RestRequest("api/Colors/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Color>(request);

            return responce.Data;
        }

        public bool Update(int id, Color Color)
        {
            var request = new RestRequest("api/Colors/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(Color);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Color Create(Color Color)
        {
            var request = new RestRequest("api/Colors", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Color);

            var responce = RestClient.Execute<Color>(request);

            return responce.Data;
        }

        public bool Delete(Color Color)
        {
            var request = new RestRequest("api/Colors/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", Color.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                Color = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Consultants region

        public IEnumerable<Consultant> Consultants
        {
            get
            {
                var request = new RestRequest("api/Consultants", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<Consultant>>(request);
                return responce.Data;
            }
        }

        public Consultant FindConsultantById(int id)
        {
            var request = new RestRequest("api/Consultants/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Consultant>(request);

            return responce.Data;
        }

        public bool Update(int id, Consultant Consultant)
        {
            var request = new RestRequest("api/Consultants/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(Consultant);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Consultant Create(Consultant Consultant)
        {
            var request = new RestRequest("api/Consultants", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Consultant);

            var responce = RestClient.Execute<Consultant>(request);

            return responce.Data;
        }

        public bool Delete(Consultant Consultant)
        {
            var request = new RestRequest("api/Consultants/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", Consultant.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                Consultant = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Countries region

        public IEnumerable<Country> Countries
        {
            get
            {
                var request = new RestRequest("api/Countries", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<Country>>(request);
                return responce.Data;
            }
        }

        public Country FindCountryById(int id)
        {
            var request = new RestRequest("api/Countries/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Country>(request);

            return responce.Data;
        }

        public bool Update(int id, Country Country)
        {
            var request = new RestRequest("api/Countries/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(Country);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Country Create(Country Country)
        {
            var request = new RestRequest("api/Countries", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Country);

            var responce = RestClient.Execute<Country>(request);

            return responce.Data;
        }

        public bool Delete(Country Country)
        {
            var request = new RestRequest("api/Countries/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", Country.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                Country = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Digital cameras region

        public IEnumerable<DigitalCamera> DigitalCameras
        {
            get
            {
                var request = new RestRequest("api/DigitalCameras", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<DigitalCamera>>(request);
                return responce.Data;
            }
        }

        public DigitalCamera FindDigitalCameraById(int id)
        {
            var request = new RestRequest("api/DigitalCameras/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<DigitalCamera>(request);

            return responce.Data;
        }

        public bool Update(int id, DigitalCamera DigitalCamera)
        {
            var request = new RestRequest("api/DigitalCameras/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(DigitalCamera);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public DigitalCamera Create(DigitalCamera DigitalCamera)
        {
            var request = new RestRequest("api/DigitalCameras", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(DigitalCamera);

            var responce = RestClient.Execute<DigitalCamera>(request);

            return responce.Data;
        }

        public bool Delete(DigitalCamera DigitalCamera)
        {
            var request = new RestRequest("api/DigitalCameras/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", DigitalCamera.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                DigitalCamera = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Makers region

        public IEnumerable<Maker> Makers
        {
            get
            {
                var request = new RestRequest("api/Makers", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<Maker>>(request);
                return responce.Data;
            }
        }

        public Maker FindMakerById(int id)
        {
            var request = new RestRequest("api/Makers/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Maker>(request);

            return responce.Data;
        }

        public bool Update(int id, Maker Maker)
        {
            var request = new RestRequest("api/Makers/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(Maker);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Maker Create(Maker Maker)
        {
            var request = new RestRequest("api/Makers", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Maker);

            var responce = RestClient.Execute<Maker>(request);

            return responce.Data;
        }

        public bool Delete(Maker Maker)
        {
            var request = new RestRequest("api/Makers/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", Maker.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                Maker = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Operating systems region

        public IEnumerable<OperatingSystem> OperatingSystems
        {
            get
            {
                var request = new RestRequest("api/OperatingSystems", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<OperatingSystem>>(request);
                return responce.Data;
            }
        }

        public OperatingSystem FindOperatingSystemById(int id)
        {
            var request = new RestRequest("api/OperatingSystems/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<OperatingSystem>(request);

            return responce.Data;
        }

        public bool Update(int id, OperatingSystem OperatingSystem)
        {
            var request = new RestRequest("api/OperatingSystems/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(OperatingSystem);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public OperatingSystem Create(OperatingSystem OperatingSystem)
        {
            var request = new RestRequest("api/OperatingSystems", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(OperatingSystem);

            var responce = RestClient.Execute<OperatingSystem>(request);

            return responce.Data;
        }

        public bool Delete(OperatingSystem OperatingSystem)
        {
            var request = new RestRequest("api/OperatingSystems/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", OperatingSystem.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                OperatingSystem = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Processors region

        public IEnumerable<Processor> Processors
        {
            get
            {
                var request = new RestRequest("api/Processors", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<Processor>>(request);
                return responce.Data;
            }
        }

        public Processor FindProcessorById(int id)
        {
            var request = new RestRequest("api/Processors/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Processor>(request);

            return responce.Data;
        }

        public bool Update(int id, Processor Processor)
        {
            var request = new RestRequest("api/Processors/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(Processor);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Processor Create(Processor Processor)
        {
            var request = new RestRequest("api/Processors", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Processor);

            var responce = RestClient.Execute<Processor>(request);

            return responce.Data;
        }

        public bool Delete(Processor Processor)
        {
            var request = new RestRequest("api/Processors/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", Processor.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                Processor = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Reviews region

        public IEnumerable<Review> Reviews
        {
            get
            {
                var request = new RestRequest("api/Reviews", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<Review>>(request);
                return responce.Data;
            }
        }

        public Review FindReviewById(int id)
        {
            var request = new RestRequest("api/Reviews/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<Review>(request);

            return responce.Data;
        }

        public bool Update(int id, Review Review)
        {
            var request = new RestRequest("api/Reviews/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(Review);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public Review Create(Review Review)
        {
            var request = new RestRequest("api/Reviews", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Review);

            var responce = RestClient.Execute<Review>(request);

            return responce.Data;
        }

        public bool Delete(Review Review)
        {
            var request = new RestRequest("api/Reviews/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", Review.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                Review = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Screen resolution region

        public IEnumerable<ScreenResolution> ScreenResolutions
        {
            get
            {
                var request = new RestRequest("api/ScreenResolutions", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<ScreenResolution>>(request);
                return responce.Data;
            }
        }

        public ScreenResolution FindScreenResolutionById(int id)
        {
            var request = new RestRequest("api/ScreenResolutions/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<ScreenResolution>(request);

            return responce.Data;
        }

        public bool Update(int id, ScreenResolution ScreenResolution)
        {
            var request = new RestRequest("api/ScreenResolutions/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(ScreenResolution);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public ScreenResolution Create(ScreenResolution ScreenResolution)
        {
            var request = new RestRequest("api/ScreenResolutions", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(ScreenResolution);

            var responce = RestClient.Execute<ScreenResolution>(request);

            return responce.Data;
        }

        public bool Delete(ScreenResolution ScreenResolution)
        {
            var request = new RestRequest("api/ScreenResolutions/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", ScreenResolution.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                ScreenResolution = null;
                return true;
            }

            return false;
        }

        #endregion

        #region User region
        public IEnumerable<User> Users
        {
            get
            {
                var request = new RestRequest("api/Users", Method.GET);
                request.RequestFormat = DataFormat.Json;
                var responce = RestClient.Execute<List<User>>(request);
                return responce.Data;
            }
        }

        public User FindUserById(int id)
        {
            var request = new RestRequest("api/Users/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var responce = RestClient.Execute<User>(request);

            return responce.Data;
        }

        public User FindUserByName(string name) {
            var request = new RestRequest("api/Users/{id}?name={name}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", "0");
            request.AddUrlSegment("name", name);
            var responce = RestClient.Execute<User>(request);

            return responce.Data;
        }

        public User Create(User user) {
            var request = new RestRequest("api/Users", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);

            var responce = RestClient.Execute<User>(request);

            return responce.Data;
        }

        public bool Delete(User user) {
            var request = new RestRequest("api/Users/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", user.Id.ToString());
            var responce = RestClient.Execute(request);

            if (responce.ResponseStatus == ResponseStatus.Completed)
            {
                user = null;
                return true;
            }

            return false;
        }

        public bool Update(int id, User user)
        {
            var request = new RestRequest("api/Users/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(user);

            var responce = RestClient.Execute(request);

            return responce.ResponseStatus == ResponseStatus.Completed;
        }

        public void Dispose()
        {

        }
        #endregion

    }
}
