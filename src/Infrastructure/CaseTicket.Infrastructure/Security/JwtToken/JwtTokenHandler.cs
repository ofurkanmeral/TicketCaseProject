using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Infrastructure.Security.JwtToken
{
    public static class JwtTokenHandler
    {
        public static JwtTokenModel CreateToken(IConfiguration configuration)
        {
            JwtTokenModel tokenModel = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["JwtTokenSetting:SecurityKey"]));
            SigningCredentials credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            tokenModel.ExpiresTime = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["JwtTokenSetting:Expiration"]));

            JwtSecurityToken jwtSecurityToken = new(
                issuer: configuration["JwtTokenSetting:Issuer"],
                audience: configuration["JwtTokenSetting:Audinence"],
                expires: tokenModel.ExpiresTime,
                notBefore: DateTime.Now,
                signingCredentials: credentials


                );
            JwtSecurityTokenHandler tokenHandler = new();
            tokenModel.AccessToken=tokenHandler.WriteToken(jwtSecurityToken);

            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            tokenModel.RefreshToken=Convert.ToBase64String(numbers);
            return tokenModel;
        }
    }
}
