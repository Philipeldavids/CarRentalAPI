using RentalCarCore.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarCore.Interfaces
{
    public interface ICarService
    {
        Task<Response<List<CarDTO>>> GetListOfFeatureCarsAsync();
    }
}