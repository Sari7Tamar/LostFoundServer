using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class AdjustmentDL : IAdjustmentDL
    {
        Lost_FindContext lost_FindContext;
        public AdjustmentDL(Lost_FindContext lost_FindContext)
        {
            this.lost_FindContext = lost_FindContext;
        }
        public async Task<List<Adjustment>> getAdjustmentsByLF_Id(int LFid)
        {
            List<Adjustment> goodAjast= new List<Adjustment>();
            LostFound LorF = await lost_FindContext.LostFounds.FindAsync(LFid);
            if(LorF!= null)
            {
                if (LorF.Type == (int)TypeLF.Lost)
                {
                    goodAjast = lost_FindContext.Adjustments
                        .Where(a => a.LostId == LFid).OrderByDescending(i=>i.AdjustmentPercentage)
                        .ToList();
                }
                else
                {
                    goodAjast = lost_FindContext.Adjustments
                        .Where(a => a.FoundId == LFid).OrderByDescending(i => i.AdjustmentPercentage)
                        .ToList();
                }
            }
           return goodAjast;
            
        }
        public async Task<List<Adjustment>> postAdjastments(List<Adjustment> adjastList)
        {
            await lost_FindContext.Adjustments.AddRangeAsync(adjastList);
      
            
            await lost_FindContext.SaveChangesAsync();
            return adjastList;
        }
        public async Task<int> putAdjustment(int id, Adjustment adjust)
        {
            Adjustment adjustmentToUpdate = await lost_FindContext.Adjustments.FindAsync(id);
            if (adjustmentToUpdate == null)
            {
                return 0;
            }
            lost_FindContext.Entry(adjustmentToUpdate).CurrentValues.SetValues(adjust);
            await lost_FindContext.SaveChangesAsync();
            return adjust.Id;
        }
        public async Task deleteAdjustment(int id)
        {
                Adjustment adjustmentToDelete = lost_FindContext.Adjustments.Where(e => e.Id == id).FirstOrDefault();
                if(adjustmentToDelete!=null)
                lost_FindContext.Adjustments.Remove(adjustmentToDelete);
                await lost_FindContext.SaveChangesAsync();
        }

        public async Task incStatusEmail(int adjustRowId)
        {
            Adjustment adjust=await lost_FindContext.Adjustments.FindAsync(adjustRowId);
            Adjustment newAdjust = await lost_FindContext.Adjustments.FindAsync(adjustRowId);
            newAdjust.StatusEmail++;
            lost_FindContext.Entry(adjust).CurrentValues.SetValues(newAdjust);
            await lost_FindContext.SaveChangesAsync();
        }



        /* public async Task deleteAdjustmentsByLFid(int LFid)
         {
             List<Adjustment> Ajusts;
             LostFound LorF = await lost_FindContext.LostFounds.FindAsync(LFid);
             if (LorF.Type == (int)TypeLF.Lost)
             {
                 Ajusts = lost_FindContext.Adjustments
                     .Where(a => a.LostId == LFid)
                     .ToList();

             }
             else
             {
                 Ajusts = lost_FindContext.Adjustments
                     .Where(a => a.FoundId == LFid)
                     .ToList();
             }
             lost_FindContext.Adjustments.RemoveRange(Ajusts);
             await lost_FindContext.SaveChangesAsync();
         }*/
      
    }
}
