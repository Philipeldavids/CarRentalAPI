using AutoMapper;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Interfaces;
using RentalCarInfrastructure.Interfaces;
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

        public async Task<Response<List<CarDetailsDTO>>> GetCarDetailsAsync(string carId)
        {
            var car = await _uintOfWork.CarRepository.GetCarDetailsAsync(carId);
            if (car != null)
            {
                var result = _mapper.Map<List<CarDetailsDTO>>(car);
                return new Response<List<CarDetailsDTO>>()
                {
                    Data = result,
                    IsSuccessful = true,
                    Message = "Response Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }

            return new Response<List<CarDetailsDTO>>()
            {
                Data = null,
                IsSuccessful = false,
                Message = "Response NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }
    }
}
