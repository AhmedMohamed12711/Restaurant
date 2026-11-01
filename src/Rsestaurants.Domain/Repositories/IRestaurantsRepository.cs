using Restaurants.Domain.Entities;
using Rsestaurants.Domain.Constants;

namespace Rsestaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        public Task<IEnumerable<Restaurant>> GetAllAsync();

        public Task<Restaurant?> GetByIdAsync(int id);

        public Task<int> Create(Restaurant entity);
        public Task Delete(Restaurant entity);
        Task SaveChanges();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrases, int PageNumber, int PageSize, string? SortBy, SortDirections SortDirection);
    }
}
