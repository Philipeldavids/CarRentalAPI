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

        public async Task<Response<IEnumerable<CarResponseDto>>> GetCarsBySearchAsync(string Location, DateTime pickupDate, DateTime returnDate)
        {
            var cars = await _uintOfWork.CarRepository.SearchCarByDateAndLocationAsync(Location, pickupDate, returnDate);
            
            if (cars != null)
            {
                var carResponse = _mapper.Map<IEnumerable<CarResponseDto>>(cars);
                return new Response<IEnumerable<CarResponseDto>>
                {
                    Data = carResponse,
                    IsSuccessful = true,
                    Message = "List of Cars Search",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<IEnumerable<CarResponseDto>>
            {
                IsSuccessful = false,
                Message = "Not Car Found",
                ResponseCode = HttpStatusCode.NotFound,
            };
        }
    }
}
