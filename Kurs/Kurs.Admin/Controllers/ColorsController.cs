using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    public class ColorsController : Controller
    {

        IKursRepository Repository;
        public ColorsController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Colors
        public ActionResult Index()
        {
            var model = Repository.Colors.Select(it => new ColorViewModel(it));
            return View(model);
        }

        // GET: Colors/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindColorById(id);
            if (item == null)
                return HttpNotFound();
            var model = new ColorViewModel(item);
            return View(model);
        }

        // GET: Colors/Create
        public ActionResult Create()
        {
            var model = new ColorViewModel();
            return View(model);
        }

        // POST: Colors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ColorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new Color
                {
                    Id = model.Id,
                    Title = model.Title
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

        // GET: Colors/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindColorById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ColorViewModel(item);
            return View(model);
        }

        // POST: Colors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ColorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindColorById(id);
                if (item == null)
                    return HttpNotFound();

                item.Title = model.Title;
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

        // GET: Colors/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindColorById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ColorViewModel(item);

            return View(model);
        }

        // POST: Colors/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Color model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindColorById(id);
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
