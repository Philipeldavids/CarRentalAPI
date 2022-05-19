using RentalCarCore.Dtos.Response;
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
        Task<Response<PaginationModel<IEnumerable<GetAllUsersRequestDto>>>> GetUsersAsync(int pageSize, int pageNumber);
    }
}