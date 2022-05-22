using RentalCarCore.Dtos.Request;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Utilities.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarCore.Interfaces
{
    public interface ICarService
    {
        Task<Response<List<CarFeatureDTO>>> GetListOfFeatureCarsAsync();
        Task<Response<CarDetailsDTO>> GetCarDetailsAsync(string carId);
        Task<Response<PaginationModel<IEnumerable<CarResponseDto>>>> GetAllCarsAsync(int pageSize, int pageNumber);
    }
}