using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    [Authorize(Roles = "Администратор,")]
    public class UsersController : Controller
    {
        IKursRepository Repository;
        public UsersController(IKursRepository repository)
        {
            Repository = repository;
        }

        // GET: Users

        public ActionResult Index()
        {
            if (User.IsInRole("Администратор"))
            {
                var users = Repository.Users.Select(it =>
                {
                    var roleId = Repository.UserRoles.FirstOrDefault(userRole => userRole.UserId == it.Id)?.RoleId;
                    if (roleId.HasValue)
                    {
                        var role = Repository.FindRoleById(roleId.Value);
                        return new UserViewModel { Id = it.Id, Name = it.Name, Role = role?.Name };
                    }
                    return new UserViewModel { Id = it.Id, Name = it.Name, Role = "Клиент" };
                });
                return View(users);
            }
            return new HttpStatusCodeResult(403);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            var user = Repository.FindUserById(id);
            if (user == null)
                return HttpNotFound();

            var roleId = Repository.UserRoles.FirstOrDefault(userRole => userRole.UserId == user.Id)?.RoleId;
            var role = roleId.HasValue ? Repository.FindRoleById(roleId.Value) : null;
            var model = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Role = role != null ? role.Name : "Клиент"
            };

            return View(model);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var model = new UserFormViewModel();
            ViewBag.Roles = Repository.Roles.Select(it => new SelectListItem { Text = it.Name, Value = it.Id.ToString() });
            return View(model);
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(UserFormViewModel model)
        {
            try
            {
                ViewBag.Roles = Repository.Roles.Select(it => new SelectListItem { Text = it.Name, Value = it.Id.ToString(), Selected = it.Id == model.RoleId });
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Password = model.Password
                    };

                    user = Repository.Create(user);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Не удалось создать пользователя");
                        return View(model);
                    }

                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = model.RoleId,
                    };

                    Repository.Create(userRole);
                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here
                return View(model);

            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            var user = Repository.FindUserById(id);
            if (user == null)
                return HttpNotFound();
            var roleId = Repository.UserRoles.FirstOrDefault(it => it.UserId == user.Id)?.RoleId;
            ViewBag.Roles = Repository.Roles.Select(it => new SelectListItem { Text = it.Name, Value = it.Id.ToString(), Selected = roleId.HasValue && it.Id == roleId.Value });
            var model = new UserFormViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password
            };

            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserFormViewModel model)
        {
            try
            {
                ViewBag.Roles = Repository.Roles.Select(it => new SelectListItem { Text = it.Name, Value = it.Id.ToString(), Selected = it.Id == model.RoleId });
                if (ModelState.IsValid && id == model.Id)
                {
                    var user = Repository.FindUserById(id);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Не удалось нати пользователя");
                        return View(model);
                    }
                    user.Name = model.Name;
                    user.Password = model.Password;
                    Repository.Update(id, user);

                    var userRoles = Repository.UserRoles.Where(it => it.UserId == model.Id);
                    foreach (var t in userRoles)
                    {
                        Repository.Delete(t);
                    }

                    var userRole = new UserRole
                    {
                        UserId = model.Id,
                        RoleId = model.RoleId
                    };

                    Repository.Create(userRole);
                    return RedirectToAction("Index");
                }
                // TODO: Add update logic here
                return View(model);

            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            var user = Repository.FindUserById(id);
            if (user == null)
                return HttpNotFound();

            var model = new UserFormViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,

            };
            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserFormViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = Repository.FindUserById(model.Id);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Не удалось найти пользователя");
                        return View(model);
                    }

                    Repository.Delete(user);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
