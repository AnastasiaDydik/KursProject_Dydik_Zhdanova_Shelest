using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Kurs.Storage;

namespace Kurs.Controllers
{
    public class CountriesController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Countries
        public IQueryable<Kurs.Admin.Repository.Country> GetCountries()
        {
            return db.Countries.Select(it => new Kurs.Admin.Repository.Country { Id = it.Id, Title = it.Title });
        }

        // GET: api/Countries/5
        [ResponseType(typeof(Kurs.Admin.Repository.Country))]
        public IHttpActionResult GetCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Country { Id = country.Id, Title = country.Title };
            return Ok(country);
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCountry(int id, Kurs.Admin.Repository.Country model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }
            var country = db.Countries.Find(model.Id);
            country.Title = model.Title;
            db.Entry(country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        [ResponseType(typeof(Kurs.Admin.Repository.Country))]
        public IHttpActionResult PostCountry(Kurs.Admin.Repository.Country model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var country = new Country { Id = model.Id, Title = model.Title };
            db.Countries.Add(country);
            db.SaveChanges();
            model.Id = country.Id;
            return CreatedAtRoute("DefaultApi", new { id = country.Id }, model);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(Kurs.Admin.Repository.Country))]
        public IHttpActionResult DeleteCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Country { Id = country.Id, Title = country.Title };
            db.Countries.Remove(country);
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

        private bool CountryExists(int id)
        {
            return db.Countries.Count(e => e.Id == id) > 0;
        }
    }
}