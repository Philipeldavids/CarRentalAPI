using System;
using System.Threading.Tasks;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;

namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class RatingRepository :GenericRepository<Rating>, IRatingRepository
    {
        private readonly AppDbContext _appDbContext;
        public RatingRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AddRating(Rating rate)
        {
            var result = await Add(rate);
            return result;
        }

       

    }
}
