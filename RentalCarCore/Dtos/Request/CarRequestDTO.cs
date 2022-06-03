using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class CarRequestDTO
    {
        public string DealerId { get; set; }
        public string Model { get; set; }
        public string YearOfMan { get; set; }
        public string PlateNumber { get; set; }
        public string ChasisNumber { get; set; }
        public string Color { get; set; }
        public string TypeOfCar { get; set; }
        public int Mileage { get; set; }
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
    }
}
