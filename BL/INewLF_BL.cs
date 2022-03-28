using DTO;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL
{
    public interface INewLF_BL
    {
       
        Task<NewLF> getLF(int id);
        Task<List<NewLF>> getLostsOrFounds(int typeLF, int userId);
        Task<int> postNewLF(NewLF newLF);
        Task deleteNewLF(int id);
    }
}