using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL
{
    public interface IAdjustmentBL
    {
        Task<int> sendEmail(Adjustment adjustRow);
        Task<List<Adjustment>> getAdjustmentsByLF_Id(int LFid);
        Task<int> putAdjustment(int id, Adjustment adjast);
        Task deleteAdjustment(int id);
    }
}