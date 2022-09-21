using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantRestApi.Entities;
using RestaurantRestApi.Models;

namespace RestaurantRestApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .SingleOrDefault(r => r.Id == id);

            if (restaurant is null) return null;

            var dishes = _dbContext
                .Dishes
                .Where(d => d.RestaurantId == id)
                .ToList();

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurant = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .ToList();

            var restaurtantDtos = _mapper.Map<List<RestaurantDto>>(restaurant);

            return restaurtantDtos;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public bool Update(int id, UpdateRestaurantDto dto)
        {
            var restaurantToUpdate = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurantToUpdate is null) return false;

            restaurantToUpdate.Name = dto.Name;
            restaurantToUpdate.Description = dto.Description;
            restaurantToUpdate.HasDelivery = dto.HasDelivery;

            _dbContext.Restaurants.Update(restaurantToUpdate);
            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return false;

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

            return true;
        }
    }
}