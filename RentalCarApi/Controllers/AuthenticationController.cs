using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Dtos.Request;
using RentalCarCore.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using RentalCarCore.Utilities;

namespace RentalCarCore.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto userRequest)
        {
            try
            {
                return Ok(await _authentication.Login(userRequest));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> CreateUserAsync(RegistrationDto userRequest)
        {
            try
            {
                if (!TryValidateModel(userRequest))
                {
                    return BadRequest();
                }
                var result = await _authentication.RegisterAsync((userRequest));
                return Ok(result);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(500, "An error occured we are working on it");
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPatch]
        [Route("Update-password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO updatePasswordDto)
        {
            var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid)
                {
                    var result = await _authentication.UpdatePasswordAsync(userId, updatePasswordDto);
                    return Ok(result);
                }

                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }



        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequestDTO confirmEmailRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authentication.EmailConfirmationAsync(confirmEmailRequestDTO);
                if (!result.IsSuccessful)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authentication.RefreshTokenAsync(request);
                if (!result.IsSuccessful)
                {
                    return BadRequest(result);
                }
                return Ok(result);

            }
            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            try
            {
                return Ok(await _authentication.ResetPasswordAsync(resetPassword));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordReset(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                return Ok(await _authentication.ForgotPasswordResetAsync(forgotPasswordDto));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}


