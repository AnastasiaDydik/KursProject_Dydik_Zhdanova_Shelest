using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    public class ScreenResolutionsController : Controller
    {
        IKursRepository Repository;
        public ScreenResolutionsController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: ScreenResolutions
        public ActionResult Index()
        {
            var model = Repository.ScreenResolutions.Select(it => new ScreenResolutionViewModel(it));

            return View(model);
        }

        // GET: ScreenResolutions/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindScreenResolutionById(id);
            if (item == null)
                return HttpNotFound();
            var model = new ScreenResolutionViewModel(item);
            return View(model);
        }

        // GET: ScreenResolutions/Create
        public ActionResult Create()
        {
            var model = new ScreenResolutionViewModel();
            return View(model);
        }

        // POST: ScreenResolutions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScreenResolutionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new ScreenResolution
                {
                    Id = model.Id,
                    Height = model.Height,
                    Width = model.Width,
                    Diagonal = model.Diagonal
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

        // GET: ScreenResolutions/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindScreenResolutionById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ScreenResolutionViewModel(item);
            return View(model);
        }

        // POST: ScreenResolutions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ScreenResolutionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindScreenResolutionById(id);
                if (item == null)
                    return HttpNotFound();

                item.Height = model.Height;
                item.Width = model.Width;
                item.Diagonal = model.Diagonal;

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

        // GET: ScreenResolutions/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindScreenResolutionById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ScreenResolutionViewModel(item);

            return View(model);
        }

        // POST: ScreenResolutions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ScreenResolutionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindScreenResolutionById(id);
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