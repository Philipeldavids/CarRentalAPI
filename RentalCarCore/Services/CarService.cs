using AutoMapper;
using RentalCarCore.Dtos.Request;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Interfaces;
using RentalCarCore.Utilities.Pagination;
using RentalCarInfrastructure.Interfaces;
using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _uintOfWork;
        private readonly IMapper _mapper;
        public CarService(IUnitOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<CarFeatureDTO>>> GetListOfFeatureCarsAsync()
        {
            var car = await _uintOfWork.CarRepository.GetAllFeatureCarsAsync();
            if (car != null)
            {
                var result = _mapper.Map<List<CarFeatureDTO>>(car);
                return new Response<List<CarFeatureDTO>>()
                {
                    Data = result,
                    IsSuccessful = true,
                    Message = "Response Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }

            return new Response<List<CarFeatureDTO>>()
            {
                Data = null,
                IsSuccessful = false,
                Message = "Response NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<Response<CarDetailsDTO>> GetCarDetailsAsync(string carId)
        {
            var car = await _uintOfWork.CarRepository.GetCarDetailsAsync(carId);
            if (car != null)
            {
                var result = _mapper.Map<CarDetailsDTO>(car);
                return new Response<CarDetailsDTO>()
                {
                    Data = result,
                    IsSuccessful = true,
                    Message = "Response Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }

            return new Response<CarDetailsDTO>()
            {
                Data = null,
                IsSuccessful = false,
                Message = "Response NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<Response<PaginationModel<IEnumerable<CarResponseDto>>>> GetAllCarsAsync(int pageSize, int pageNumber)
        {
            var cars = await _uintOfWork.CarRepository.GetAllCarsAsync();
            var carResponse = _mapper.Map<IEnumerable<CarResponseDto>>(cars);
            if(cars != null)
            {
                var carResult = PaginationClass.PaginationAsync(carResponse, pageSize, pageNumber);
                return new Response<PaginationModel<IEnumerable<CarResponseDto>>>
                {
                    Data = carResult,
                    IsSuccessful = true,
                    Message = "List of Cars",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<PaginationModel<IEnumerable<CarResponseDto>>>
            {
                IsSuccessful = false,
                Message = "List of cars Not Found",
                ResponseCode = HttpStatusCode.NoContent,
            };
        }


        public async Task<Response<IEnumerable<CarSearchDto>>> GetCarsBySearchAsync(string Location, DateTime pickupDate, DateTime returnDate)
        {
            var cars = await _uintOfWork.CarRepository.SearchCarByDateAndLocationAsync(Location, pickupDate, returnDate);

            if (cars != null)
            {
                var carResponse = new List<CarSearchDto>();
                foreach (var item in cars)
                {
                    var resp = new CarSearchDto()
                    {
                        Id = item.Key.Id,
                        Rating = item.Key.Ratings.Sum(x => x.Ratings) / item.Key.Ratings.Count,
                        Model = item.Key.Model,
                        YearOfMan = item.Key.YearOfMan,
                        Price = item.Key.Price,
                        NoOfPeople = item.Key.Ratings.Count,
                        UnitOfPrice = item.Key.UnitOfPrice,
                        ImageUrl = item.Key.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                        Avaliabilty = item.Value

                    };

                    carResponse.Add(resp);
                }
                    
                
                return new Response<IEnumerable<CarSearchDto>>
                {
                    Data = carResponse,
                    IsSuccessful = true,
                    Message = "List of Cars Search",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<IEnumerable<CarSearchDto>>
            {
                IsSuccessful = false,
                Message = "Not Car Found",
                ResponseCode = HttpStatusCode.NotFound,
            };
        }

        public async Task<Response<PaginationModel<IEnumerable<CarOfferDto>>>> GetAllOfferCarsAsync(int pageSize, int pageNumber)
        {
            var cars = await _uintOfWork.CarRepository.GetAllOfferCarsAsync();
            var carResponse = _mapper.Map<IEnumerable<CarOfferDto>>(cars);
            if (cars != null)
            {
                var carResult = PaginationClass.PaginationAsync(carResponse, pageSize, pageNumber);
                return new Response<PaginationModel<IEnumerable<CarOfferDto>>>
                {
                    Data = carResult,
                    IsSuccessful = true,
                    Message = "List of Cars' Offers",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<PaginationModel<IEnumerable<CarOfferDto>>>
            {
                IsSuccessful = false,
                Message = "Cars' Offers Not Found",
                ResponseCode = HttpStatusCode.NoContent

            };
        }


        public async Task<Response<string>> AddRating(RatingDto ratingDto)
        {
            var user = await _uintOfWork.UserRepository.GetUser(ratingDto.UserId);
            var trips = await _uintOfWork.UserRepository.GetTripsByUserId(ratingDto.UserId);
            var trip = trips.Where(x => x.Id == ratingDto.TripId && x.Status == "Done");

            if (user != null)
            {
                if (trip != null)
                {
                    var rate = _mapper.Map<Rating>(ratingDto);
                    var result = await _uintOfWork.RatingRepository.AddRating(rate);
                    if (result)
                    {
                        return new Response<string>
                        {
                            IsSuccessful = true,
                            Message = "Response Successfull",
                            ResponseCode = HttpStatusCode.OK
                        };
                    }
                }
                return new Response<string>
                {
                    IsSuccessful = false,
                    Message = "Cannot rate this car",
                    ResponseCode = HttpStatusCode.BadRequest
                };

            }

            return new Response<string>
            {
                IsSuccessful = false,
                Message = "Response NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<Response<string>> AddComment(CommentDto commentDto)
        {
            var user = await _uintOfWork.UserRepository.GetUser(commentDto.UserId);
            var trips = await _uintOfWork.UserRepository.GetTripsByUserId(commentDto.UserId);
            var trip = trips.Where(x => x.Id == commentDto.TripId && x.Status == "Done");
            if (user != null)
            {
                if (trip != null)
                {
                    var comment = _mapper.Map<Comment>(commentDto);
                    var result = await _uintOfWork.CommentRepository.AddComment(comment);
                    if (result)
                    {
                        return new Response<string>
                        {
                            IsSuccessful = true,
                            Message = "Comment Added Successfully",
                            ResponseCode = HttpStatusCode.OK
                        };
                    }
                }
                return new Response<string>
                {
                    IsSuccessful = false,
                    Message = "Cannot comment on this car",
                    ResponseCode = HttpStatusCode.BadRequest
                };

            }
            return new Response<string>
            {
                IsSuccessful = false,
                Message = "Comment Not Successfull",
                ResponseCode = HttpStatusCode.BadRequest
            };

        }

        public async Task<Response<Trip>> BookTripAsync(TripBookingRequestDTO tripRequest)
        {
            var available = await _uintOfWork.CarRepository.GetACarTripAsync(tripRequest.CarId);
            if (available != null)
            {
                return new Response<Trip>
                {
                    Data = available,
                    IsSuccessful = false,
                    Message = "Car not available",
                    ResponseCode = HttpStatusCode.BadRequest
                };
            }
            var car = await _uintOfWork.CarRepository.GetCarDetailsAsync(tripRequest.CarId);
            var user = await _uintOfWork.UserRepository.GetUser(tripRequest.UserId);
            if(user != null && car != null)
            {
                var trip = new Trip()
                {
                    PickUpDate = DateTime.Now,
                    ReturnDate = DateTime.Now,
                    CarId = car.Id,
                    UserId = user.Id,
                    Status = "Pending"
                };
                await _uintOfWork.TripRepository.BookATrip(trip);
                return new Response<Trip>
                {
                    Data = trip,
                    IsSuccessful = true,
                    Message = "Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<Trip>
            {
                IsSuccessful = false,
                Message = "NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<Response<string>> DeleteCar(string carId, string dealerId)
        {
            var obj = await _uintOfWork.CarRepository.DeleteACar(carId, dealerId);
            if (obj)
            {
                return new Response<string>
                {
                    IsSuccessful = true,
                    Message = "Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<string>
            {
                IsSuccessful = false,
                Message = "NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }
    }
}
