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
        Task<IEnumerable<Car>> GetAllCarsAsync();

        Task<IEnumerable<Car>> SearchCarByDateAndLocationAsync(string Location, DateTime pickupDate, DateTime returnDate);
        Task<IEnumerable<Car>> GetAllOfferCarsAsync();

    }
}