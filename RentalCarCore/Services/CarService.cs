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
            var trips = await _uintOfWork.UserRepository.GetTripsByUserIdAsync(ratingDto.UserId);
            var trip = trips.Where(x => x.Id == ratingDto.TripId && x.Status == "Done" && x.IsRated == false).FirstOrDefault();

            if (user != null)
            {
                if (trip != null)
                {
                    var rate = _mapper.Map<Rating>(ratingDto);
                    var result = await _uintOfWork.RatingRepository.AddRating(rate);
                    if (result)
                    {
                        trip.IsRated = true;
                        await _uintOfWork.TripRepository.UpdateATrip(trip);
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
            var trips = await _uintOfWork.UserRepository.GetTripsByUserIdAsync(commentDto.UserId);
            var trip = trips.Where(x => x.Id == commentDto.TripId && x.Status == "Done" && x.IsCommented == false).FirstOrDefault();
            if (user != null)
            {
                if (trip != null)
                {
                    var comment = _mapper.Map<Comment>(commentDto);
                    var result = await _uintOfWork.CommentRepository.AddComment(comment);
                    if (result)
                    {
                        trip.IsCommented = true;
                        await _uintOfWork.TripRepository.UpdateATrip(trip);
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
            var car = await _uintOfWork.CarRepository.GetCarDetailsAsync(tripRequest.CarId);
            var user = await _uintOfWork.UserRepository.GetUser(tripRequest.UserId);
            if (user == null || car == null)
            {
                return new Response<Trip>
                {
                    Data = null,
                    IsSuccessful = false,
                    Message = "No Record Found",
                    ResponseCode = HttpStatusCode.BadRequest
                };
            }

            var lastTrip = car.Trips.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if(lastTrip.ReturnDate < tripRequest.PickupDate)
            {
                var trip = new Trip()
                {
                    PickUpDate = tripRequest.PickupDate,
                    ReturnDate = tripRequest.ReturnDate,
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
                Data = lastTrip,
                IsSuccessful = false,
                Message = "Car is not avaliable",
                ResponseCode = HttpStatusCode.Forbidden
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

        public async Task<Response<string>> DealerAddCar(CarRequestDTO request)
        {
            var dealer = await _uintOfWork.DealerRepository.GetDealer(request.DealerId);
            if (dealer != null)
            {
                var car = _mapper.Map<Car>(request);
                await _uintOfWork.CarRepository.AddNewCar(car);
                return new Response<string>
                {
                    IsSuccessful = true,
                    Message = "Dealer Found",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<string>
            {
                IsSuccessful = false,
                Message = "Dealer Not Found",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<Response<string>> EditCar(string carId, CarUpdateDto carUpdateDto)
        {
            var car = await _uintOfWork.CarRepository.GetCarById(carId);

            if (car != null)
            {
                car.Model = carUpdateDto.Model;
                car.YearOfMan = carUpdateDto.YearOfMan;
                car.PlateNumber = carUpdateDto.PlateNumber;
                car.ChasisNumber = carUpdateDto.ChasisNumber;
                car.Color = carUpdateDto.Color;
                car.TypeOfCar = carUpdateDto.TypeOfCar;
                car.CarDetails.TypeOfSeat = carUpdateDto.TypeOfSeat;
                car.CarDetails.AirCondition = carUpdateDto.AirCondition;
                car.CarDetails.Sunroof = carUpdateDto.Sunroof;
                car.CarDetails.Bluetooth = carUpdateDto.Bluetooth;
                car.CarDetails.NavigationSystem = carUpdateDto.NavigationSystem;
                car.CarDetails.RemoteStart = carUpdateDto.RemoteStart;
                car.CarDetails.BackUpcamera = carUpdateDto.BackUpcamera;
                car.CarDetails.ThirdRowSeating = carUpdateDto.ThirdRowSeating;
                car.CarDetails.CarPlay = carUpdateDto.CarPlay;

                _uintOfWork.CarRepository.EditCarByDealer(car);

                return new Response<string>()
                {
                    IsSuccessful = true,
                    ResponseCode = HttpStatusCode.OK,
                };
            }
            return new Response<string>()
            {
                IsSuccessful = false,
                Message = "Car not found",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }
    }
}
