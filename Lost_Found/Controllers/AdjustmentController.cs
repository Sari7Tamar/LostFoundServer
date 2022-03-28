using BL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lost_Found.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   /* [Authorize]*/
    public class AdjustmentController : ControllerBase
    {
        IAdjustmentBL adjustmentBL;
        public AdjustmentController(IAdjustmentBL adjustmentBL)
        {
            this.adjustmentBL = adjustmentBL;
        }


        // GET api/<MatchController>/5
        [HttpGet("LF/{id}")]
        public Task<List<Adjustment>> Get(int id)
        {
            return adjustmentBL.getAdjustmentsByLF_Id(id);
        }

        //// POST api/<MatchController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<MatchController>/5
        [HttpPut("{id}")]
        public Task<int> Put(int id, [FromBody] Adjustment adjust)//  אולי לקבל סטטוס ושורת התאמה ואז לעדכן סטטוס 
        {
            return adjustmentBL.putAdjustment(id, adjust);
        }

        // DELETE api/<MatchController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
           await adjustmentBL.deleteAdjustment(id);
        }
        // POST api/<MatchController>
        [HttpPost("sendEmail")]
        public Task<int> sendEmail([FromBody] Adjustment adjustment)
        {
            return adjustmentBL.sendEmail(adjustment);
        }

    }
}
