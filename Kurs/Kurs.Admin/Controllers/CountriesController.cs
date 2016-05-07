using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class CountriesController : Controller
    {
        IKursRepository Repository;
        public CountriesController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Countries
        public ActionResult Index()
        {
            var model = Repository.Countries.Select(it => new CountryViewModel(it));
            return View(model);
        }

        // GET: Countries/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindCountryById(id);
            if (item == null)
                return HttpNotFound();
            var model = new CountryViewModel(item);
            return View(model);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            var model = new CountryViewModel();
            return View(model);
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new Country
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

        // GET: Countries/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindCountryById(id);
            if (item == null)
                return HttpNotFound();

            var model = new CountryViewModel(item);
            return View(model);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CountryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindCountryById(id);
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

        // GET: Countries/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindCountryById(id);
            if (item == null)
                return HttpNotFound();

            var model = new CountryViewModel(item);

            return View(model);
        }

        // POST: Countries/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Country model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindCountryById(id);
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
