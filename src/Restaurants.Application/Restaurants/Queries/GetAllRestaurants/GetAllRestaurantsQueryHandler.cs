using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler
    (ILogger<GetAllRestaurantsQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository)
    :IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get All Restaurants");
        var (restaurants,totalcount) = await restaurantsRepository.GetAllMatchingAsync(request.searchPhrases
            ,request.PageNumber,request.PageSize,request.SortBy,request.SortDirection);
                   
        
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        var result = new PagedResult<RestaurantDto>(restaurantsDto,totalcount, request.PageSize, request.PageNumber);
        return result;
    }
}
