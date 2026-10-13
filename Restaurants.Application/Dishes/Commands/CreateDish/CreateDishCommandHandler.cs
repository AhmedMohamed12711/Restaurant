using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler (ILogger<CreateDishCommandHandler> logger
    ,IRestaurantsRepository restaurantsRepository,IMapper mapper,IDishesRepository dishesRepository): IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);
        var restuarant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restuarant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.Create(dish);
    }
}
