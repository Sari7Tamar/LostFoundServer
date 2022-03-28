using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UserDL : IUserDL
    {
        Lost_FindContext lost_FindContext;
        public UserDL(Lost_FindContext lost_FindContext)
        {
            this.lost_FindContext = lost_FindContext;
        }
        public async Task<User> getUser(int id)
        {
           return await lost_FindContext.Users.FindAsync(id);
        }
        public async Task<User> getUser(string email, string password)
        {
            return  lost_FindContext.Users.FirstOrDefault(i => i.Email == email && i.Password == password);

        }
        public async Task<int> postUser(User user)
        {
            User userWithSameEmail=lost_FindContext.Users.FirstOrDefault(i => i.Email == user.Email);
            if(userWithSameEmail==null)
            {
                await lost_FindContext.Users.AddAsync(user);
                await lost_FindContext.SaveChangesAsync();
                return user.Id;
            }
            return 0;
        }
        public async Task<User> putUser(int id, User user)
        {
            User userToUpdate=await lost_FindContext.Users.FindAsync(id);
            if (userToUpdate == null)
            {
                return null;
            }
            lost_FindContext.Entry(userToUpdate).CurrentValues.SetValues(user);
            await lost_FindContext.SaveChangesAsync();
            return user;
        }
        public async Task deleteUser(int id)
        {
            User userToDelete= lost_FindContext.Users.Where(e => e.Id == id).FirstOrDefault();
            if (userToDelete!= null)
                lost_FindContext.Users.Remove(userToDelete);
            await lost_FindContext.SaveChangesAsync();
        }
    }

}
