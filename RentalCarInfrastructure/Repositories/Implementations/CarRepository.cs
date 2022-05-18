using Microsoft.EntityFrameworkCore;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        private readonly AppDbContext _appDbContext;
        public CarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Car>> GetAllFeatureCarsAsync()
        {
            var query = _appDbContext.Cars
                  .Include(x => x.CarDetails)
                  .Include(i => i.Images)
                  .Include(r => r.Ratings);
            var cars = await query.OrderByDescending(x => x.Ratings.Sum(x => x.Ratings) / x.Ratings.Count).ToListAsync();
            var nums = query.OrderBy(x => x.Ratings.Sum(x => x.Ratings) / x.Ratings.Count).Select(x => x.Ratings.Sum(x=>x.Ratings) / x.Ratings.Count).ToList();
            return cars;
        }
    }
}

