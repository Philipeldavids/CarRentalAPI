﻿using Microsoft.Extensions.Configuration;
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
        public TokenGen(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
            var userToken = new JwtSecurityToken(audience: _configuration
                ["JWTSettings: Audience"],
                issuer: _configuration["JWTSettings:Issuer"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)

                );
            return new JwtSecurityTokenHandler().WriteToken(userToken);
        }
<<<<<<< HEAD
        public string GenerateRefreshToken()
        {
            return  Guid.NewGuid().ToString();
=======
<<<<<<< HEAD
        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
=======

        public string GenerateRefreshToken(User user)
        {
            throw new NotImplementedException();
>>>>>>> reviews
>>>>>>> 9f7bd7411c370e2fcee6076d7a19d140eebbbb92
        }
    }
}