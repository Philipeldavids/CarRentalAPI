using AutoMapper;
using RentalCarInfrastructure.Models;
using RentalCarCore.Dtos.Response;
using RentalCarCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarCore.Dtos.Mapping
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<RegistrationDto, User>().ReverseMap();
            CreateMap<Trip, TripsDTO>().ReverseMap();
            CreateMap<Car,CarDTO>().ReverseMap();
        }
    }
}
