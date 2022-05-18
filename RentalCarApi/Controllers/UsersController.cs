using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarCore.Interfaces;
using RentalCarInfrastructure.ModelImage;
using Serilog;

namespace RentalCarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        public UsersController(IUserService userService, IImageService imageService)
        {
            _userService = userService;
            _imageService = imageService;
        }

        [HttpGet("UserId/GetUserTrips")]
        public async Task<IActionResult> GetUserTrips(string Userid)
        {
            try
            {
                var result = await _userService.GetTrips(Userid);
                if (result.IsSuccessful)
                {
                    return Ok(result);
                }

                return BadRequest(result);
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

        [HttpPatch("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] AddImageDto imageDto)
        {
            try
            {
                var upload = await _imageService.UploadAsync(imageDto.Image);
                var result = new ImageAddedDto()
                {
                    PublicId = upload.PublicId,
                    Url = upload.Url.ToString()
                };
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}