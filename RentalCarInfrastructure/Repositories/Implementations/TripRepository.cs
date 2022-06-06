using Microsoft.EntityFrameworkCore;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        private readonly AppDbContext _appDbContext;
        public TripRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> BookATrip(Trip trip)
        {
            var result = await Add(trip);
            return result;
        }

        public async Task<Trip> GetCarTrip(string tripId)
        {
            var result = await _appDbContext.Trips.FindAsync(tripId);
            return result;
        }

        public async Task<List<Trip>> GetAllTransactionByUserAsyc(string userId)
        {
            var result = await _appDbContext.Trips
                .Where(x => x.UserId == userId)
                .Include(y => y.Transactions).ToListAsync();
            return result;

        }

        public async Task<bool> UpdateATrip(Trip trip)
        {
            var result = await Update(trip);
            return result;
        }
    }
}
