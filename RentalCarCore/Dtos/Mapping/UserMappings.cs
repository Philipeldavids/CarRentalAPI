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

            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();

            CreateMap<Car,CarDTO>().ReverseMap();
            CreateMap<Car,CarDetailsDTO>()
            .ForMember(car => car.Ratings, opt => opt.MapFrom(src => src.Ratings.Count == 0 ? 0 : (double)src.Ratings.Sum(car => car.Ratings) / ((double)src.Ratings.Count)))
            .ForMember(car => car.NoOfUserRated, opt => opt.MapFrom(src => src.Ratings.Count)).ReverseMap();

            CreateMap<User, GetAllUserResponsetDto>().ReverseMap();
            CreateMap<Car, CarResponseDto>()
                .ForMember(car => car.ImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault().ImageUrl))
                .ForMember(car => car.Rating, opt => opt.MapFrom(src => src.Ratings.Count == 0 ? 0 : (double)src.Ratings.Sum(car => car.Ratings) / ((double)src.Ratings.Count)))
                .ForMember(car => car.Count, opt => opt.MapFrom(src => src.Ratings.Count)).ReverseMap();
        }
    }
}
