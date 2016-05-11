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
    public class ReviewsController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Reviews
        public IQueryable<Kurs.Admin.Repository.Review> GetReviews(int? deviceId = null)
        {
            var reviews = db.Reviews.AsQueryable();
            if (deviceId.HasValue)
                reviews = reviews.Where(it => it.DeviceId == deviceId.Value);
            return reviews.Select(it => new Kurs.Admin.Repository.Review { Id = it.Id, Content = it.Content, DeviceId = it.DeviceId });
        }

        // GET: api/Reviews/5
        [ResponseType(typeof(Kurs.Admin.Repository.Review))]
        public IHttpActionResult GetReview(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Review
            {
                Id = review.Id,
                Content = review.Content,
                DeviceId = review.DeviceId
            };
            return Ok(model);
        }

        // PUT: api/Reviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReview(int id, Kurs.Admin.Repository.Review model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var review = db.Reviews.Find(model.Id);
            review.Content = model.Content;
            review.DeviceId = model.DeviceId;

            db.Entry(review).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        [ResponseType(typeof(Kurs.Admin.Repository.Review))]
        public IHttpActionResult PostReview(Kurs.Admin.Repository.Review model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var review = new Review
            {
                Id = model.Id,
                Content = model.Content,
                DeviceId = model.DeviceId
            };
            db.Reviews.Add(review);
            db.SaveChanges();
            model.Id = review.Id;
            return CreatedAtRoute("DefaultApi", new { id = review.Id }, model);
        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult DeleteReview(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Review
            {
                Id = review.Id,
                Content = review.Content,
                DeviceId = review.DeviceId
            };
            db.Reviews.Remove(review);
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

        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.Id == id) > 0;
        }
    }
}