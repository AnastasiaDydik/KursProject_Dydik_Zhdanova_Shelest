using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class OperatingSystemsController : Controller
    {
        IKursRepository Repository;
        public OperatingSystemsController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: OperatingSystems
        public ActionResult Index()
        {
            var model = Repository.OperatingSystems.Select(it => new OperatingSystemViewModel(it));
            return View(model);
        }

        // GET: OperatingSystems/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindOperatingSystemById(id);
            if (item == null)
                return HttpNotFound();
            var model = new OperatingSystemViewModel(item);
            return View(model);
        }

        // GET: OperatingSystems/Create
        public ActionResult Create()
        {
            var model = new OperatingSystemViewModel();
            return View(model);
        }

        // POST: OperatingSystems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OperatingSystemViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new Repository.OperatingSystem
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

        // GET: OperatingSystems/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindOperatingSystemById(id);
            if (item == null)
                return HttpNotFound();

            var model = new OperatingSystemViewModel(item);
            return View(model);
        }

        // POST: OperatingSystems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OperatingSystemViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindOperatingSystemById(id);
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

        // GET: OperatingSystems/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindOperatingSystemById(id);
            if (item == null)
                return HttpNotFound();

            var model = new OperatingSystemViewModel(item);

            return View(model);
        }

        // POST: OperatingSystems/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Repository.OperatingSystem model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindOperatingSystemById(id);
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