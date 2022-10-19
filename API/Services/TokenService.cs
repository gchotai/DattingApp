using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Enitites;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config) {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user) {
            var cread = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.Username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = cread
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}


// var cread = new SigningCredentials(_key, SecurityAlgorithms.Aes256CbcHmacSha512);
            
//             var claims = new List<Claim> { 
//                 new Claim(JwtRegisteredClaimNames.NameId,user.Username)
//             };

//             var tokenDescriptor = new SecurityTokenDescriptor{
//                 Subject = new ClaimsIdentity(claims),
//                 Expires = DateTime.Now.AddDays(7),
//                 SigningCredentials = cread
//             };            

//             var tokenHandler = new JwtSecurityTokenHandler();

//             var token = tokenHandler.CreateToken(tokenDescriptor);
            
//             return tokenHandler.WriteToken(token);