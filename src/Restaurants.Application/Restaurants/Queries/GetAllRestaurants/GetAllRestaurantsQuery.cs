using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Rsestaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery:IRequest<PagedResult<RestaurantDto>>
{
    public string? searchPhrases { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }   
    public string? SortBy { get; set; }   
    public SortDirections SortDirection { get; set; }
}
