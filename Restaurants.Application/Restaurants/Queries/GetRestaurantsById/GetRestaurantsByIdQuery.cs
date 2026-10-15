using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantsById;

public class GetRestaurantsByIdQuery(int id) : IRequest<RestaurantDto>
{

    public int Id { get;  } = id;
}
