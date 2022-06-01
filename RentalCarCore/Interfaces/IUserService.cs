﻿using RentalCarCore.Dtos.Response;
using RentalCarCore.Dtos.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCarCore.Utilities.Pagination;

namespace RentalCarCore.Interfaces
{
    public interface IUserService
    {
        Task<Response<List<TripsDTO>>> GetTrips(string UserId);

        Task<Response<string>> UpdateUserDetails(string Id, UpdateUserDto updateUserDto);

        

        Task<Response<UserDetailResponseDTO>> GetUser(string userId);

        Task<Response<PaginationModel<IEnumerable<GetAllUserResponsetDto>>>> GetUsersAsync(int pageSize, int pageNumber);

        Task<Response<PaginationModel<IEnumerable<GetAllDealerResponseDto>>>> GetAllDealersAsync(int pageSize, int pageNumber);
        Task<Response<PaginationModel<IEnumerable<AllTripsDto>>>> GetAllTripsAsync(int pageSize, int pageNumber);
    }
}