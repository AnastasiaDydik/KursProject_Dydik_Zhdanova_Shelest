using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Kurs.Storage;
using OperatingSystem = Kurs.Storage.OperatingSystem;

namespace Kurs.Controllers
{
    public class OperatingSystemsController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/OperatingSystems
        public IQueryable<Kurs.Admin.Repository.OperatingSystem> GetOperatingSystems()
        {
            return db.OperatingSystems.Select(it => new Kurs.Admin.Repository.OperatingSystem { Id = it.Id, Title = it.Title });
        }

        // GET: api/OperatingSystems/5
        [ResponseType(typeof(Kurs.Admin.Repository.OperatingSystem))]
        public IHttpActionResult GetOperatingSystem(int id)
        {
            Storage.OperatingSystem operatingSystem = db.OperatingSystems.Find(id);
            if (operatingSystem == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.OperatingSystem
            {
                Id = operatingSystem.Id,
                Title = operatingSystem.Title
            };
            return Ok(model);
        }

        // PUT: api/OperatingSystems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOperatingSystem(int id, Kurs.Admin.Repository.OperatingSystem model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var operatingSystem = db.OperatingSystems.Find(model.Id);
            operatingSystem.Title = model.Title;
            db.Entry(operatingSystem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperatingSystemExists(id))
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

        // POST: api/OperatingSystems
        [ResponseType(typeof(Kurs.Admin.Repository.OperatingSystem))]
        public IHttpActionResult PostOperatingSystem(Kurs.Admin.Repository.OperatingSystem model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var operatingSystem = new OperatingSystem
            {
                Id = model.Id,
                Title = model.Title
            };
            db.OperatingSystems.Add(operatingSystem);
            db.SaveChanges();
            model.Id = operatingSystem.Id;
            return CreatedAtRoute("DefaultApi", new { id = operatingSystem.Id }, model);
        }

        // DELETE: api/OperatingSystems/5
        [ResponseType(typeof(Kurs.Admin.Repository.OperatingSystem))]
        public IHttpActionResult DeleteOperatingSystem(int id)
        {
            OperatingSystem operatingSystem = db.OperatingSystems.Find(id);
            if (operatingSystem == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.OperatingSystem
            {
                Id = operatingSystem.Id,
                Title = operatingSystem.Title
            };
            db.OperatingSystems.Remove(operatingSystem);
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

        private bool OperatingSystemExists(int id)
        {
            return db.OperatingSystems.Count(e => e.Id == id) > 0;
        }
    }
}