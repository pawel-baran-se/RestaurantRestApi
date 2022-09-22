using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantRestApi.Entities;
using RestaurantRestApi.Exceptions;
using RestaurantRestApi.Models;
using System.Linq.Expressions;

namespace RestaurantRestApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .SingleOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            var dishes = _dbContext
                .Dishes
                .Where(d => d.RestaurantId == id)
                .ToList();

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var baseQuery = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .Where(r => query.searchPhrase == null || (r.Name.ToLower().Contains(query.searchPhrase.ToLower())
                    || r.Description.ToLower().Contains(query.searchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r => r.Name},
                    {nameof(Restaurant.Description), r => r.Description},
                    {nameof(Restaurant.Category), r => r.Category},
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                     baseQuery.OrderBy(selectedColumn)
                     : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurant = baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();

            var restaurtantDtos = _mapper.Map<List<RestaurantDto>>(restaurant);
            var totalItemsCount = baseQuery.Count();

            var result = new PageResult<RestaurantDto>(restaurtantDtos, totalItemsCount, query.pageSize, query.pageNumber);

            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurantToUpdate = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurantToUpdate is null) throw new NotFoundException("Restaurant not found");

            restaurantToUpdate.Name = dto.Name;
            restaurantToUpdate.Description = dto.Description;
            restaurantToUpdate.HasDelivery = dto.HasDelivery;

            _dbContext.Restaurants.Update(restaurantToUpdate);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }
    }
}