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
    public class CategoriesController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Categories
        public IQueryable<Kurs.Admin.Repository.Category> GetCategories()
        {
            return db.Categories.Select(it => new Kurs.Admin.Repository.Category { Id = it.Id, Title = it.Title });
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Kurs.Admin.Repository.Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(new Kurs.Admin.Repository.Category { Id = category.Id, Title = category.Title});
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Kurs.Admin.Repository.Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var catecory = db.Categories.Find(model.Id);
            catecory.Title = model.Title;
            db.Entry(catecory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Kurs.Admin.Repository.Category))]
        public IHttpActionResult PostCategory(Kurs.Admin.Repository.Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = new Category { Id = model.Id, Title = model.Title };
            db.Categories.Add(category);
            db.SaveChanges();
            model.Id = category.Id;
            return CreatedAtRoute("DefaultApi", new { id = category.Id }, model);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Kurs.Admin.Repository.Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            var model = new Kurs.Admin.Repository.Category { Id = category.Id, Title = category.Title };
            db.Categories.Remove(category);
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

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}