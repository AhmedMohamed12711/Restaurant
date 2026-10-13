using MediatR;
using Restaurants.Application.Dishes.DTOSs;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery(int restuarantId,int dishId):IRequest<DishDto>
{
    public int RestuarantId { get; } = restuarantId;
    public int DishId { get; } = dishId;
}
