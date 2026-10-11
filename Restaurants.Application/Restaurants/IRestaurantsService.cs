using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
        public  Task<RestaurantDto?> GetRestaurantById(int id);
        Task<int> Create(CreateRestaurantDto dto);
    }
}