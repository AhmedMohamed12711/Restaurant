using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Rsestaurants.Domain.Constants;
using Rsestaurants.Domain.Repositories;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }
    public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrases,int PageNumber,int PageSize, string? SortBy, SortDirections SortDirection)
    {
        var normalizedSearchPhrase = searchPhrases?.Trim().ToLower();
        var baseQuery = dbContext.Restaurants
                            .AsNoTracking()
                            .Where(r => string.IsNullOrEmpty(normalizedSearchPhrase)
                            || (r.Name.ToLower().Contains(normalizedSearchPhrase)
                            || r.Description.ToLower().Contains(normalizedSearchPhrase)));

        var totalCount=await baseQuery.CountAsync();

        IOrderedQueryable<Restaurant> orderedQuery;

        if (SortBy != null)
        {
            var columnSelected=new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name),r=>r.Name },
                {nameof(Restaurant.Category),r=>r.Category },
                {nameof(Restaurant.Description),r=>r.Description }
            };
            if (columnSelected.TryGetValue(SortBy, out var selectionColumn))
            {
                orderedQuery = SortDirection == SortDirections.Ascending
                    ? baseQuery.OrderBy(selectionColumn)
                    : baseQuery.OrderByDescending(selectionColumn);
            }
            else
            {
                orderedQuery = baseQuery.OrderBy(r => r.Id);
            }
        }
        else
        {
            orderedQuery = baseQuery.OrderBy(r => r.Id);
        };

        var restaurants = await orderedQuery
       .Skip(PageSize *(PageNumber-1))
       .Take(PageSize).ToListAsync();


        return (restaurants,totalCount);
    }
    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurants = await dbContext.Restaurants
            .Include(r=>r.Dishes)
            .FirstOrDefaultAsync(x=>x.Id==id);
        return restaurants;
    }

    public  Task SaveChanges()=>dbContext.SaveChangesAsync();
    
}
