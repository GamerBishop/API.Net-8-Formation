using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dto => dto.City, opt =>
                opt.MapFrom(restaurant => restaurant.Adress == null ? null : restaurant.Adress.City))
            .ForMember(dto => dto.Street, opt =>
                opt.MapFrom(restaurant => restaurant.Adress == null ? null : restaurant.Adress.Street))
            .ForMember(dto => dto.ZipCode,
                opt => opt.MapFrom(restaurant => restaurant.Adress == null ? null : restaurant.Adress.ZipCode))
            .ForMember(dto => dto.Dishes,
                opt => opt.MapFrom(restaurant => restaurant.Dishes));

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(restaurant => restaurant.Adress,
                opt => opt.MapFrom(dto => new Adress
                {
                    City = dto.City,
                    Street = dto.Street,
                    ZipCode = dto.ZipCode
                }));

        CreateMap<UpdateRestaurantCommand, Restaurant>();
    }

}
