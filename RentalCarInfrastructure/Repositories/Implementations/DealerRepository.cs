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
    public class DealerRepository : GenericRepository<DealerRepository>, IDealerRepository
    {
        public DealerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Dealer>> GetDealersAsync(string location)
        {
            var dealers = await GetAllRecord();
            return (List<Dealer>)dealers;
        }
    }
}
