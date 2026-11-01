using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOSs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger
    ,IRestaurantsRepository restaurantsRepository,IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id: {RestaurantId}",
        request.DishId,
        request.RestuarantId);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestuarantId);

        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestuarantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var result = mapper.Map<DishDto>(dish);
        return result;

    }
}
