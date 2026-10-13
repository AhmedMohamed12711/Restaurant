using MediatR;
using Restaurants.Application.Dishes.DTOSs;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery(int restuarantId) :IRequest<IEnumerable<DishDto>>
{
    public int RestuarantId { get; } = restuarantId;
}
