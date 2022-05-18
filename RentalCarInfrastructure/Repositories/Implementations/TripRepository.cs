using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;

namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        private readonly AppDbContext _appDbContext;
        public TripRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

    }
}
