using Restaurants.Domain.Entities;

namespace Rsestaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    public Task<IEnumerable<Restaurant>> GetAllAsync();

    public Task<Restaurant?> GetByIdAsync(int id);

    public Task<int> Create(Restaurant entity);
    public Task Delete(Restaurant entity);
    Task SaveChanges();
}
