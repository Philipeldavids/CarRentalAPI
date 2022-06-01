using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.ModelValidationHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Request
{
    public class CarUpdateDto
    {
       /* public string UserId { get; set; }
        public string CarId { get; set; }*/

       // [StringLength(50, MinimumLength = 2, ErrorMessage = DataAnnotationsHelper.ModelValidator)]
        public string Model { get; set; }


        //[StringLength(50, MinimumLength = 4, ErrorMessage = DataAnnotationsHelper.YearOfManValidator)]
        public string YearOfMan { get; set; }


       // [StringLength(150, MinimumLength = 5, ErrorMessage = DataAnnotationsHelper.PlateNumberValidator)]
        public string PlateNumber { get; set; }


       // [StringLength(150, MinimumLength = 5, ErrorMessage = DataAnnotationsHelper.ChasisNumberValidator)]
        public string ChasisNumber { get; set; }

        
        //[StringLength(50, MinimumLength = 3, ErrorMessage = DataAnnotationsHelper.ColorValidator)]
        public string Color { get; set; }

        //[StringLength(50, MinimumLength = 3, ErrorMessage = DataAnnotationsHelper.TypeOfCarValidator)]
        public string TypeOfCar { get; set; }

        public string TypeOfSeat { get; set; }

        public bool Sunroof { get; set; }
        public bool Bluetooth { get; set; }
        public bool NavigationSystem { get; set; }
        public bool AirCondition { get; set; }
        public bool RemoteStart { get; set; }
        public bool BackUpcamera { get; set; }
        public bool ThirdRowSeating { get; set; }
        public bool CarPlay { get; set; }


    }
}
