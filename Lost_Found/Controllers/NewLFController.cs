using BL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using DTO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860



namespace Lost_Found.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class NewLFController : ControllerBase
    {
        INewLF_BL newLF_BL;
        public NewLFController(INewLF_BL newLF_BL)
        {
            this.newLF_BL = newLF_BL;
        }

        // GET api/<LFController>/5
        [HttpGet("{id}")]
        public Task<NewLF> Get(int id)
        {
            return newLF_BL.getLF(id);
        }

        // GET: api/<LFController>
        [HttpGet("{typeLF}/{userId}")]
     
        public Task<List<NewLF>> getLostsOrFounds(int typeLF, int userId)
        {
            return newLF_BL.getLostsOrFounds(typeLF, userId);
           
        }

        // POST api/<LFController>
        [HttpPost]
        public Task<int> Post([FromBody] NewLF newLF)
        {
            return newLF_BL.postNewLF(newLF);
        }

        //// PUT api/<LFController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}


        // DELETE api/<LFController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await newLF_BL.deleteNewLF(id);

        }
        
        
    }
}
