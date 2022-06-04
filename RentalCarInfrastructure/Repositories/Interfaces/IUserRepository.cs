using RentalCarInfrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userId);
        Task<bool> UpdateUser(User user);
        Task<List<Trip>> GetTripsByUserIdAsync(string userId);
        Task<List<Trip>> GetTripsAsync();
    }
}
