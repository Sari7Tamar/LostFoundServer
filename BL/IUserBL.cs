using Entities;
using System.Threading.Tasks;

namespace BL
{
    public interface IUserBL
    {
        Task deleteUser(int id);
        Task<User> getUser(int id);
        Task<User> getUser(string name, string password);
        Task<int> postUser(User user);
        //Task<User> putUser(int id, User user);
       
    }
}