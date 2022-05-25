﻿using AutoMapper;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RentalCarInfrastructure.Interfaces;
using RentalCarInfrastructure.Models;
using RentalCarCore.Dtos.Request;
using RentalCarInfrastructure.Repositories.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using RentalCarCore.Utilities.Pagination;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using Microsoft.Extensions.Configuration;
using RentalCarCore.Dtos.Mapping;

namespace RentalCarCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Rating> _ratingRepository;
        private readonly IConfiguration _configuration;
        private PayStackApi payStackApi;

        public UserService(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Rating> ratingRepository, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ratingRepository = ratingRepository;
            _configuration = configuration;
            payStackApi = new PayStackApi(_configuration["Payment:PaystackSK"]);
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
            var user = await _unitOfWork.UserRepository.GetUser(Id);

            if (user != null)
            {

                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.PhoneNumber = updateUserDto.PhoneNumber;
                user.Address = updateUserDto.Address;
                var result = await _unitOfWork.UserRepository.UpdateUser(user);

                if (result)
                {
                    return new Response<string>()
                    {
                        IsSuccessful = true,
                        Message = "Profile updated",
                        ResponseCode = HttpStatusCode.OK

                    };
                }
                return new Response<string>()
                {
                    IsSuccessful = false,
                    Message = "Profile not updated",
                    ResponseCode = HttpStatusCode.BadRequest
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

        public async Task<Response<PaginationModel<IEnumerable<GetAllUserResponsetDto>>>> GetUsersAsync(int pageSize, int pageNumber)
        {
            var users = await _userManager.Users.ToListAsync();
            var response = _mapper.Map<IEnumerable<GetAllUserResponsetDto>>(users);

            if (users != null)
            {
                var result = PaginationClass.PaginationAsync(response, pageSize, pageNumber);
                return new Response<PaginationModel<IEnumerable<GetAllUserResponsetDto>>>()
                {
                    Data = result,
                    Message = "List of All Users",
                    ResponseCode = HttpStatusCode.OK,
                    IsSuccessful = true
                };
            }

            return new Response<PaginationModel<IEnumerable<GetAllUserResponsetDto>>>()
            {
                Data = null,
                Message = "No Registered Users",
                ResponseCode = HttpStatusCode.NoContent,
                IsSuccessful = false
            };
        }
        public async Task<Response<PaymentResponseDTO>> UserPayment(PaymentRequestDTO pay)
        {
            var trip = await _unitOfWork.TripRepository.GetCarTrip(pay.TripId);
            if (trip != null)
            {
                TransactionInitializeRequest request = new()
                {
                    AmountInKobo = pay.Amount * 100,
                    Email = pay.Email,
                    Reference = GenerateReference(),
                    Currency = "NGN",
                    CallbackUrl = ""
                };
                TransactionInitializeResponse transResponse = payStackApi.Transactions.Initialize(request);
                if (transResponse.Status)
                {
                    var trans = TransactionMapping.GetTransaction(pay, request.Reference, trip.Id);
                    await _unitOfWork.TransactionRepository.AddTransaction(trans);
                    var result = TransactionMapping.GetPayment(trans, transResponse.Data.AuthorizationUrl);
                    return new Response<PaymentResponseDTO>
                    {
                        Data = result,
                        IsSuccessful = true,
                        Message = transResponse.Message,
                        ResponseCode = HttpStatusCode.OK
                    };
                }
                return new Response<PaymentResponseDTO>
                {
                    Data = null,
                    IsSuccessful = false,
                    Message = transResponse.Message,
                    ResponseCode = HttpStatusCode.BadRequest
                };
            }

            return new Response<PaymentResponseDTO>
            {
                Data = null,
                IsSuccessful = false,
                Message = "Trip Not Found",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        public async Task<Response<string>> VerifyPayment(string reference)
        {
            TransactionVerifyResponse response = payStackApi.Transactions.Verify(reference);
            if (response.Data.Status == "success")
            {
                var trans = _unitOfWork.TransactionRepository.GetTransactionReference(reference);
                if (trans != null)
                {
                    trans.Status = "success";
                    await _unitOfWork.TransactionRepository.UpdateTransaction(trans);
                    return new Response<string>
                    {
                        IsSuccessful = true,
                        Message = response.Data.Status,
                        ResponseCode = HttpStatusCode.OK
                    };
                }
                return new Response<string>
                {
                    IsSuccessful = false,
                    Message = "Transaction not Found",
                    ResponseCode = HttpStatusCode.BadRequest
                };
            }

            return new Response<string>
            {
                IsSuccessful = false,
                Message = "unsuccessful",
                ResponseCode = HttpStatusCode.BadRequest
            };
        }

        private static string GenerateReference()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            return random.Next(100000000, 999999999).ToString();
        }
    }
}

