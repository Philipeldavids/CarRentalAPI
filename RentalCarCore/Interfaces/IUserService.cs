using RentalCarCore.Dtos.Response;
using RentalCarCore.Dtos.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarCore.Interfaces
{
    public interface IUserService
    {
        Task<Response<List<TripsDTO>>> GetTrips(string UserId);

        Task<Response<string>> UpdateUserDetails(string Id, UpdateUserDto updateUserDto);

        Task<Response<string>> AddRating(RatingDto ratingDto);
        Task<Response<string>> AddComment(CommentDto commentDto);

        Task<Response<UserDetailResponseDTO>> GetUser(string userId);

    }
}