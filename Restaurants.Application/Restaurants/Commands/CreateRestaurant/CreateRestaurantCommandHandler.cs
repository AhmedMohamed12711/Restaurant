using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IMapper mapper
    ,IRestaurantsRepository restaurantsRepository,
    ILogger<CreateRestaurantCommandHandler> logger)
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create a new restaurant");
        var restaurant = mapper.Map<Restaurant>(request);
        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }
}
