using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantsById;

public class GetRestaurantsByIdQueryHandler(ILogger<GetRestaurantsByIdQueryHandler> logger,
    IMapper mapper,IRestaurantsRepository restaurantsRepository)
    : IRequestHandler<GetRestaurantsByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantsByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get Specific Restaurant by{request.Id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        var restaurantsDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantsDto!;
    }
}
