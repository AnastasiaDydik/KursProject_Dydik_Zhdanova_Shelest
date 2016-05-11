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
    public class ConsultantsController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Consultants
        public IQueryable<Kurs.Admin.Repository.Consultant> GetConsultants()
        {
            return db.Consultants.Select(it => new Kurs.Admin.Repository.Consultant { Id = it.Id, Name = it.Name, Email = it.Email, PhoneNumber = it.PhoneNumber });
        }

        // GET: api/Consultants/5
        [ResponseType(typeof(Kurs.Admin.Repository.Consultant))]
        public IHttpActionResult GetConsultant(int id)
        {
            Consultant consultant = db.Consultants.Find(id);
            if (consultant == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Consultant { Id = consultant.Id, Name = consultant.Name, Email = consultant.Email, PhoneNumber = consultant.PhoneNumber };
            return Ok(model);
        }

        // PUT: api/Consultants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConsultant(int id, Kurs.Admin.Repository.Consultant model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var consultant = db.Consultants.Find(model.Id);
            consultant.Name = model.Name;
            consultant.PhoneNumber = model.PhoneNumber;
            consultant.Email = model.Email;
            db.Entry(consultant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantExists(id))
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

        // POST: api/Consultants
        [ResponseType(typeof(Kurs.Admin.Repository.Consultant))]
        public IHttpActionResult PostConsultant(Kurs.Admin.Repository.Consultant model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var consultant = new Consultant { Id = model.Id, Name = model.Name, Email = model.Email, PhoneNumber = model.PhoneNumber };
            db.Consultants.Add(consultant);
            db.SaveChanges();
            model.Id = consultant.Id;

            return CreatedAtRoute("DefaultApi", new { id = consultant.Id }, model);
        }

        // DELETE: api/Consultants/5
        [ResponseType(typeof(Kurs.Admin.Repository.Consultant))]
        public IHttpActionResult DeleteConsultant(int id)
        {
            Consultant model = db.Consultants.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Consultants.Remove(model);
            db.SaveChanges();
            var consultant = new Consultant { Id = model.Id, Name = model.Name, Email = model.Email, PhoneNumber = model.PhoneNumber };
            return Ok(consultant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsultantExists(int id)
        {
            return db.Consultants.Count(e => e.Id == id) > 0;
        }
    }
}