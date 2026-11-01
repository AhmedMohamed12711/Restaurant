using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Rsestaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Repositories;

public class dishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<int> Create(Dish entity)
    {
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
    public async Task Delete(IEnumerable<Dish> entities)
    {
        dbContext.Dishes.RemoveRange(entities);
        await dbContext.SaveChangesAsync();
    }
}
