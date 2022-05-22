using RentalCarCore.Dtos.Request;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarCore.Interfaces
{
    public interface ICarService
    {
        Task<Response<List<CarFeatureDTO>>> GetListOfFeatureCarsAsync();
        Task<Response<CarDetailsDTO>> GetCarDetailsAsync(string carId);
        Task<Response<PaginationModel<IEnumerable<CarResponseDto>>>> GetAllCarsAsync(int pageSize, int pageNumber);


        Task<Response<IEnumerable<CarResponseDto>>> GetCarsBySearchAsync(string Location, DateTime pickupDate, DateTime returnDate);

        Task<Response<PaginationModel<IEnumerable<CarOfferDto>>>> GetAllOfferCarsAsync(int pageSize, int pageNumber);

    }
}