using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarCore.Interfaces;
using Serilog;

namespace RentalCarApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
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
        [HttpGet("carDetails")]
        public async Task<IActionResult> GetCarDetails(string carId)
        {
            try
            {
                var result = await _carService.GetCarDetailsAsync(carId);
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

        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetAllCars(int pageSize, int pageNumber)
        {
            var carResponse = await _carService.GetAllCarsAsync(pageSize, pageNumber);
            return StatusCode((int) carResponse.ResponseCode, carResponse);
        }

    }
}