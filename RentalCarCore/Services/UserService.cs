using AutoMapper;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RentalCarInfrastructure.Interfaces;
using RentalCarInfrastructure.Models;
using RentalCarCore.Dtos.Request;

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
                if (trips != null)
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

        public async Task<Response<string>> UpdateUserDetails(string Id, UpdateUserDto updateUserDto)
        {
            var user = _unitOfWork.UserRepository.GetUser(Id);

            if (user != null)
            {

                var result = await _unitOfWork.UserRepository.UpdateUser(new User()
                {
                    FirstName = string.IsNullOrWhiteSpace(updateUserDto.FirstName) ? updateUserDto.FirstName : updateUserDto.FirstName,
                    LastName = string.IsNullOrWhiteSpace(updateUserDto.LastName) ? updateUserDto.LastName : updateUserDto.LastName,
                    Address = string.IsNullOrWhiteSpace(updateUserDto.PhoneNumber) ? updateUserDto.PhoneNumber : updateUserDto.PhoneNumber,
                    PhoneNumber = string.IsNullOrWhiteSpace(updateUserDto.Address) ? updateUserDto.Address : updateUserDto.Address,

                });

                if (result)
                {
                    return new Response<string>()
                    {
                        IsSuccessful = true,
                        Message = "Profile updated"
                    };
                }
                return new Response<string>()
                {
                    IsSuccessful = false,
                    Message = "Profile not updated"
                };
            }

            throw new ArgumentException("User not found");
        }

        public async Task<Response<UserDetailResponseDTO>> GetUser(string userId)
        {
            User user = await _unitOfWork.UserRepository.GetUser(userId);
            if (user != null)
            {
                var result = _mapper.Map<UserDetailResponseDTO>(user);
                return new Response<UserDetailResponseDTO>()
                {
                    Data = result,
                    IsSuccessful = true,
                    Message = "Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }

            throw new ArgumentException("Resourse not found");
        }
    }
}
