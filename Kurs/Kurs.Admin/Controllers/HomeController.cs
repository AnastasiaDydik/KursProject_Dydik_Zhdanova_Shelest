using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    public class HomeController : Controller
    {
        IKursRepository Repository;
        public HomeController(IKursRepository repository)
        {
            Repository = repository;
        }

        public ActionResult Index(int? cat = null, decimal? minPrice = null, decimal? maxPrice = null, string keyword = null)
        {
            var devices = Repository.Devices(cat, minPrice, maxPrice, keyword);

            if (cat.HasValue)
                devices = devices.Where(it => it.CategoryId == cat.Value);
            if (minPrice.HasValue)
                devices = devices.Where(it => it.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                devices = devices.Where(it => it.Price <= maxPrice.Value);
            if (!string.IsNullOrWhiteSpace(keyword))
                devices = devices.Where(it => it.Model.Contains(keyword) || it.Info.Contains(keyword));

             var model = devices.Select(it =>
             {
                 var category = Repository.FindCategoryById(it.CategoryId);
                 var color = it.ColorId.HasValue ? Repository.FindColorById(it.ColorId.Value) : null;
                 var maker = Repository.FindMakerById(it.MakerId);
                 var screenResolution = it.ScreenResolutionId.HasValue ? Repository.FindScreenResolutionById(it.ScreenResolutionId.Value) : null;
                 var proccessor = it.ProcessorId.HasValue ? Repository.FindProcessorById(it.ProcessorId.Value) : null;
                 var os = it.OperatingSystemId.HasValue ? Repository.FindOperatingSystemById(it.OperatingSystemId.Value) : null;
                 var camera = it.DigitalCameraId.HasValue ? Repository.FindDigitalCameraById(it.DigitalCameraId.Value) : null;
                 var country = Repository.FindCountryById(it.CountryId);
                 return new DeviceListItemViewModel(it, category, color, maker, screenResolution, proccessor, os, camera, country);
             });
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult Categories()
        {
            var model = Repository.Categories.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            return PartialView(model);
        }

        public PartialViewResult Filter()
        {
            var model = Repository.Categories.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            return PartialView(model);
        }
        
    }
}