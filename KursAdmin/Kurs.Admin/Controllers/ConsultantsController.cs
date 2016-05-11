using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class ConsultantsController : Controller
    {
        IKursRepository Repository;
        public ConsultantsController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Consultants
        public ActionResult Index()
        {
            var model = Repository.Consultants.Select(it => new ConsultantViewModel(it));

            return View(model);
        }

        // GET: Consultants/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindConsultantById(id);
            if (item == null)
                return HttpNotFound();
            var model = new ConsultantViewModel(item);
            return View(model);
        }

        // GET: Consultants/Create
        public ActionResult Create()
        {
            var model = new ConsultantViewModel();
            return View(model);
        }

        // POST: Consultants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsultantViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new Consultant
                {
                    Id = model.Id,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
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

        // GET: Consultants/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindConsultantById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ConsultantViewModel(item);
            return View(model);
        }

        // POST: Consultants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ConsultantViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindConsultantById(id);
                if (item == null)
                    return HttpNotFound();

                item.Name = model.Name;
                item.PhoneNumber = model.PhoneNumber;
                item.Email = model.Email;

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

        // GET: Consultants/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindConsultantById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ConsultantViewModel(item);

            return View(model);
        }

        // POST: Consultants/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ConsultantViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindConsultantById(id);
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
