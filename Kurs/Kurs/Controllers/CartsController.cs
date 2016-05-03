using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Kurs.Storage;

namespace Kurs.Controllers
{
    public class CartsController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/Carts
        public IQueryable<Kurs.Admin.Repository.Cart> GetCarts(int? userId = null, bool loadSoldCarts = false)
        {
            var carts = db.Carts.AsQueryable();
            if (userId.HasValue)
                carts = carts.Where(it => it.UserId == userId.Value);
            if (!loadSoldCarts)
                carts = carts.Where(it => !it.IsSold);

            return carts.Select(it => new Kurs.Admin.Repository.Cart { Id = it.Id, DeviceId = it.DeviceId, IsSold = it.IsSold, Quantity = it.Quantity, UserId = it.UserId });
        }

        // GET: api/Carts/5
        [ResponseType(typeof(Kurs.Admin.Repository.Cart))]
        public IHttpActionResult GetCart(int id)
        {
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            var model = new Kurs.Admin.Repository.Cart
            {
                Id = cart.Id,
                DeviceId = cart.DeviceId,
                IsSold = cart.IsSold,
                Quantity = cart.Quantity,
                UserId = cart.UserId
            };
            return Ok(model);
        }

        // PUT: api/Carts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCart(int id, Kurs.Admin.Repository.Cart model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var cart = db.Carts.Find(model.Id);
            cart.DeviceId = model.DeviceId;
            cart.IsSold = model.IsSold;
            cart.UserId = model.UserId;
            cart.Quantity = model.Quantity;

            db.Entry(cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        [ResponseType(typeof(Kurs.Admin.Repository.Cart))]
        public IHttpActionResult PostCart(Kurs.Admin.Repository.Cart model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart = new Cart
            {
                Id = model.Id,
                DeviceId = model.DeviceId,
                IsSold = model.IsSold,
                Quantity = model.Quantity,
                UserId = model.UserId
            };

            db.Carts.Add(cart);

            try
            {
                db.SaveChanges();
                model.Id = cart.Id;
            }
            catch (DbUpdateException)
            {
                if (CartExists(cart.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(Kurs.Admin.Repository.Cart))]
        public IHttpActionResult DeleteCart(int id)
        {
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }
            var model = new Kurs.Admin.Repository.Cart
            {
                Id = cart.Id,
                DeviceId = cart.DeviceId,
                IsSold = cart.IsSold,
                Quantity = cart.Quantity,
                UserId = cart.UserId
            };
            db.Carts.Remove(cart);
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

        private bool CartExists(int id)
        {
            return db.Carts.Count(e => e.Id == id) > 0;
        }
    }
}