using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System;
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
                             .Include(x => x.Trips)
                             .Include(x => x.Comments).Where(y => y.Id == carId).FirstOrDefaultAsync();
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

        public async Task<Trip> GetACarTripAsync(string carId)
        {
            var query = await _appDbContext.Trips
                 .Where(x => x.CarId == carId && x.Status == "Pending").FirstOrDefaultAsync();
            return query;
        }


        public async Task<IEnumerable<Car>> GetAllOfferCarsAsync()
        {
            var query = await _appDbContext.Cars
                .Where(x => x.Images.Count > 0)
                .Include(x => x.Images.Where(x => x.IsFeature == true))
                .Include(x => x.Offers.Where(x => x.IsActive == true))
                .Include(x => x.CarDetails)
                .Include(x => x.Ratings)
                .ToListAsync();
            //var cars = await query.OrderByDescending(x => x.Ratings.Sum(x => x.Ratings) / x.Ratings.Count).ToListAsync();
            return query;
        }

        public async Task<bool> DeleteACar(string carId, string dealerId)
        {
            var car = await _appDbContext.Cars.Where(x=>x.DealerId==dealerId && x.Id==carId).FirstOrDefaultAsync();
            var delete = await Delete(car);
            return delete;
        }

        public async Task<bool> AddNewCar(Car car)
        {
            var cars = await Add(car);
            return cars;
        }

        public async Task<bool> EditCar(Car car)
        {


            var res = await Update(car);
            return res;

        }

        public void EditCarByDealer(Car car)
        {
            _appDbContext.Update(car);
            _appDbContext.SaveChanges();
        }

        public async Task<Car> GetCarById(string carId)
        {
            var carQuery = await _appDbContext.Cars
                             .Include(cd => cd.CarDetails).Where(y => y.Id == carId).FirstOrDefaultAsync();
            return carQuery;
        }

        public async Task<List<Trip>> GetCarTripsByUserIdAsync(string userId)
        {
            var trips = await _appDbContext.Trips
                .Where(x => x.UserId == userId)
                .ToListAsync();
            
            return trips;
        }
    }
}

