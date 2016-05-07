using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class CategoriesController : Controller
    {
        IKursRepository Repository;
        public CategoriesController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Categories
        public ActionResult Index()
        {
            var model = Repository.Categories.Select(it => new CategoryViewModel(it));
            return View(model);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            var category = Repository.FindCategoryById(id);
            if (category == null)
                return HttpNotFound();
            var model = new CategoryViewModel(category);
            return View(model);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var category = new Category
                {
                    Id = model.Id,
                    Title = model.Title
                };

                var result = Repository.Create(category);

                if(result != null)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Не удалось создать элемент");
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            var category = Repository.FindCategoryById(id);
            if (category == null)
                return HttpNotFound();

            var model = new CategoryViewModel(category);
            return View(model);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var category = Repository.FindCategoryById(id);
                if (category == null)
                    return HttpNotFound();

                category.Title = model.Title;
                var result = Repository.Update(id, category);
                if(!result)
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

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            var category = Repository.FindCategoryById(id);
            if (category == null)
                return HttpNotFound();

            var model = new CategoryViewModel(category);

            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var category = Repository.FindCategoryById(id);
                if (category == null)
                    return HttpNotFound();

                var result = Repository.Delete(category);
                if(!result)
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
