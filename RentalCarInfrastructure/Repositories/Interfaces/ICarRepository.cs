using RentalCarInfrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllFeatureCarsAsync();
        Task<IEnumerable<Car>> GetCarDetailsAsync(string carId);
        Task<IEnumerable<Car>> GetAllCarsAsync();

        Task<IEnumerable<Car>> GetCarByLocationAsync(Location state);
    }
}