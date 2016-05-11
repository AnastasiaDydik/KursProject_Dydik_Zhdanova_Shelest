using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class DevicesController : Controller
    {
        IKursRepository Repository;
        public DevicesController(IKursRepository repository)
        {
            Repository = repository;
        }
        // GET: Devices
        public ActionResult Index()
        {
            var devices = Repository.Devices().Select(it =>
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
            return View(devices);
        }

        // GET: Devices/Details/5
        public ActionResult Details(int id)
        {
            var device = Repository.FindDeviceById(id);
            if (device == null)
                return HttpNotFound();

            var category = Repository.FindCategoryById(device.CategoryId);
            var color = device.ColorId.HasValue ? Repository.FindColorById(device.ColorId.Value) : null;
            var maker = Repository.FindMakerById(device.MakerId);
            var screenResolution = device.ScreenResolutionId.HasValue ? Repository.FindScreenResolutionById(device.ScreenResolutionId.Value) : null;
            var proccessor = device.ProcessorId.HasValue ? Repository.FindProcessorById(device.ProcessorId.Value) : null;
            var os = device.OperatingSystemId.HasValue ? Repository.FindOperatingSystemById(device.OperatingSystemId.Value) : null;
            var camera = device.DigitalCameraId.HasValue ? Repository.FindDigitalCameraById(device.DigitalCameraId.Value) : null;
            var country = Repository.FindCountryById(device.CountryId);
            var model = new DeviceListItemViewModel(device, category, color, maker, screenResolution, proccessor, os, camera, country);

            return View(model);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            ViewBag.Categories = Repository.Categories.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            ViewBag.Colors = Repository.Colors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            ViewBag.Countries = Repository.Countries.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            ViewBag.DigitalCameras = Repository.DigitalCameras.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height}", Value = it.Id.ToString() });
            ViewBag.Processors = Repository.Processors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            ViewBag.Makers = Repository.Makers.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            ViewBag.OperatingSystems = Repository.OperatingSystems.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString() });
            ViewBag.ScreenResolutions = Repository.ScreenResolutions.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height} {it.Diagonal}\"", Value = it.Id.ToString() });
            var model = new DeviceViewModel();
            return View(model);
        }

        // POST: Devices/Create
        [HttpPost]
        public ActionResult Create(DeviceViewModel model)
        {
            try
            {
                var context = HttpContext;
                var modelState = ModelState;
                ViewBag.Categories = Repository.Categories.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.CategoryId });
                ViewBag.Colors = Repository.Colors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.ColorId });
                ViewBag.Countries = Repository.Countries.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.CountryId });
                ViewBag.DigitalCameras = Repository.DigitalCameras.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height}", Value = it.Id.ToString(), Selected = it.Id == model.DigitalCameraId });
                ViewBag.Processors = Repository.Processors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.ProcessorId });
                ViewBag.Makers = Repository.Makers.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.MakerId });
                ViewBag.OperatingSystems = Repository.OperatingSystems.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.OperatingSystemId });
                ViewBag.ScreenResolutions = Repository.ScreenResolutions.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height} {it.Diagonal}\"", Value = it.Id.ToString(), Selected = it.Id == model.ScreenResolutionId });
                if (!ModelState.IsValid)
                    return View(model);

                var device = new Device
                {
                    Id = model.Id,
                    CategoryId = model.CategoryId,
                    CountryId = model.CountryId,
                    ColorId = model.ColorId,
                    DigitalCameraId = model.DigitalCameraId,
                    FreeCount = model.FreeCount,
                    Heigth = model.Heigth,
                    Image = model.Image,
                    Info = model.Info,
                    MakerId = model.MakerId,
                    Memory = model.Memory,
                    Model = model.ModelName,
                    OperatingSystemId = model.OperatingSystemId,
                    Price = model.Price,
                    ProcessorId = model.ProcessorId,
                    Ram = model.Ram,
                    ScreenResolutionId = model.ScreenResolutionId,
                    TotalCount = model.TotalCount,
                    Width = model.Width,

                };

                var result = Repository.Create(device);
                if (result == null)
                {
                    ModelState.AddModelError("", "Не удалось создать элемента");
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int id)
        {
            var device = Repository.FindDeviceById(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            var model = new DeviceViewModel
            {
                CategoryId = device.CategoryId,
                ColorId = device.ColorId,
                CountryId = device.CountryId,
                DigitalCameraId = device.DigitalCameraId,
                FreeCount = device.FreeCount,
                Heigth = device.Heigth,
                Id = device.Id,
                Image = device.Image,
                Info = device.Info,
                MakerId = device.MakerId,
                Memory = device.Memory,
                ModelName = device.Model,
                OperatingSystemId = device.OperatingSystemId,
                Price = device.Price,
                ProcessorId = device.ProcessorId,
                Ram = device.Ram,
                ScreenResolutionId = device.ScreenResolutionId,
                TotalCount = device.TotalCount,
                Width = device.Width
            };

            ViewBag.Categories = Repository.Categories.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.CategoryId });
            ViewBag.Colors = Repository.Colors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.ColorId });
            ViewBag.Countries = Repository.Countries.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.CountryId });
            ViewBag.DigitalCameras = Repository.DigitalCameras.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height}", Value = it.Id.ToString(), Selected = it.Id == model.DigitalCameraId });
            ViewBag.Processors = Repository.Processors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.ProcessorId });
            ViewBag.Makers = Repository.Makers.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.MakerId });
            ViewBag.OperatingSystems = Repository.OperatingSystems.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.OperatingSystemId });
            ViewBag.ScreenResolutions = Repository.ScreenResolutions.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height} {it.Diagonal}\"", Value = it.Id.ToString(), Selected = it.Id == model.ScreenResolutionId });


            return View(model);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DeviceViewModel model)
        {
            try
            {
                ViewBag.Categories = Repository.Categories.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.CategoryId });
                ViewBag.Colors = Repository.Colors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.ColorId });
                ViewBag.Countries = Repository.Countries.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.CountryId });
                ViewBag.DigitalCameras = Repository.DigitalCameras.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height}", Value = it.Id.ToString(), Selected = it.Id == model.DigitalCameraId });
                ViewBag.Processors = Repository.Processors.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.ProcessorId });
                ViewBag.Makers = Repository.Makers.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.MakerId });
                ViewBag.OperatingSystems = Repository.OperatingSystems.Select(it => new SelectListItem { Text = it.Title, Value = it.Id.ToString(), Selected = it.Id == model.OperatingSystemId });
                ViewBag.ScreenResolutions = Repository.ScreenResolutions.Select(it => new SelectListItem { Text = $"{it.Width}x{it.Height} {it.Diagonal}\"", Value = it.Id.ToString(), Selected = it.Id == model.ScreenResolutionId });

                if (!ModelState.IsValid)
                    return View(model);


                var device = new Device
                {
                    Id = model.Id,
                    CategoryId = model.CategoryId,
                    CountryId = model.CountryId,
                    ColorId = model.ColorId,
                    DigitalCameraId = model.DigitalCameraId,
                    FreeCount = model.FreeCount,
                    Heigth = model.Heigth,
                    Image = model.Image,
                    Info = model.Info,
                    MakerId = model.MakerId,
                    Memory = model.Memory,
                    Model = model.ModelName,
                    OperatingSystemId = model.OperatingSystemId,
                    Price = model.Price,
                    ProcessorId = model.ProcessorId,
                    Ram = model.Ram,
                    ScreenResolutionId = model.ScreenResolutionId,
                    TotalCount = model.TotalCount,
                    Width = model.Width,

                };

                var result = Repository.Update(device.Id, device);
                if (!result)
                {
                    ModelState.AddModelError("", "Не удалось обновить элемент");
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int id)
        {
            var device = Repository.FindDeviceById(id);
            if (device == null)
                return HttpNotFound();

            var model = new DeviceViewModel
            {
                Id = device.Id,
                CategoryId = device.CategoryId,
                CountryId = device.CountryId,
                ColorId = device.ColorId,
                DigitalCameraId = device.DigitalCameraId,
                FreeCount = device.FreeCount,
                Heigth = device.Heigth,
                Image = device.Image,
                Info = device.Info,
                MakerId = device.MakerId,
                Memory = device.Memory,
                ModelName = device.Model,
                OperatingSystemId = device.OperatingSystemId,
                Price = device.Price,
                ProcessorId = device.ProcessorId,
                Ram = device.Ram,
                ScreenResolutionId = device.ScreenResolutionId,
                TotalCount = device.TotalCount,
                Width = device.Width,
            };
            return View(model);
        }

        // POST: Devices/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DeviceViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);


                var device = Repository.FindDeviceById(model.Id);
                if (device == null)
                {
                    ModelState.AddModelError("", "Не найдено устройство");
                    return View(model);
                }

                var isDelete = Repository.Delete(device);
                if(!isDelete)
                {
                    ModelState.AddModelError("", "Не удалось удалить устройство");
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
