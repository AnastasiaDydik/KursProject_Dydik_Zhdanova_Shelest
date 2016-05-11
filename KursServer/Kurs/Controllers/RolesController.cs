using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Kurs.Storage;

namespace Kurs.Controllers
{
    public class RolesController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Roles
        public IQueryable<Kurs.Admin.Repository.Role> GetRoles()
        {
            return db.Roles.Select(it => new Kurs.Admin.Repository.Role { Id = it.Id, Name = it.Name });
        }

        // GET: api/Roles/5
        [ResponseType(typeof(Kurs.Admin.Repository.Role))]
        public IHttpActionResult GetRole(int id, string name = "")
        {
            Role role = null;
            if (id == 0)
            {
                role = db.Roles.FirstOrDefault(it => it.Name == name);
            }
            else
            {
                role = db.Roles.Find(id);
            }
            
            if (role == null)
            {
                return NotFound();
            }

            var model = new Kurs.Admin.Repository.Role
            {
                Id = role.Id,
                Name = role.Name
            };
            
            return Ok(model);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(int id, Kurs.Admin.Repository.Role model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var role = db.Roles.Find(model.Id);
            role.Name = model.Name;

            db.Entry(role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        [ResponseType(typeof(Kurs.Admin.Repository.Role))]
        public IHttpActionResult PostRole(Kurs.Admin.Repository.Role model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = new Role
            {
                Id = model.Id,
                Name = model.Name
            };

            db.Roles.Add(role);

            try
            {
                db.SaveChanges();
                model.Id = role.Id;
            }
            catch (DbUpdateException)
            {
                if (RoleExists(role.Id))
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

        // DELETE: api/Roles/5
        [ResponseType(typeof(Kurs.Admin.Repository.Role))]
        public IHttpActionResult DeleteRole(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Role
            {
                Id = role.Id,
                Name = role.Name
            };
            db.Roles.Remove(role);
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

        private bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}