using RentalCarCore.Dtos.Response;
using RentalCarCore.Dtos.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCarCore.Utilities.Pagination;
using RentalCarInfrastructure.Models;

namespace RentalCarCore.Interfaces
{
    public interface IUserService
    {
        Task<Response<List<TripsDTO>>> GetTrips(string UserId);

        Task<Response<string>> UpdateUserDetails(string Id, UpdateUserDto updateUserDto);

        Task<Response<string>> AddRating(RatingDto ratingDto);
        Task<Response<string>> AddComment(CommentDto commentDto);

        Task<Response<UserDetailResponseDTO>> GetUser(string userId);

        Task<Response<PaginationModel<IEnumerable<GetAllUserResponsetDto>>>> GetUsersAsync(int pageSize, int pageNumber);
        Task<Response<User>> DeleteUser(string userId);
        Task<Response<List<TransactionResponseDto>>> GetAllTransactionByUser(string userId);
    }
}