using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarCore.Dtos.Request;
using RentalCarCore.Interfaces;
using Serilog;

namespace RentalCarApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        public CarsController(ICarService carService, IUserService userService)
        {
            _carService = carService;
            _userService = userService;
        }

        [HttpGet("GetFeaturedCars")]
        public async Task<IActionResult> GetFeaturedCars()
        {
            try
            {
                var result = await _carService.GetListOfFeatureCarsAsync();
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
        [HttpGet("Id")]
        public async Task<IActionResult> GetCarDetails(string Id)
        {
            try
            {
                var result = await _carService.GetCarDetailsAsync(Id);
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

        [HttpGet()]
        public async Task<IActionResult> GetAllCars(int pageSize, int pageNumber)
        {
            var carResponse = await _carService.GetAllCarsAsync(pageSize, pageNumber);
            return StatusCode((int) carResponse.ResponseCode, carResponse);
        }

        [HttpPost("AddRating")]
        public async Task<IActionResult> AddRating(RatingDto ratingDto)
        {
            try
            {
                if (!ModelState.IsValid)
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


        [HttpGet("SearchCars")]
        public async Task<IActionResult> GetSearchCars(string state, DateTime pickupDate, DateTime returnDate)
        {
            try
            {
                var result = await _carService.GetCarsBySearchAsync(state, pickupDate, returnDate);
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
    }
}