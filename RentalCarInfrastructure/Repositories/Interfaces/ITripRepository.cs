﻿using RentalCarInfrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip> GetCarTrip(string tripId);
    }
}