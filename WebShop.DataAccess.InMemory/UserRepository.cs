using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.DataAccess.InMemory
{
   public class UserRepository
    {


        ObjectCache cache = MemoryCache.Default;
        List<UserAccount> users = new List<UserAccount>();

        public UserRepository()
        {
            users = cache["users"] as List<UserAccount>;
            if (users == null)
            {
                users = new List<UserAccount>();
            }

        }
        public void Commit()
        {
            cache["users"] = users;
        }
        public void Insert(UserAccount p)
        {
            users.Add(p);
        }
        public void Update(UserAccount user)
        {
            UserAccount userToUpdate = users.Find(p => p.Id == user.Id);
            if (userToUpdate != null)
            {
                userToUpdate = user;
            }
            else
            {
                throw new Exception("user not found");
            }
        }
        public UserAccount Find(string Id)
        {
            UserAccount user = users.Find(u => u.Id == Id);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("user not found");
            }
        }
        public IQueryable<UserAccount> Collection()
        {
            return users.AsQueryable();
        }
        public void Delete(string Id)
        {
            UserAccount userToDelete = users.Find(p => p.Id == Id);
            if (userToDelete != null)
            {
                users.Remove(userToDelete);
            }
            else
            {
                throw new Exception("user not found!");
            }
        }

    }
}
