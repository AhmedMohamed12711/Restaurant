using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator:AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowPageSize = [5, 10, 15, 30];
    private string[] allowedSortByColumnsName = [nameof(RestaurantDto.Name)
        , nameof(RestaurantDto.Category), nameof(RestaurantDto.Description)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);


        RuleFor(r => r.PageSize)
            .Must(value=>allowPageSize.Contains(value))
            .WithMessage($"PageSize must be in [{string.Join(",",allowPageSize)}]");

        RuleFor(r => r.SortBy)
            .Must(value=> allowedSortByColumnsName.Contains(value))
            .When(q=>q.SortBy!=null)
            .WithMessage($"PageSize must be in [{string.Join(",", allowedSortByColumnsName)}]");
    }
}
