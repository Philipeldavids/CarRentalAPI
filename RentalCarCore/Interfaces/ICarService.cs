using RentalCarCore.Dtos.Request;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarCore.Interfaces
{
    public interface ICarService
    {
        Task<Response<List<CarDTO>>> GetListOfFeatureCarsAsync();
        Task<Response<PaginationModel<IEnumerable<CarResponseDto>>>> GetAllCarsAsync(int pageSize, int pageNumber);
    }
}