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
    public class ColorsController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Colors
        public IQueryable<Kurs.Admin.Repository.Color> GetColors()
        {
            return db.Colors.Select(it => new Kurs.Admin.Repository.Color { Id = it.Id, Title = it.Title});
        }

        // GET: api/Colors/5
        [ResponseType(typeof(Kurs.Admin.Repository.Color))]
        public IHttpActionResult GetColor(int id)
        {
            Color color = db.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Color { Id = color.Id, Title = color.Title };
            return Ok(model);
        }

        // PUT: api/Colors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutColor(int id, Kurs.Admin.Repository.Color model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var color = db.Colors.Find(model.Id);
            color.Title = model.Title;
            db.Entry(color).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
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

        // POST: api/Colors
        [ResponseType(typeof(Kurs.Admin.Repository.Color))]
        public IHttpActionResult PostColor(Kurs.Admin.Repository.Color model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var color = new Color { Id = model.Id, Title = model.Title };
            db.Colors.Add(color);
            db.SaveChanges();
            model.Id = color.Id;
            return CreatedAtRoute("DefaultApi", new { id = color.Id }, model);
        }

        // DELETE: api/Colors/5
        [ResponseType(typeof(Kurs.Admin.Repository.Color))]
        public IHttpActionResult DeleteColor(int id)
        {
            Color color = db.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Color { Id = color.Id, Title = color.Title };
            db.Colors.Remove(color);
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

        private bool ColorExists(int id)
        {
            return db.Colors.Count(e => e.Id == id) > 0;
        }
    }
}