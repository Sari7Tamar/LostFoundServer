using Entities;
using System.Threading.Tasks;

namespace DL
{
    public interface IUserDL
    {
        Task<User> getUser(int id);
        Task<User> getUser(string name, string password);
        Task<int> postUser(User user);
        Task<User> putUser(int id, User user);
        Task deleteUser(int id);
    }
}