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
        Response<string> VerifyPayment(string reference);
        Task<Response<PaymentResponseDTO>> UserPayment(PaymentRequestDTO pay);
        Task<Response<DealerResponseDTO>> AddDealer(DealerRequestDTO dealer);
        Task<Response<UserDetailResponseDTO>> GetUser(string userId);

        Task<Response<PaginationModel<IEnumerable<GetAllUserResponsetDto>>>> GetUsersAsync(int pageSize, int pageNumber);

        Task<Response<PaginationModel<IEnumerable<GetAllDealerResponseDto>>>> GetAllDealersAsync(int pageSize, int pageNumber);
        Task<Response<PaginationModel<IEnumerable<AllTripsDto>>>> GetAllTripsAsync(int pageSize, int pageNumber);
        Task<Response<User>> DeleteUser(string userId);
        Task<Response<List<TransactionResponseDto>>> GetAllTransactionByUser(string userId);
    }
}