using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DB_Test.EntityContext;
using DB_Test.Services;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DB_Test.Services.AuthService
{
    public class AuthService: IAuthService
    {
        // Generates JWT
        private IConfiguration Config { get; }
        private readonly DbContextOptions<ApplicationContext> _options;
        
        public AuthService(IConfiguration config)
        {
            Config = config;
             var builder = new DbContextOptionsBuilder<ApplicationContext>();
            _options = builder.UseNpgsql(Config.GetConnectionString("ResultTestsDatabase")).Options;
        }

        public string GetAuthData(string email, Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(Config["JwtExpires"]));

            var token = new JwtSecurityToken(
                Config["JwtIssuer"],
                Config["JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetHashedPassword(string pass) => Crypto.HashPassword(pass);

        public bool ValidateUserPassword(string hashed, string pass) => Crypto.VerifyHashedPassword(hashed, pass);
    }
}
