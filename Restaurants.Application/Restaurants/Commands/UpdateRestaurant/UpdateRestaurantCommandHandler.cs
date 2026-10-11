using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler
    (ILogger<UpdateRestaurantCommandHandler> logger
    , IRestaurantsRepository restaurantsRepository,
    IMapper mapper)
    : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Update Restaurant with Id: {request.Id}");
        var restaurant=await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant == null) return false;
        mapper.Map(request, restaurant);
        await restaurantsRepository.SaveChanges();
        return true;
    }
}
