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
    public class ScreenResolutionsController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/ScreenResolutions
        public IQueryable<Kurs.Admin.Repository.ScreenResolution> GetScreenResolutions()
        {
            return db.ScreenResolutions.Select(it => new Kurs.Admin.Repository.ScreenResolution { Id = it.Id, Width = it.Width, Height = it.Heigdgt, Diagonal = it.Diagonal });
        }

        // GET: api/ScreenResolutions/5
        [ResponseType(typeof(Kurs.Admin.Repository.ScreenResolution))]
        public IHttpActionResult GetScreenResolution(int id)
        {
            ScreenResolution screenResolution = db.ScreenResolutions.Find(id);
            if (screenResolution == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.ScreenResolution
            {
                Id = screenResolution.Id,
                Diagonal = screenResolution.Diagonal,
                Width = screenResolution.Width,
                Height = screenResolution.Heigdgt
            };
            return Ok(model);
        }

        // PUT: api/ScreenResolutions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScreenResolution(int id, Kurs.Admin.Repository.ScreenResolution model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var screenResolution = db.ScreenResolutions.Find(model.Id);

            screenResolution.Heigdgt = model.Height;
            screenResolution.Width = model.Width;
            screenResolution.Diagonal = model.Diagonal;
            db.Entry(screenResolution).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenResolutionExists(id))
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

        // POST: api/ScreenResolutions
        [ResponseType(typeof(Kurs.Admin.Repository.ScreenResolution))]
        public IHttpActionResult PostScreenResolution(Kurs.Admin.Repository.ScreenResolution model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var screenResolution = new ScreenResolution
            {
                Id = model.Id,
                Diagonal = model.Diagonal,
                Heigdgt = model.Height,
                Width = model.Width
            };
            db.ScreenResolutions.Add(screenResolution);
            db.SaveChanges();
            model.Id = screenResolution.Id;
            return CreatedAtRoute("DefaultApi", new { id = screenResolution.Id }, model);
        }

        // DELETE: api/ScreenResolutions/5
        [ResponseType(typeof(ScreenResolution))]
        public IHttpActionResult DeleteScreenResolution(int id)
        {
            ScreenResolution screenResolution = db.ScreenResolutions.Find(id);
            if (screenResolution == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.ScreenResolution
            {
                Id = screenResolution.Id,
                Width = screenResolution.Width,
                Height = screenResolution.Heigdgt,
                Diagonal = screenResolution.Diagonal
            };
            db.ScreenResolutions.Remove(screenResolution);
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

        private bool ScreenResolutionExists(int id)
        {
            return db.ScreenResolutions.Count(e => e.Id == id) > 0;
        }
    }
}