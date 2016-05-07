using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class ReviewsController : Controller
    {
        IKursRepository Repository;
        public ReviewsController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Reviews
        public ActionResult Index()
        {
            var model = Repository.Reviews.Select(it => new ReviewViewModel(it));

            return View(model);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            var item = Repository.FindReviewById(id);
            if (item == null)
                return HttpNotFound();
            var model = new ReviewViewModel(item);
            return View(model);
        }

        // GET: Reviews/Create
        public ActionResult Create(int deviceId)
        {
            var model = new ReviewViewModel { DeviceId = deviceId };
            return View(model);
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = new Review
                {
                    Id = model.Id,
                    Content = model.Content,
                    DeviceId = model.DeviceId
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

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            var item = Repository.FindReviewById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ReviewViewModel(item);
            return View(model);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReviewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindReviewById(id);
                if (item == null)
                    return HttpNotFound();

                item.Content = model.Content;
                item.DeviceId = model.DeviceId;

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

        // GET: Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            var item = Repository.FindReviewById(id);
            if (item == null)
                return HttpNotFound();

            var model = new ReviewViewModel(item);

            return View(model);
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ReviewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var item = Repository.FindReviewById(id);
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


        public PartialViewResult Reviews(int deviceId)
        {
            ViewBag.DeviceId = deviceId;
            var model = Repository.Reviews.Select(it => new ReviewViewModel(it));

            return PartialView(model);
        }
    }
}