using Microsoft.Extensions.Configuration;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Interfaces;
using RentalCarCore.Utilities;
using RentalCarInfrastructure.ModelMail;
using System.Globalization;
using System.Threading.Tasks;

namespace RentalCarCore.Services
{
    public class ConfirmationMailService : IConfirmationMailService
    {
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        public ConfirmationMailService(IMailService mailService, IConfiguration configuration)
        {
            _mailService = mailService;
            _configuration = configuration;
        }
        public async Task SendAConfirmationEmail(UserResponseDto user, string templatefile)
        {
            var template = _mailService.GetEmailTemplate(templatefile);
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;
            var userName = textInfo.ToTitleCase(user.FirstName);
            var encodedToken = TokenConverter.EncodeToken(user.Token);
            
            var link = (templatefile == "ForgotPassword.html") ? $"{_configuration["Application:AppDomain"]}/Authentication/ResetPassword?email={user.Email}&token={encodedToken}"
                : $"{_configuration["Application:AppDomain"]}/Authentication/ConfirmEmail?email={user.Email}&token={encodedToken}";
           
            template = template.Replace("{User}", $"{userName}");
            template = template.Replace("{link}", link);
            
            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Body = template,
                Subject = (templatefile == "ForgotPassword.html") ? "Reset Password" : "Confirm Password"
            };
            await _mailService.SendEmailAsync(mailRequest); 
        }
    }

}
