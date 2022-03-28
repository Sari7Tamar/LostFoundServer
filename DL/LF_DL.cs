using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class LF_DL: ILF_DL
    {
        Lost_FindContext lost_FindContext;
        IMapper mapper;
        public LF_DL(Lost_FindContext lost_FindContext, IMapper mapper)
        {
            this.lost_FindContext = lost_FindContext;
            this.mapper = mapper;
        }

        public async Task<NewLF> getLF(int id)
        {
            LostFound lf= await lost_FindContext.LostFounds.Where(lf=>lf.Id==id).Include(l => l.Addresses).Include(l => l.PublicTransports).FirstOrDefaultAsync();

            NewLF newlf = mapper.Map<LostFound, NewLF>(lf);
            return newlf;
        }

        public async Task<List<NewLF>> getLostsOrFounds(int typeLF, int userId)
        {
            List<LostFound> listLF;
            
            if (userId==0)
            {
                listLF= await lost_FindContext.LostFounds
               .Where(i => i.Type == typeLF).Include(l => l.Addresses).Include(l => l.PublicTransports)
               .ToListAsync();
            }
            else
            {
                 listLF= await lost_FindContext.LostFounds
                .Where(i => i.Type == typeLF && i.UserId==userId).Include(l => l.Addresses).Include(l => l.PublicTransports)
                .ToListAsync();
            }
            List<NewLF> newLFList = mapper.Map<List<LostFound>, List<NewLF>>(listLF);
            return newLFList; 
        }
        public async Task<int> postLF(NewLF newLF)
        {
            LostFound lf = mapper.Map<NewLF, LostFound>(newLF);

            /*LostFound lostFound = newLF.LF;
            if (newLF.address != null)
            {
               
            
            
            
            
            lostFound.Addresses.Add(newLF.address);
            }
            
            else
                lostFound.PublicTransports.Add(newLF.publicTransport);*/

            await lost_FindContext.LostFounds.AddAsync(lf);
          
            await lost_FindContext.SaveChangesAsync();
            return lf.Id;
        }

        public async Task deleteNewLF(int id)
        {
            LostFound lf= lost_FindContext.LostFounds.Where(e => e.Id == id).FirstOrDefault();
            if (lf!=null)
            {
                lost_FindContext.LostFounds.Remove(lf);
                await lost_FindContext.SaveChangesAsync();

            }
        }
    }

  
}
