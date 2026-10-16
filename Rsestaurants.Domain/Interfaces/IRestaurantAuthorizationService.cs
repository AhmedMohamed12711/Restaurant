using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Rsestaurants.Domain.Constants;

namespace Restaurants.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}