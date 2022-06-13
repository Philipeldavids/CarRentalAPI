using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentalCarCore.Dtos;
using RentalCarCore.Interfaces;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Implementations
{
    public class TokenGen : ITokenGen
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public TokenGen(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }


        public async Task<string> GenerateToken(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Avatar", user.Avatar),
            };

            //Gets the roles of the logged in user and adds it to Claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
            var userToken = new JwtSecurityToken(
                audience: _configuration["JWTSettings: Audience"],
                issuer: _configuration["JWTSettings:Issuer"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)

                );
            return new JwtSecurityTokenHandler().WriteToken(userToken);
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}