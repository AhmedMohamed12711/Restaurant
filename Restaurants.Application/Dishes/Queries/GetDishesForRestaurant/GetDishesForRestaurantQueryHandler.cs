using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOSs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger
    ,IRestaurantsRepository restaurantsRepository,IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes for restaurant with id: {RestaurantId}", request.RestuarantId);
        var restuarnat=await restaurantsRepository.GetByIdAsync(request.RestuarantId);
        if(restuarnat==null) throw new NotFoundException(nameof(Restaurant),request.RestuarantId.ToString());
        var result = mapper.Map<IEnumerable<DishDto>>(restuarnat.Dishes);
        return result;

    }
}
