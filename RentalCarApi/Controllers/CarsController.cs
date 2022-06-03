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
            return StatusCode((int)carResponse.ResponseCode, carResponse);
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
                    var result = await _carService.AddRating(ratingDto);
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
                    var result = await _carService.AddComment(commentDto);
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

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while processing your request, please try again");

            }
        }


        [HttpGet("GetAllOfferedCars")]
        public async Task<IActionResult> GetAllOfferCars(int pageSize, int pageNumber)
        {
            try
            {
                var carOffer = await _carService.GetAllOfferCarsAsync(pageSize, pageNumber);
                if (carOffer.IsSuccessful)
                {
                    return StatusCode((int)carOffer.ResponseCode, carOffer);
                }

                return BadRequest(carOffer);

            }
            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while processing your request, please try again");

            }
        }

        [HttpPost("PaymentForCarTrip")]
        public async Task<IActionResult> MakeCarPayment(PaymentRequestDTO request)
        {
            try
            {
                var result = await _userService.UserPayment(request);
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

        [HttpPost("BookTrip")]
        public async Task<IActionResult> BookTrip(TripBookingRequestDTO tripRequest)
        {

            try
            {
                var trip = await _carService.BookTripAsync(tripRequest);
                if (trip.IsSuccessful)
                {
                    return Ok(trip);
                }

                return BadRequest(trip);
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

        [HttpPost("verifypayment")]
        public IActionResult ConfirmPayment(string reference)
        {
            try
            {
                var result =  _userService.VerifyPayment(reference);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while processing your request, please try again");

            }
        }

        [HttpDelete("DeleteACar")]
        public async Task<IActionResult> DeleteACar(string carId, string dealerId)
        {

            try
            {
                var obj = await _carService.DeleteCar(carId, dealerId);
                if (obj.IsSuccessful)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while processing your request, please try again");
            }
        }

        [HttpPost("AddNewCar")]
        public async Task<IActionResult> AddACar(CarRequestDTO requestDTO)
        {
            try
            {
                var addCar = await _carService.DealerAddCar(requestDTO);
                if (addCar.IsSuccessful)
                {
                    return Ok(addCar);
                }

                return BadRequest(addCar);
            }

            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while processing your request, please try again");

            }
        }

        [HttpPut("id")]
        //[Authorize(Roles = "Dealer")]
        public async Task<IActionResult> UpdateCar(string carId, CarUpdateDto carUpdateDto)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _carService.EditCar(carId, carUpdateDto);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Try again later");
            }

        }
    }
}