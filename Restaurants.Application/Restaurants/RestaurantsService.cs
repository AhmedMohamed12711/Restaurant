using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

public class RestaurantsService(IRestaurantsRepository restaurantsRepository, 
    ILogger<RestaurantsService> logger,IMapper mapper) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Create a new restaurant");
        var restaurant=mapper.Map<Restaurant>(dto);
        int id=await restaurantsRepository.Create(restaurant);
        return id;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Get All Restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDto!;
    }
    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation($"Get Specific Restaurant by{id}");
        var restaurant=await restaurantsRepository.GetByIdAsync(id);
        var restaurantsDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantsDto;
    }
}
