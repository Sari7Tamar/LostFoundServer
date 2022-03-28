using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DL
{
    public interface IAdjustmentDL
    {
        Task deleteAdjustment(int id);
        Task<List<Adjustment>> getAdjustmentsByLF_Id(int LFid);
        Task<List<Adjustment>> postAdjastments(List<Adjustment> adjastList);
        Task<int> putAdjustment(int id, Adjustment adjast);

        //Task deleteAdjustmentsByLFid(int LFid);
    }
}