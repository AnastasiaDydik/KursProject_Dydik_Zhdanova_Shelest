using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    public class MakersController : Controller
    {
        IKursRepository Repository;
        public MakersController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Makers
        public ActionResult Index()
        {
            var model = Repository.Makers.Select(it => new MakerViewModel(it));
            return View(model);
        }

        // GET: Makers/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindMakerById(id);
            if (item == null)
                return HttpNotFound();
            var model = new MakerViewModel(item);
            return View(model);
        }

        // GET: Makers/Create
        public ActionResult Create()
        {
            var model = new MakerViewModel();
            return View(model);
        }

        // POST: Makers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MakerViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new Maker
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

        // GET: Makers/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindMakerById(id);
            if (item == null)
                return HttpNotFound();

            var model = new MakerViewModel(item);
            return View(model);
        }

        // POST: Makers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MakerViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindMakerById(id);
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

        // GET: Makers/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindMakerById(id);
            if (item == null)
                return HttpNotFound();

            var model = new MakerViewModel(item);

            return View(model);
        }

        // POST: Makers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Maker model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindMakerById(id);
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