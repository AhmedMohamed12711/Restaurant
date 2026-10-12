using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger
    , IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Retuarant with Id : {@RestuarantId}",request.Id);
        var restaurant=await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        await restaurantsRepository.Delete(restaurant);
    }
}
