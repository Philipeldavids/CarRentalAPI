using System;
using System.Threading.Tasks;
using RentalCarInfrastructure.Models;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        Task<bool> AddRating(Rating rate);
       
    }
}
