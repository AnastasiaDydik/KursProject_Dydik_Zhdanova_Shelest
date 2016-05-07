using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Kurs.Storage;
using Kurs.Admin.Repository;

namespace Kurs.Controllers
{
    public class UserRolesController : ApiController
    {
        private KursDbEntities db = new KursDbEntities();

        // GET: api/UserRoles
        public IEnumerable<UserRole> GetUserRoles()
        {

            var result = new List<UserRole>();
           
            foreach(var role in db.Roles)
            {
                var userRoles = role.Users.Select(it => new UserRole { UserId = it.Id, RoleId = role.Id });
                result.AddRange(userRoles);
            }
            return result;
        }

        // GET: api/UserRoles/5
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult GetUserRoles(int id, int roleId)
        {
            var user = db.Users.Find(id);
            UserRole result = null;
            if(user != null)
            {
                var isUserInRole = user.Roles.Any(it => it.Id == roleId);
                result = isUserInRole ? new UserRole { UserId = id, RoleId = roleId }:null;
                return Ok(result);
            }

            return Ok(result);
        }


        // POST: api/UserRoles
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult PostUserRoles(UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = db.Users.Find(userRole.UserId);
            var role = db.Roles.Find(userRole.RoleId);
            if (user != null && role != null)
            {
                user.Roles.Add(role);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = userRole.UserId, RoleId = userRole.RoleId }, userRole);
        }

        // DELETE: api/UserRoles/5
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult DeleteUserRoles(int id, int roleId)
        {
            var user = db.Users.Find(id);
            var role = db.Roles.Find(roleId);

            if(user != null && role != null)
            {
                var isUserInRole = user.Roles.Any(it => it.Id == role.Id);
                if (isUserInRole)
                {
                    user.Roles.Remove(role);
                    db.SaveChanges();
                }
                return Ok(new UserRole { UserId = id, RoleId = roleId });
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}