using RentalCarCore.Dtos;
using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Interfaces
{
    public interface ITokenGen
    {
        Task<string> GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
