using Restaurants.Domain.Entities;

namespace Rsestaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task Delete(IEnumerable<Dish> entities);
  
    }
}
