using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface IDealerRepository
    {
        Task<List<Dealer>> GetDealersAsync();
        Task<bool> AddNewDealer(Dealer dealer);
        Task<Dealer> DeleteACar(string userId);
        Task<Dealer> GetDealer(string id);
    }
}
