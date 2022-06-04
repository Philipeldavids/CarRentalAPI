using Microsoft.EntityFrameworkCore;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> GetUser(string userId)
        {
            var users = GetARecord(userId);
            return await users;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var result = await Update(user);
            return result;
        }

        public async Task<List<Trip>> GetTripsByUserIdAsync(string userId)
        {
            var trips = await _appDbContext.Trips.Where(x => x.UserId == userId)
                .Include(y => y.Transactions).ToListAsync();
            return trips;
        }

        public async Task<List<Trip>> GetTripsAsync()
        {
            var trips = await _appDbContext.Trips.OrderByDescending(x => x.PickUpDate).ToListAsync();
            return (List<Trip>)trips;
        }

    }
}
