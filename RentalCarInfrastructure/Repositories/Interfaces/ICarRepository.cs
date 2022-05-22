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
<<<<<<< HEAD
        Task<IEnumerable<Car>> SearchCarByDateAndLocationAsync(string Location, DateTime pickupDate, DateTime returnDate);
=======

        Task<IEnumerable<Car>> GetCarByLocationAsync(Location state);
        Task<IEnumerable<Car>> GetCarByDateAsync(DateTime pickupDate, DateTime returnDate);
        Task<IEnumerable<Car>> GetAllOfferCarsAsync();
>>>>>>> 616538c51f00a9208d3b3b3e5ec3b3a2523c5397
    }
}