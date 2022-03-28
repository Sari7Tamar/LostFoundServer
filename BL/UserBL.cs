
using DL;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class UserBL : IUserBL

    {
        IUserDL userDL;
        IConfiguration configuration;
        public UserBL(IUserDL userDL, IConfiguration configuration)
        {
            this.userDL = userDL;
            this.configuration = configuration;
        }
       
        public async Task<User> getUser(int id)
        {
            return await userDL.getUser(id);
        }
        public async Task<User> getUser(string email, string password)
        {

            User user = await userDL.getUser(email, password);
            if (user == null) return null;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return WithoutPassword(user);
               
        }
        public static User WithoutPassword(User user)
        {
            user.Password = null;
            return user;
        }
        public async Task<int> postUser(User user)
        {
            
            int id=await userDL.postUser(user);
            if (id == 0) return 0;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return id;

        }
        public async Task<User> putUser(int id, User user)
        {
            return await userDL.putUser(id, user);
        }
        public  async Task deleteUser(int id) 
        {
            await userDL.deleteUser(id);

        }
    }
}
