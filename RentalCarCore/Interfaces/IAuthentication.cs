using RentalCarCore.Dtos.Response;
using RentalCarCore.Dtos.Request;
using System.Threading.Tasks;

namespace RentalCarCore.Interfaces
{
    public interface IAuthentication
    {
        Task<Response<UserResponseDto>> Login(UserRequestDto userRequestDto);
        Task<Response<UserResponseDto>> RegisterAsync(RegistrationDto registrationRequest);
        Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequestDTO token);
        Task<Response<string>> EmailConfirmationAsync(ConfirmEmailRequestDTO confirmEmailRequest);
        Task<Response<string>> UpdatePasswordAsync(string Id, UpdatePasswordDTO updatePasswordDto);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<Response<string>> ForgotPasswordResetAsync(ForgotPasswordDto forgotPasswordDto);
    }
}