using RentalCarInfrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip> GetCarTrip(string tripId);
        Task<bool> BookATrip(Trip trip);
        Task<List<Trip>> GetAllTransactionByUserAsyc(string userId);
        Task<bool> UpdateATrip(Trip trip);
    }
}