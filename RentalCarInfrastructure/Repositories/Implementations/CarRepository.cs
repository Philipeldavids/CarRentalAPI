using Microsoft.EntityFrameworkCore;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

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
            var query =  await _appDbContext.Cars
                  .Include(x => x.CarDetails)
                  .Include(x => x.Images.Where(x => x.IsFeature == true))
                  .Include(r => r.Ratings)
                  .OrderByDescending(x => x.Ratings.Sum(x => x.Ratings) / x.Ratings.Count + x.Ratings.Count).Take(6)
                  .ToListAsync();
                 
            return query;
        }

        public async Task<Car> GetCarDetailsAsync(string carId)
        {
            var carDetails = await _appDbContext.Cars
                             .Include(x => x.CarDetails)
                             .Include(x => x.Images)
                             .Include(x => x.Ratings)
                             .Include(x => x.Comments).Where(y => y.Id == carId).FirstOrDefaultAsync();
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

        

        public async Task<Dictionary<Car, bool>> SearchCarByDateAndLocationAsync(string Location, DateTime pickupDate, DateTime returnDate)
        {
            var dealers = await _appDbContext.Dealers
                              .Include(x => x.Locations.Where(x => x.State == Location))
                              .Include(x => x.Cars)
                                .ThenInclude(x => x.Trips)
                              .Include(x => x.Cars)
                                .ThenInclude(x => x.Ratings)
                              .Include(x => x.Cars)
                                .ThenInclude(x => x.Images)
                                .ToListAsync();



            if(dealers != null)
            {
                var locations = dealers.FindAll(x => x.Locations.Count > 0);
                
                List<Car> cars = new List<Car>();

                Dictionary<Car, bool> result = new Dictionary<Car, bool> ();

                foreach (var item in locations)
                {
                    cars.AddRange(item.Cars);
                }

                foreach (var item in cars)
                {
                    var trip = item.Trips.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
                    if (trip.PickUpDate <= pickupDate && trip.ReturnDate >= returnDate)
                    {
                        result[item] = false;
                    }
                    else
                    {
                        result[item] = true;
                    }
                }

                return result;

            }


            return null;

        }


        public async Task<IEnumerable<Car>> GetAllOfferCarsAsync()
        {
            var query = _appDbContext.Cars
                .Where(x => x.Images.Count > 0)
                .Include(x => x.Images.Where(x => x.IsFeature == true))
                .Include(x => x.Offers.Where(x => x.IsActive == true))
                .Include(x => x.CarDetails)
                .Include(x => x.Ratings);
            var cars = await query.OrderByDescending(x => x.Ratings.Sum(x => x.Ratings) / x.Ratings.Count).ToListAsync();
            return cars;
        }
    }
}

