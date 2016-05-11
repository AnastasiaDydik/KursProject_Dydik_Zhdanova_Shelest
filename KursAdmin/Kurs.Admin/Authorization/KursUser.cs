using Microsoft.AspNet.Identity;

namespace Kurs.Admin.Authorization
{
    public class KursUser : IUser<string>
    {
        public string Id
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public string Password { get; set; }
        /*
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
    UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one 
            // defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity =
                await manager.CreateIdentityAsync(this,
                    DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }*/

    }
}