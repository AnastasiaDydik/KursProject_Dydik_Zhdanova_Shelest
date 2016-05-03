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
    public class MakersController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Makers
        public IQueryable<Kurs.Admin.Repository.Maker> GetMakers()
        {
            return db.Makers.Select(it => new Kurs.Admin.Repository.Maker { Id = it.Id, Title = it.Title });
        }

        // GET: api/Makers/5
        [ResponseType(typeof(Kurs.Admin.Repository.Maker))]
        public IHttpActionResult GetMaker(int id)
        {
            Maker maker = db.Makers.Find(id);
            if (maker == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Maker
            {
                Id = maker.Id,
                Title = maker.Title
            };
            return Ok(model);
        }

        // PUT: api/Makers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaker(int id, Kurs.Admin.Repository.Maker model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var maker = db.Makers.Find(model.Id);
            maker.Title = maker.Title;
            db.Entry(maker).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakerExists(id))
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

        // POST: api/Makers
        [ResponseType(typeof(Kurs.Admin.Repository.Maker))]
        public IHttpActionResult PostMaker(Kurs.Admin.Repository.Maker model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var maker = new Maker
            {
                Id = model.Id,
                Title = model.Title
            };
            db.Makers.Add(maker);
            db.SaveChanges();
            model.Id = maker.Id;
            return CreatedAtRoute("DefaultApi", new { id = maker.Id }, model);
        }

        // DELETE: api/Makers/5
        [ResponseType(typeof(Maker))]
        public IHttpActionResult DeleteMaker(int id)
        {
            Maker maker = db.Makers.Find(id);
            if (maker == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Maker
            {
                Id = maker.Id,
                Title = maker.Title
            };
            db.Makers.Remove(maker);
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

        private bool MakerExists(int id)
        {
            return db.Makers.Count(e => e.Id == id) > 0;
        }
    }
}