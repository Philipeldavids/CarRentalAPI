﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Models
{
    public class CarDetail : BaseEntity
    {
        [Required]
        public Guid CarId { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Part TypeOfSeat must be between 3 and 50 characters in length")]
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
    }
}