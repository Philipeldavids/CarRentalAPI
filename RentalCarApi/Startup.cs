using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentalCarApi.Extentions;
using RentalCarCore.Interfaces;
using RentalCarCore.Services;
using RentalCarCore.Utilities;
using RentalCarApi.Middlewares;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.ModelImage;
using RentalCarInfrastructure.ModelMail;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Implementations;
using RentalCarInfrastructure.Repositories.Interfaces;
using static RentalCarInfrastructure.Seeder.Seeders;
using RentalCarCore.Dtos.Mapping;

namespace RentalCarApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Jwt Authentication and Authorization
            services.ConfigureAuthentication(Configuration);

            // Configure Identity
            services.ConfigureIdentity();
           
            // Register Dependency Injection Service Extension
            services.AddDependencyInjection();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<ImageUploadSettings>(Configuration.GetSection("ImageUploadSettings"));
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContextAndConfigurations(Environment, Configuration);
            services.AddAutoMapper(typeof(UserMappings));
            services.ConfigureCors();
            services.AddSwaggerConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Rental Api v1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            Seeder.Seed(roleManager, userManager, dbContext).GetAwaiter().GetResult();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}