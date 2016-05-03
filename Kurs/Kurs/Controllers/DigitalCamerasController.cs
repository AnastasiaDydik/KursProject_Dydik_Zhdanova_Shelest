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
    public class DigitalCamerasController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/DigitalCameras
        public IQueryable<Kurs.Admin.Repository.DigitalCamera> GetDigitalCameras()
        {
            return db.DigitalCameras.Select(it => new Kurs.Admin.Repository.DigitalCamera { Id = it.Id, Width = it.Width, Height = it.Height });
        }

        // GET: api/DigitalCameras/5
        [ResponseType(typeof(Kurs.Admin.Repository.DigitalCamera))]
        public IHttpActionResult GetDigitalCamera(int id)
        {
            DigitalCamera digitalCamera = db.DigitalCameras.Find(id);
            if (digitalCamera == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.DigitalCamera
            {
                Id = digitalCamera.Id,
                Height = digitalCamera.Height,
                Width = digitalCamera.Width
            };
            return Ok(model);
        }

        // PUT: api/DigitalCameras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDigitalCamera(int id, Kurs.Admin.Repository.DigitalCamera model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var digitalCamera = new DigitalCamera
            {
                Id = model.Id,
                Height = model.Height,
                Width = model.Width
            };
            db.Entry(digitalCamera).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DigitalCameraExists(id))
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

        // POST: api/DigitalCameras
        [ResponseType(typeof(Kurs.Admin.Repository.DigitalCamera))]
        public IHttpActionResult PostDigitalCamera(Kurs.Admin.Repository.DigitalCamera model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var digitalCamera = new DigitalCamera
            {
                Id = model.Id,
                Height = model.Height,
                Width = model.Width
            };

            db.DigitalCameras.Add(digitalCamera);
            db.SaveChanges();
            model.Id = digitalCamera.Id;
            return CreatedAtRoute("DefaultApi", new { id = digitalCamera.Id }, model);
        }

        // DELETE: api/DigitalCameras/5
        [ResponseType(typeof(Kurs.Admin.Repository.DigitalCamera))]
        public IHttpActionResult DeleteDigitalCamera(int id)
        {
            DigitalCamera digitalCamera = db.DigitalCameras.Find(id);
            if (digitalCamera == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.DigitalCamera
            {
                Id = digitalCamera.Id,
                Height = digitalCamera.Height,
                Width = digitalCamera.Width
            };

            db.DigitalCameras.Remove(digitalCamera);
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

        private bool DigitalCameraExists(int id)
        {
            return db.DigitalCameras.Count(e => e.Id == id) > 0;
        }
    }
}