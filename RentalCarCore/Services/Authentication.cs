using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Dtos.Request;
using RentalCarCore.Interfaces;
using RentalCarCore.Utilities;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RentalCarInfrastructure.Interfaces;

namespace RentalCarCore.Services
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenGen _tokenGen;
        private readonly IConfirmationMailService _confirmationMailService;
        private readonly IUnitOfWork _unitOfWork;

        public Authentication(ITokenGen tokenGen, 
                            UserManager<User> userManager, IMapper mapper, 
                            IConfirmationMailService confirmationMailService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _tokenGen = tokenGen;
            _userManager = userManager;
            _confirmationMailService = confirmationMailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserResponseDto>> Login(UserRequestDto userRequestDto)
        {
            User user = await _userManager.FindByEmailAsync(userRequestDto.Email);
            if (user.GetType().GetProperties().All(x => x.GetValue(user) == null))
            {
                throw new Exception("");
            }
            if (user != null && user.EmailConfirmed == true  && user.IsActive == true)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequestDto.Password))
                {
                    string refreshToken = _tokenGen.GenerateRefreshToken();
                    string token = await _tokenGen.GenerateToken(user);
                    user.RefreshToken = refreshToken;
                    user.ExpiryTime = DateTime.Now.AddDays(3);

                    var result = new UserResponseDto()
                    {
                        Id = user.Id,
                        Token = token,
                        RefreshToken = refreshToken
                    };


                    await _userManager.UpdateAsync(user);
                    return new Response<UserResponseDto>
                    {
                        Data = result,
                        Message = MessageResponse.SuccessMessage,
                        ResponseCode = HttpStatusCode.OK,
                        IsSuccessful = true
                    };
                }
                return new Response<UserResponseDto>
                {
                    Message = MessageResponse.FailedMessage,
                    ResponseCode = HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                };
            }
            return new Response<UserResponseDto>
            {
                Message = "Invalid Credentails Or Unconfirmed user",
                ResponseCode = HttpStatusCode.BadRequest,
                IsSuccessful = false,
            };
           
        }

        public async Task<Response<UserResponseDto>> RegisterAsync(RegistrationDto registrationRequest)
        {
            User user = _mapper.Map<User>(registrationRequest);
            user.UserName = registrationRequest.Email;
            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                // assign user to the customer role
                await _userManager.AddToRoleAsync(user, "Customer");

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var answer = new UserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token = emailToken,
                    FirstName = user.FirstName

                };
                await _confirmationMailService.SendAConfirmationEmail(answer, "ConfirmEmail.html");
                return new Response<UserResponseDto>
                {
                    Data = null,
                    Message = "Comfirmation Send to your Mail",
                    ResponseCode = HttpStatusCode.OK,
                    IsSuccessful = true
                };
            }
            string errors = result.Errors.Aggregate(string.Empty, (current, error) => current + (error.Description + Environment.NewLine));
            throw new ArgumentException(errors);
        }
        public async Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequestDTO token)
        {
            var response = new Response<RefreshTokenResponse>();
            var refreshToken = token.RefreshToken;
            var userId = token.UserId;

            var user = await _unitOfWork.UserRepository.GetUser(userId);
            if (user.RefreshToken != refreshToken || user.ExpiryTime != DateTime.Now)
            {
                response.Data = null;
                response.ResponseCode = HttpStatusCode.BadRequest;
                response.Message = "Bad Request";
                response.IsSuccessful = false;
                return response;
            }
            var refreshMapping = new RefreshTokenResponse
            {
                NewAccessToken = await _tokenGen.GenerateToken(user),
                NewRefreshToken = _tokenGen.GenerateRefreshToken()
            };

            user.RefreshToken = refreshMapping.NewRefreshToken;
            await _unitOfWork.UserRepository.UpdateUser(user);
            response.Data = refreshMapping;
            response.ResponseCode = HttpStatusCode.OK;
            response.Message = "Token Refresh Successfully";
            response.IsSuccessful = true;
            return response;
        }

        public async Task<Response<string>> EmailConfirmationAsync(ConfirmEmailRequestDTO confirmEmailRequest)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailRequest.Email);
            if (user != null)
            {
                var decodedToken = TokenConverter.DecodeToken(confirmEmailRequest.Token);
                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

                if (result.Succeeded)
                {
                    var response = new Response<string>()
                    {
                        Message = "Email Confirmation was successful",
                        IsSuccessful = true,
                        ResponseCode = HttpStatusCode.OK,  
                    };

                    return response;
                }
                throw new ArgumentException("Your email could not be confirmed");
            }
            throw new ArgumentException($"User with email '{confirmEmailRequest.Email}' not found");
        }

        public async Task<Response<string>> UpdatePasswordAsync(string Id, UpdatePasswordDTO updatePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var comparePassword = await _userManager.CheckPasswordAsync(user, updatePasswordDto.CurrentPassword);
            if (comparePassword)
            {
                var result = await _userManager.ChangePasswordAsync(user, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);
                if (result.Succeeded)
                {
                    return new Response<string>()
                    {
                        IsSuccessful = true,
                        Message = "Password updated"
                    };

                }
                return new Response<string>()
                {
                    IsSuccessful = false,
                    ResponseCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "password failed, please try again."
                };
            }

            return new Response<string>()
            {
                Message = "Current password is not correct",
                ResponseCode = System.Net.HttpStatusCode.BadRequest,
                IsSuccessful = false
            };
        }

        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                new Response<string>
                {
                    IsSuccessful = false,
                    Message = $"Email {resetPassword.Email} does not exist"
                };
            }
            var token = TokenConverter.DecodeToken(resetPassword.Token);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (result.Succeeded)
            {
                return new Response<string>
                {
                    IsSuccessful = true,
                    Message = "Password has been reset successfully"
                };
            }
            return new Response<string>
            {
                IsSuccessful = false,
                Message = " Password was not reset successfully",
                //Errors = result.GetIdentityErrors()
            };
        }
        public async Task<Response<string>> ForgotPasswordResetAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            var userResponse = _mapper.Map<UserResponseDto>(user);
            var response = new Response<string>
            {
                IsSuccessful = false,
                Message = "A mail has been sent to the specified email address if it exists"
            };
            if (user == null)
            {
                response.IsSuccessful = false;
                return response;
            }
            userResponse.FirstName = user.FirstName;
            userResponse.Token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _confirmationMailService.SendAConfirmationEmail(userResponse, "ForgotPassword.html");
            response.IsSuccessful = true;
            return response;
        }

    }
}
