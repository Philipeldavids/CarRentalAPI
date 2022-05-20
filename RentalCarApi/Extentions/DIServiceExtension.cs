using Microsoft.Extensions.DependencyInjection;
using RentalCarCore.Implementations;
using RentalCarCore.Interfaces;
using RentalCarCore.Services;
using RentalCarInfrastructure.Interfaces;
using RentalCarInfrastructure.ModelImage;
using RentalCarInfrastructure.ModelMail;
using RentalCarInfrastructure.Repositories.Implementations;
using RentalCarInfrastructure.Repositories.Interfaces;

namespace RentalCarApi.Extentions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAuthentication, Authentication>();
            services.AddScoped<ITokenGen, TokenGen>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IConfirmationMailService, ConfirmationMailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();    
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
        }
    }
}