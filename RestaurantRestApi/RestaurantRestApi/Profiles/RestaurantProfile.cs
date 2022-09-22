using AutoMapper;
using RestaurantRestApi.Entities;
using RestaurantRestApi.Models;

namespace RestaurantRestApi.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(d => d.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(d => d.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address()
                { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));


            CreateMap<CreateDishDto, Dish>();

        }
    }
}