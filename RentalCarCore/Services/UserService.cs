using AutoMapper;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RentalCarInfrastructure.Interfaces;
using RentalCarInfrastructure.Models;

namespace RentalCarCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<TripsDTO>>> GetTrips(string UserId)
        {
            var user = await _unitOfWork.UserRepository.GetUser(UserId);

            if (user != null)
            {
                var trips = await _unitOfWork.UserRepository.GetTripsByUserId(UserId);
                if(trips != null)
                {
                    var result = _mapper.Map<List<TripsDTO>>(trips);
                    return new Response<List<TripsDTO>>()
                    {
                        Data = result,
                        IsSuccessful = true,
                        Message = "Response Successful",
                        ResponseCode = HttpStatusCode.OK
                    };
                }
                return new Response<List<TripsDTO>>()
                {
                    Data = null,
                    IsSuccessful = false,
                    Message = "Response NotSuccessful",
                    ResponseCode = HttpStatusCode.BadRequest
                };
            }

            return new Response<List<TripsDTO>>()
            {
                Data = null,
                IsSuccessful = false,
                Message = "Response NotSuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }
    }
}
