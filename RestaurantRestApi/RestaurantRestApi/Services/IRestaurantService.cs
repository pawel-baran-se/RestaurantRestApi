using RestaurantRestApi.Models;

namespace RestaurantRestApi.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        PageResult<RestaurantDto> GetAll(RestaurantQuery query);
        RestaurantDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
    }
}