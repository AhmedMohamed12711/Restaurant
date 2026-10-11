using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantsById;

public class GetRestaurantsByIdQuery:IRequest<RestaurantDto?>
{
    public GetRestaurantsByIdQuery(int id)
    {
        Id = id;
    }
    public int Id {  get; set; }
}
