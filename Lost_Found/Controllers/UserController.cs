using BL;
using DTO;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lost_Found.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class UserController : ControllerBase
    {
        
        IUserBL userBL;
        ILogger<UserController> logger;

        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
        }



        // GET api/<UserController>/5

        [HttpGet("{id}")]    //אולי לא צריך, אם כן לשנות לטוקן
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await userBL.getUser(id);
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);

        }

        [AllowAnonymous]

        [HttpPost("login")]
        public async Task<ActionResult<User>> Get([FromBody] UserDTO userLogin)
        {
           User user = await userBL.getUser(userLogin.Email, userLogin.Password);
            if (user==null)
            {
                return NoContent();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> Post([FromBody] User user)
        {
            return await userBL.postUser(user);
        }

        // PUT api/<UserController>/5
       // [HttpPut("{userId}/{secondUserId}")]
     

        // DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //     userBL.deleteUser(id);// לא מסכים כי הדליט לא מאפשר אס"א
        //}
    }
}
