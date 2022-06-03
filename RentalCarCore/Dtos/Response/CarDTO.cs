using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Response
{
    public class CarFeatureDTO
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string YearOfMan { get; set; }
        public string TypeOfCar { get; set; }
        public double Price { get; set; }
        public string UnitOfPrice { get; set; }

        public string TypeOfSeat { get; set; }

        public bool Sunroof { get; set; }
        public bool Bluetooth { get; set; }
        public bool NavigationSystem { get; set; }
        public bool AirCondition { get; set; }
        public bool RemoteStart { get; set; }
        public bool BackUpcamera { get; set; }
        public bool ThirdRowSeating { get; set; }
        public bool Driver { get; set; }
        public bool CarPlay { get; set; }
        public bool IsTrack { get; set; }


        public string ImageUrl { get; set; }
    }
}
