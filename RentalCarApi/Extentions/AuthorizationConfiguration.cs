using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RentalCarCore.Utilities;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarApi.Extentions
{
    public static class AuthorizationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JWTSettings:Audience"],
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminOnly", policy => policy.RequireRole(UserRoles.Admin));
                options.AddPolicy("RequireDealerOnly", policy => policy.RequireRole(UserRoles.Dealer));
                options.AddPolicy("RequireCustomerOnly", policy => policy.RequireRole(UserRoles.Customer));
                options.AddPolicy("RequireDealerAndCustomer", policy => policy.RequireRole(UserRoles.Dealer, UserRoles.Customer));
            });


        }

    }

}
 