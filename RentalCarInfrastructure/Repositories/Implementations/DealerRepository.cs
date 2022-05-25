﻿using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext _appDbContext;
        public DealerRepository(AppDbContext AppDbContext) : base(AppDbContext)
        {
            _appDbContext = AppDbContext;
        }

        public async Task<List<Dealer>> GetDealersAsync()
        {
            var dealers = await _appDbContext.Dealers.Include(l => l.Locations).ToListAsync();
            return (List<Dealer>)dealers;
        }
    }
}