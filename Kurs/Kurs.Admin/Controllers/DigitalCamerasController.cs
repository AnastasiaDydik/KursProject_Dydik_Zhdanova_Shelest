using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    public class DigitalCamerasController : Controller
    {
        IKursRepository Repository;
        public DigitalCamerasController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: DigitalCameras
        public ActionResult Index()
        {
            var model = Repository.DigitalCameras.Select(it => new DigitalCameraViewModel(it));
            return View(model);
        }

        // GET: DigitalCameras/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindDigitalCameraById(id);
            if (item == null)
                return HttpNotFound();
            var model = new DigitalCameraViewModel(item);
            return View(model);
        }

        // GET: DigitalCameras/Create
        public ActionResult Create()
        {
            var model = new DigitalCameraViewModel();
            return View(model);
        }

        // POST: DigitalCameras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DigitalCameraViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new DigitalCamera
                {
                    Id = model.Id,
                    Height = model.Height,
                    Width = model.Width
                };

                var result = Repository.Create(item);

                if (result != null)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Не удалось создать элемент");
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: DigitalCameras/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindDigitalCameraById(id);
            if (item == null)
                return HttpNotFound();

            var model = new DigitalCameraViewModel(item);
            return View(model);
        }

        // POST: DigitalCameras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DigitalCameraViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindDigitalCameraById(id);
                if (item == null)
                    return HttpNotFound();

                item.Height = model.Height;
                item.Width = model.Width;
                var result = Repository.Update(id, item);
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

        // GET: DigitalCameras/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindDigitalCameraById(id);
            if (item == null)
                return HttpNotFound();

            var model = new DigitalCameraViewModel(item);

            return View(model);
        }

        // POST: DigitalCameras/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DigitalCamera model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindDigitalCameraById(id);
                if (item == null)
                    return HttpNotFound();

                var result = Repository.Delete(item);
                if (!result)
                {
                    ModelState.AddModelError("", "Не удалось удалить элемент");
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