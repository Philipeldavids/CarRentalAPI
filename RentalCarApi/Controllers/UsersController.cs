using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarCore.Dtos.Request;
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

        [Authorize]
        [HttpPut]
        [Route("Id")]
        public async Task<IActionResult> UpdatePassword(string Id, UpdateUserDto updateUserdDto)
        {


            //var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid)
                {
                    var result = await _userService.UpdateUserDetails(Id, updateUserdDto);
                    return Ok(result);
                }

                return BadRequest(ModelState);

            }
            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Try again after 5 minutes");
            }

        }

        [HttpPost("AddRating")]
        public async Task<IActionResult> AddRating(RatingDto ratingDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid)
                {
                    var result = await _userService.AddRating(ratingDto);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch(ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, try again after 5 minutes");
            }
        }
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(CommentDto commentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid)
                {
                    var result = await _userService.AddComment(commentDto);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, try again after 5 minutes");

            }
        }
        [HttpGet("UserId")]
        public async Task<IActionResult>GetUser(string userId)
        {
            try
            {
                return Ok(await _userService.GetUser(userId));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);

            }
        }

        [HttpGet("GetAllUsers")]
       [Authorize(Roles = "Admin")]
       public async Task<IActionResult> GetAllUser(int pageSize, int pageNumber)
        {
            var response = await _userService.GetUsersAsync(pageSize, pageNumber);
            return StatusCode((int)response.ResponseCode, response);
        }
    }
}