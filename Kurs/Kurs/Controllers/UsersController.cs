using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Kurs.Storage;

namespace Kurs.Controllers
{
   // [Authorize(Roles ="Администратор")]
    public class UsersController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Users
        public IEnumerable<Kurs.Admin.Repository.User> GetUsers()
        {
            return db.Users.Select(it => new Kurs.Admin.Repository.User { Id = it.Id, Name = it.Name, Password = it.Password });
        }

        // GET: api/Users/5
        [ResponseType(typeof(Kurs.Admin.Repository.User))]
        public IHttpActionResult GetUser(int? id, string name = null)
        {
            User user = null;
            if (id.HasValue && id != 0)
                user = db.Users.Find(id.Value);
            if (!string.IsNullOrWhiteSpace(name))
                user = db.Users.SingleOrDefault(it => it.Name == name);
            if (user == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.User
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password
            };

            return Ok(model);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, Kurs.Admin.Repository.User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var user = db.Users.Find(model.Id);
            user.Name = model.Name;
            user.Password = model.Password;

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(Kurs.Admin.Repository.User))]
        public IHttpActionResult PostUser(Kurs.Admin.Repository.User model, bool? isAdmin = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                Id = model.Id,
                Name = model.Name,
                Password = model.Password
            };

            db.Users.Add(user);

            try
            {
                db.SaveChanges();

                var role = db.Roles.SingleOrDefault(it => it.Name == "Клиент");
                if (isAdmin.HasValue && isAdmin.Value)
                    role = db.Roles.SingleOrDefault(it => it.Name == "Администратор");
                if (role != null)
                {
                    user.Roles.Add(role);
                    db.SaveChanges();
                }

                model.Id = user.Id;
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(Kurs.Admin.Repository.User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.User
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password
            };
            var userCarts = db.Carts.Where(it => it.UserId == user.Id);
            user.Roles.Clear();
            db.Carts.RemoveRange(userCarts);
            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}