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

        public async Task<Response<List<CarDTO>>> GetListOfFeatureCarsAsync()
        {
            var car = await _uintOfWork.CarRepository.GetAllFeatureCarsAsync();
            if (car != null)
            {
                var result = _mapper.Map<List<CarDTO>>(car);
                return new Response<List<CarDTO>>()
                {
                    Data = result,
                    IsSuccessful = true,
                    Message = "Response Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }

            return new Response<List<CarDTO>>()
            {
                Data = null,
                IsSuccessful = false,
                Message = "Response NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<Response<PaginationModel<IEnumerable<CarRequestDto>>>> GetAllCarsAsync(int pageSize, int pageNumber)
        {
            var cars = await _uintOfWork.CarRepository.GetAllCarsAsync();
            var carResponse = _mapper.Map<IEnumerable<CarRequestDto>>(cars);
            if(cars != null)
            {
                var carResult = PaginationClass.PaginationAsync(carResponse, pageSize, pageNumber);
                return new Response<PaginationModel<IEnumerable<CarRequestDto>>>
                {
                    Data = carResult,
                    IsSuccessful = true,
                    Message = "List of Cars",
                    ResponseCode = HttpStatusCode.OK
                };
            }
            return new Response<PaginationModel<IEnumerable<CarRequestDto>>>
            {
                IsSuccessful = false,
                Message = "List of cars Not Found",
                ResponseCode = HttpStatusCode.NoContent,
            };
        }
    }
}
