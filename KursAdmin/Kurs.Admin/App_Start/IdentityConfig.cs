using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Kurs.Admin.Authorization;
using Kurs.Admin.Repository;
using System.Collections.Generic;

namespace Kurs.Admin
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<KursUser>
    {
        readonly KursUserStore store;
        public ApplicationUserManager(KursUserStore store)
            : base(store)
        {
            this.store = store;
        }

        public override async Task<KursUser> FindAsync(string userName, string password)
        {
            var user = await store.FindByNameAsync(userName);

            if (user == null) return null;

            var isUserLockedOut = await store.GetLockoutEnabledAsync(user);

            if (isUserLockedOut) return user;

            var isPasswordValid = await CheckPasswordAsync(user, password);

            if (isPasswordValid)
            {
                await store.ResetAccessFailedCountAsync(user);
            }
            else
            {

                user = null;
            }

            return user;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var store = new KursUserStore(context.Get<KursRepository>());

            var manager = new ApplicationUserManager(store);

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<KursUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };



            // Configure user lockout defaults
            //manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<KursUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await store.FindByIdAsync(userId);
            return await store.IsInRoleAsync(user, role);
        }

    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<KursUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /*public override Task<ClaimsIdentity> CreateUserIdentityAsync(KursUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }*/

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = UserManager.FindByName(userName);
            
            if (user == null)
                return SignInStatus.Failure;
            var isAdmin = UserManager.IsInRole(user.Id.ToString(), "Администратор");
            if (!isAdmin)
                return SignInStatus.Failure;

            if (user.Password == password)
            {
                await SignInAsync(user, isPersistent, true);
                return SignInStatus.Success;
            }
            return SignInStatus.Failure;
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
