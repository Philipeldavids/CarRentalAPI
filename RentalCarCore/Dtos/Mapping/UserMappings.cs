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
            // Users
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<RegistrationDto, User>().ReverseMap();
            
            CreateMap<UserDetailResponseDTO, User>().ReverseMap();
            CreateMap<Dealer, DealerResponseDTO>().ReverseMap();
            CreateMap<Dealer,DealerRequestDTO>().ReverseMap();
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();


            // Feature Cars 
            CreateMap<Car, CarFeatureDTO>()
                .ForMember(car => car.ImageUrl, opt => opt.MapFrom(src => src.Images.Select(x => x.ImageUrl).FirstOrDefault()))
                .ForMember(car => car.TypeOfSeat, opt => opt.MapFrom(src => src.CarDetails.TypeOfSeat))
                .ForMember(car => car.Sunroof, opt => opt.MapFrom(src => src.CarDetails.Sunroof))
                .ForMember(car => car.Bluetooth, opt => opt.MapFrom(src => src.CarDetails.Bluetooth))
                .ForMember(car => car.AirCondition, opt => opt.MapFrom(src => src.CarDetails.AirCondition))
                .ForMember(car => car.BackUpcamera, opt => opt.MapFrom(src => src.CarDetails.BackUpcamera))
                .ForMember(car => car.CarPlay, opt => opt.MapFrom(src => src.CarDetails.CarPlay))
                .ForMember(car => car.Driver, opt => opt.MapFrom(src => src.CarDetails.Driver))
                .ForMember(car => car.IsTrack, opt => opt.MapFrom(src => src.CarDetails.IsTrack))
                .ForMember(car => car.NavigationSystem, opt => opt.MapFrom(src => src.CarDetails.NavigationSystem))
                .ForMember(car => car.RemoteStart, opt => opt.MapFrom(src => src.CarDetails.RemoteStart))
                .ForMember(car => car.ThirdRowSeating, opt => opt.MapFrom(src => src.CarDetails.ThirdRowSeating))
                .ReverseMap();

            // Car Details
            CreateMap<Car,CarDetailsDTO>()
                .ForMember(car => car.Ratings, opt => opt.MapFrom(src => src.Ratings.Count == 0 ? 0 : (double)src.Ratings.Sum(car => car.Ratings) / ((double)src.Ratings.Count)))
                .ForMember(car => car.NoOfUserRated, opt => opt.MapFrom(src => src.Ratings.Count))
                .ForMember(car => car.TypeOfSeat, opt => opt.MapFrom(src => src.CarDetails.TypeOfSeat))
                .ForMember(car => car.Sunroof, opt => opt.MapFrom(src => src.CarDetails.Sunroof))
                .ForMember(car => car.Bluetooth, opt => opt.MapFrom(src => src.CarDetails.Bluetooth))
                .ForMember(car => car.AirCondition, opt => opt.MapFrom(src => src.CarDetails.AirCondition))
                .ForMember(car => car.BackUpcamera, opt => opt.MapFrom(src => src.CarDetails.BackUpcamera))
                .ForMember(car => car.CarPlay, opt => opt.MapFrom(src => src.CarDetails.CarPlay))
                .ForMember(car => car.Driver, opt => opt.MapFrom(src => src.CarDetails.Driver))
                .ForMember(car => car.IsTrack, opt => opt.MapFrom(src => src.CarDetails.IsTrack))
                .ForMember(car => car.NavigationSystem, opt => opt.MapFrom(src => src.CarDetails.NavigationSystem))
                .ForMember(car => car.RemoteStart, opt => opt.MapFrom(src => src.CarDetails.RemoteStart))
                .ForMember(car => car.ThirdRowSeating, opt => opt.MapFrom(src => src.CarDetails.ThirdRowSeating))
                .ForMember(car => car.LastTrip, opt => opt.MapFrom(src => src.Trips.OrderByDescending(x => x.CreatedAt).FirstOrDefault()))
            .ReverseMap();

            // Get All Users
            CreateMap<User, GetAllUserResponsetDto>().ReverseMap();

            // Car Offers
            CreateMap<Car, CarOfferDto>()
                .ForMember(car => car.ImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(gallery => gallery.IsFeature).ImageUrl))
                .ForMember(car => car.Rating, opt => opt.MapFrom(src => src.Ratings.Count == 0 ? 0 : (double)src.Ratings.Sum(car => car.Ratings) / ((double)src.Ratings.Count)))
                .ForMember(car => car.Count, opt => opt.MapFrom(src => src.Ratings.Count))
                .ForMember(car => car.Offer, opt => opt.MapFrom(src => src.Offers.OrderByDescending(x => x.CreatedAt).FirstOrDefault()))
                .ReverseMap();


            // Car listings
            CreateMap<Car, CarResponseDto>()
                .ForMember(car => car.ImageUrl, opt => opt.MapFrom(src => src.Images.Select(x => x.ImageUrl).FirstOrDefault()))
                .ForMember(car => car.Rating, opt => opt.MapFrom(src => src.Ratings.Count == 0 ? 0 : (double)src.Ratings.Sum(car => car.Ratings) / ((double)src.Ratings.Count)))
                .ForMember(car => car.NoOfPeople, opt => opt.MapFrom(src => src.Ratings.Count)).ReverseMap();

            // Dealer listings

            CreateMap<Dealer, GetAllDealerResponseDto>()
                .ReverseMap();

            // Add New Car
            CreateMap<Car, CarRequestDTO>()
                .ForMember(car => car.TypeOfSeat, opt => opt.MapFrom(src => src.CarDetails.TypeOfSeat))
                .ForMember(car => car.Sunroof, opt => opt.MapFrom(src => src.CarDetails.Sunroof))
                .ForMember(car => car.Bluetooth, opt => opt.MapFrom(src => src.CarDetails.Bluetooth))
                .ForMember(car => car.AirCondition, opt => opt.MapFrom(src => src.CarDetails.AirCondition))
                .ForMember(car => car.BackUpcamera, opt => opt.MapFrom(src => src.CarDetails.BackUpcamera))
                .ForMember(car => car.CarPlay, opt => opt.MapFrom(src => src.CarDetails.CarPlay))
                .ForMember(car => car.Driver, opt => opt.MapFrom(src => src.CarDetails.Driver))
                .ForMember(car => car.NavigationSystem, opt => opt.MapFrom(src => src.CarDetails.NavigationSystem))
                .ForMember(car => car.RemoteStart, opt => opt.MapFrom(src => src.CarDetails.RemoteStart))
                .ForMember(car => car.ThirdRowSeating, opt => opt.MapFrom(src => src.CarDetails.ThirdRowSeating))
                .ReverseMap();

            // List of All Trips
            CreateMap<Trip, AllTripsDto>()
               .ForMember(trip => trip.Amount, opt => opt.MapFrom(op => op.Transactions))
               .ForMember(trip => trip.PaymentMethod, opt => opt.MapFrom(op => op.Transactions))
               .ForMember(trip => trip.TransactionRef, opt => opt.MapFrom(op => op.Transactions))
               .ForMember(trip => trip.Status, opt => opt.MapFrom(op => op.Transactions))
               .ReverseMap();


            // Update car
            CreateMap<Car, CarUpdateDto>()
                .ForMember(car => car.TypeOfSeat, opt => opt.MapFrom(src => src.CarDetails.TypeOfSeat))
                .ForMember(car => car.Sunroof, opt => opt.MapFrom(src => src.CarDetails.Sunroof))
                .ForMember(car => car.Bluetooth, opt => opt.MapFrom(src => src.CarDetails.Bluetooth))
                .ForMember(car => car.AirCondition, opt => opt.MapFrom(src => src.CarDetails.AirCondition))
                .ForMember(car => car.BackUpcamera, opt => opt.MapFrom(src => src.CarDetails.BackUpcamera))
                .ForMember(car => car.CarPlay, opt => opt.MapFrom(src => src.CarDetails.CarPlay))
                .ForMember(car => car.NavigationSystem, opt => opt.MapFrom(src => src.CarDetails.NavigationSystem))
                .ForMember(car => car.ThirdRowSeating, opt => opt.MapFrom(src => src.CarDetails.ThirdRowSeating))
                .ForMember(car => car.RemoteStart, opt => opt.MapFrom(src => src.CarDetails.RemoteStart))
                .ReverseMap();


            


        }
    }
}
