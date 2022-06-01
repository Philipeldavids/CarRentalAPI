﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
            var cars = await query.OrderByDescending(x => x.Ratings.Sum(x => x.Ratings) / x.Ratings.Count).Take(6).ToListAsync();
            return cars;
        }

        public async Task<IEnumerable<Car>> GetCarDetailsAsync(string carId)
        {
            var carDetails = await _appDbContext.Cars
                             .Include(x => x.CarDetails)
                             .Include(x => x.Images)
                             .Include(x => x.Ratings)
                             .Include(x => x.Comments).Where(y => y.Id == carId).ToListAsync();
            return carDetails;
        }
        public async Task<Car> GetACarDetailAsync(string carId)
        {
            var carDetails = await _appDbContext.Cars
                             .Where(y => y.Id == carId).FirstOrDefaultAsync();
            return carDetails;
        }
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            var query = await _appDbContext.Cars
                .Where(x => x.Images.Count > 0)
                .Include(x => x.Images.Where(x => x.IsFeature == true))
                .Include(x => x.Ratings)
                .ToListAsync();
            return query;
        }

        public async Task<IEnumerable<Car>> GetCarByLocationAsync(Location state)
        {
            var carLocation = await _appDbContext.Cars
                              .Include(cd => cd.CarDetails)
                              .Include(tr => tr.Trips)
                              .Include(d => d.Dealers.Locations).ToListAsync();

            var result = carLocation;


            if (state != null)
            {
                result = (List<Car>)(IIncludableQueryable<Car, ICollection<Location>>)result.Where(x => x.Dealers.Locations.Contains(state));
            }
            return result;
        }

        /*public async Task<IEnumerable<Car>> GetCarByDateAsync(DateTime pickupDate, DateTime returnDate)
        {
            var carLocation = await _appDbContext.Cars
                              .Include(cd => cd.CarDetails)
                              .Include(tr => tr.Trips)
                              .Include(d => d.Dealers.Locations).ToListAsync();

            var result = carLocation;

            if (pickupDate < returnDate)
            {
                result = result.Where(r => r.Trips.FirstOrDefault(pickupDate);
            }
            if (carLocation.Where(d => d.Trips.Contains(pickupDate)){

            }


        }*/
    }
}

