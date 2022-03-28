using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using DTO;
using Entities;


namespace BL
{
    public class NewLF_BL : INewLF_BL
    {
        ILF_DL LF_DL;
        public NewLF_BL(ILF_DL LF_DL)
        {
            this.LF_DL = LF_DL;
        }
       
        public async Task<NewLF> getLF(int id)
        {
            return await LF_DL.getLF(id);
        }
        public async Task<List<NewLF >> getLostsOrFounds(int typeLF, int userId)
        {
            return await LF_DL.getLostsOrFounds(typeLF, userId);
        }
        public async Task<int> postNewLF(NewLF newLF)
        {
            newLF.LF.Id = await LF_DL.postLF(newLF);
            return newLF.LF.Id;
        }

        public async Task deleteNewLF(int id)
        {
            await LF_DL.deleteNewLF(id);
        }

    }
}
