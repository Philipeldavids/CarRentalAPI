using RentalCarInfrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllFeatureCarsAsync();
        Task<Car> GetCarDetailsAsync(string carId);
        Task<Car> GetACarDetailAsync(string carId);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Dictionary<Car, bool>> SearchCarByDateAndLocationAsync(string Location, DateTime pickupDate, DateTime returnDate);
        Task<IEnumerable<Car>> GetAllOfferCarsAsync();
        Task<Trip> GetACarTripAsync(string carId);

        Task<bool> DeleteACar(string carId, string dealerId);
        Task<bool> AddNewCar(Car car);

        Task<bool> EditCar(Car car);
        Task<Car> GetCarById(string carId);
        void EditCarByDealer(Car car);

        Task<List<Trip>> GetCarTripsByUserIdAsync(string userId);


    }
}