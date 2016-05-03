using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    public class ProcessorsController : Controller
    {
        IKursRepository Repository;
        public ProcessorsController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Processors
        public ActionResult Index()
        {
            var model = Repository.Processors.Select(it => new ProcessorViewModel(it));
            return View(model);
        }

        // GET: Processors/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindProcessorById(id);
            if (item == null)
                return HttpNotFound();
            var model = new ProcessorViewModel(item);
            return View(model);
        }

        // GET: Processors/Create
        public ActionResult Create()
        {
            var model = new ProcessorViewModel();
            return View(model);
        }

        // POST: Processors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProcessorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new Processor
                {
                    Id = model.Id,
                    Title = model.Title,
                    Cores = model.Cores,
                    Frequency = model.Frequency
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

        // GET: Processors/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindProcessorById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ProcessorViewModel(item);
            return View(model);
        }

        // POST: Processors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProcessorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindProcessorById(id);
                if (item == null)
                    return HttpNotFound();

                item.Title = model.Title;
                item.Cores = model.Cores;
                item.Frequency = model.Frequency;

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

        // GET: Processors/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindProcessorById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ProcessorViewModel(item);

            return View(model);
        }

        // POST: Processors/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Processor model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindProcessorById(id);
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