using RentalCarCore.Dtos.Request;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Utilities.Pagination;
using RentalCarInfrastructure.Models;
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


        Task<Response<IEnumerable<CarSearchDto>>> GetCarsBySearchAsync(string Location, DateTime pickupDate, DateTime returnDate);

        Task<Response<PaginationModel<IEnumerable<CarOfferDto>>>> GetAllOfferCarsAsync(int pageSize, int pageNumber);

        Task<Response<string>> AddRating(RatingDto ratingDto);
        Task<Response<string>> AddComment(CommentDto commentDto);
        Task<Response<Trip>> BookTripAsync(TripBookingRequestDTO tripRequest);
        Task<Response<string>> DeleteCar(string carId, string dealerId);
        Task<Response<string>> DealerAddCar(CarRequestDTO request);
        Task<Response<string>> EditCar(string carId, CarUpdateDto carUpdateDto);


    }
}