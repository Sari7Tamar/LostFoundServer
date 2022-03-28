using DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public interface ILF_DL
    {
        Task<NewLF> getLF(int id);
        Task<List<NewLF>> getLostsOrFounds(int typeLF, int userId);
        Task<int> postLF(NewLF LF);
        Task deleteNewLF(int id);
    }
}
