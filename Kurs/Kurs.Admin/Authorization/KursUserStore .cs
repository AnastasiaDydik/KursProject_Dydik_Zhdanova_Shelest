using Microsoft.AspNet.Identity;
using Kurs.Admin.Repository;
using System.Threading.Tasks;
using System;

namespace Kurs.Admin.Authorization
{
    public class KursUserStore : IUserStore<KursUser>, IUserLockoutStore<KursUser, string>, IUserPasswordStore<KursUser>
    {
        IKursRepository Repository;

        public KursUserStore(IKursRepository repository) : base()
        {
            Repository = repository;
        }
        
        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(string userId)
        {
            return Repository.FindUserById(int.Parse(userId)).Name;
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            return Repository.FindUserByName(userName).Id.ToString();
        }

        /// <summary>
        /// Returns an TUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public KursUser GetUserById(string userId)
        {
            var userModel = Repository.FindUserById(int.Parse(userId));
            if (userModel != null)
            {
                var user = new KursUser();
                user.Id = userModel.Id.ToString();
                user.UserName = userModel.Name;
                user.Password = userModel.Password;

                return user;

            }
            return null;

        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Insert(KursUser user)
        {
            var model = new User
            {
                Name = user.UserName,
                Password = user.Password
            };

            Repository.Create(model);
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Delete(KursUser user)
        {
            var model = Repository.FindUserById(int.Parse(user.Id));
            if (model != null)
                Repository.Delete(model);
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Update(KursUser user)
        {
            var model = Repository.FindUserById(int.Parse(user.Id));
            if (model != null)
            {
                model.Name = user.UserName;
                model.Password = user.Password;

               Repository.Update(model.Id, model);
            }
        }

        public Task CreateAsync(KursUser user)
        {
            return Task.Run(() => Insert(user));
        }

        public Task UpdateAsync(KursUser user)
        {
            return Task.Run(() => Insert(user));
        }

        public Task DeleteAsync(KursUser user)
        {
            return Task.Run(() => Delete(user));
        }

        public Task<KursUser> FindByIdAsync(string userId)
        {
            return Task.Run(() => GetUserById(userId));
        }

        public Task<KursUser> FindByNameAsync(string userName)
        {
            return Task.Run(() =>
            {
                var user = Repository.FindUserByName(userName);
                if(user != null)
                {
                    return new KursUser { Id = user.Id.ToString(), UserName = user.Name, Password = user.Password };
                }
                return null;
            });
        }

        public void Dispose()
        {
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(KursUser user)
        {
            return Task.Run(() => { return new DateTimeOffset(); });
        }

        public Task SetLockoutEndDateAsync(KursUser user, DateTimeOffset lockoutEnd)
        {
            return Task.Run(() => { return; });
        }

        public Task<int> IncrementAccessFailedCountAsync(KursUser user)
        {
            return Task.Run(() => { return 0; });
        }

        public Task ResetAccessFailedCountAsync(KursUser user)
        {
            return Task.Run(() => { return; });
        }

        public Task<int> GetAccessFailedCountAsync(KursUser user)
        {
            return Task.Run(() => { return 0; });
        }

        public Task<bool> GetLockoutEnabledAsync(KursUser user)
        {
            return Task.Run(() => { return false; });
        }

        public Task SetLockoutEnabledAsync(KursUser user, bool enabled)
        {
            return Task.Run(() => { return; });
        }

        public Task SetPasswordHashAsync(KursUser user, string passwordHash)
        {
            return Task.Run(() => { return; });
        }

        public Task<string> GetPasswordHashAsync(KursUser user)
        {
            return Task.Run(() => { return user.Password; });
        }

        public Task<bool> HasPasswordAsync(KursUser user)
        {
            return Task.Run(() => { return true; });
        }
    }
}