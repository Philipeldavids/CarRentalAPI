using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

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

    }
}
