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
    public class ProcessorsController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Processors
        public IQueryable<Kurs.Admin.Repository.Processor> GetProcessors()
        {
            return db.Processors.Select(it => new Kurs.Admin.Repository.Processor { Id = it.Id, Cores = it.Cores, Frequency = it.Frequency, Title = it.Title });
        }

        // GET: api/Processors/5
        [ResponseType(typeof(Kurs.Admin.Repository.Processor))]
        public IHttpActionResult GetProcessor(int id)
        {
            Processor processor = db.Processors.Find(id);
            if (processor == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Processor
            {
                Id = processor.Id,
                Cores = processor.Cores,
                Frequency = processor.Frequency,
                Title = processor.Title
            };
            return Ok(model);
        }

        // PUT: api/Processors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcessor(int id, Kurs.Admin.Repository.Processor model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var processor = db.Processors.Find(model.Id);
            processor.Cores = model.Cores;
            processor.Frequency = model.Frequency;
            processor.Title = model.Title;
            db.Entry(processor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessorExists(id))
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

        // POST: api/Processors
        [ResponseType(typeof(Kurs.Admin.Repository.Processor))]
        public IHttpActionResult PostProcessor(Kurs.Admin.Repository.Processor model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var processor = new Processor
            {
                Id = model.Id,
                Cores = model.Cores,
                Frequency = model.Frequency,
                Title = model.Title
            };
            db.Processors.Add(processor);
            db.SaveChanges();
            model.Id = processor.Id;
            return CreatedAtRoute("DefaultApi", new { id = processor.Id }, model);
        }

        // DELETE: api/Processors/5
        [ResponseType(typeof(Kurs.Admin.Repository.Processor))]
        public IHttpActionResult DeleteProcessor(int id)
        {
            Processor processor = db.Processors.Find(id);
            if (processor == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Processor
            {
                Id = processor.Id,
                Cores = processor.Cores,
                Frequency = processor.Frequency,
                Title = processor.Title
            };
            db.Processors.Remove(processor);
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

        private bool ProcessorExists(int id)
        {
            return db.Processors.Count(e => e.Id == id) > 0;
        }
    }
}