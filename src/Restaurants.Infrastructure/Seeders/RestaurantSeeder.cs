using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];

        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        User owner = new User()
        {
            Email = "seed-user@test.com"
        };

        string[] names = [
            "KFC", "McDonald's", "Burger King", "Pizza Hut", "Domino's",
            "Subway", "Taco Bell", "Wendy's", "Chick-fil-A", "Popeyes",
            "Panda Express", "Five Guys", "Shake Shack", "Chipotle", "Olive Garden",
            "Red Lobster", "Applebee's", "Denny's", "IHOP", "Outback Steakhouse",
            "The Cheesecake Factory", "Buffalo Wild Wings", "TGI Friday's", "Cracker Barrel", "Waffle House",
            "Nando's", "Wagamama", "Zizzi", "Pizza Express", "Greggs",
            "Leon", "Tortilla", "Honest Burgers", "Byron", "GBK",
            "Patty & Bun", "Flat Iron", "Dishoom", "Hawksmoor", "The Ivy",
            "Nobu", "Hakkasan", "Yo Sushi", "Itsu", "Wasabi",
            "Sushi Samba", "Roka", "Benihana", "Busaba", "Rosa's Thai",
        ];

        string[] categories = [
            "Fast Food", "Italian", "Japanese", "Mexican", "Chinese",
            "Indian", "Thai", "Mediterranean", "Steakhouse", "Seafood",
            "Vegetarian", "Vegan", "American", "French", "Korean",
            "Middle Eastern", "Greek", "Turkish", "Brazilian", "Spanish"
        ];

        string[] cities = [
            "London", "Manchester", "Birmingham", "Leeds", "Liverpool",
            "Glasgow", "Edinburgh", "Bristol", "Cardiff", "Belfast",
            "Cairo", "Alexandria", "Dubai", "New York", "Paris",
            "Berlin", "Tokyo", "Sydney", "Toronto", "Rome"
        ];

        string[] streets = [
            "Oxford St", "High St", "King's Rd", "Baker St", "Elm St",
            "Park Ave", "Main St", "Broadway", "Church Rd", "Victoria St",
            "Queen St", "Regent St", "Bond St", "Fleet St", "Piccadilly",
            "Strand", "Whitehall", "Kensington Rd", "Chelsea Blvd", "Camden Rd"
        ];

        string[] dishNames = [
            "Classic Burger", "Grilled Chicken", "Caesar Salad", "Margherita Pizza",
            "Fish & Chips", "Pad Thai", "Sushi Platter", "Tacos Al Pastor",
            "Lamb Kebab", "Chicken Wings", "Steak Frites", "Pasta Carbonara",
            "Tom Yum Soup", "Biryani", "Falafel Wrap", "Ramen Bowl",
            "BBQ Ribs", "Greek Salad", "Shawarma Plate", "Dim Sum Basket"
        ];

        var restaurants = new List<Restaurant>();
        var random = new Random(42); // fixed seed for reproducibility

        for (int i = 0; i < 100; i++)
        {
            var nameIndex = i % names.Length;
            var categoryIndex = i % categories.Length;
            var cityIndex = i % cities.Length;
            var streetIndex = i % streets.Length;

            // Make names unique by appending a branch number when we cycle through
            var branchNumber = i / names.Length;
            var restaurantName = branchNumber == 0 ? names[nameIndex] : $"{names[nameIndex]} - Branch {branchNumber + 1}";

            var restaurant = new Restaurant()
            {
                Owner = owner,
                Name = restaurantName,
                Category = categories[categoryIndex],
                Description = $"{restaurantName} is a popular {categories[categoryIndex].ToLower()} restaurant known for its excellent food and great service. Located in {cities[cityIndex]}.",
                ContactEmail = $"contact{i + 1}@{names[nameIndex].ToLower().Replace(" ", "").Replace("'", "").Replace("&", "and")}.com",
                HasDelivery = i % 3 != 0, // ~66% have delivery
                Dishes =
                [
                    new()
                    {
                        Name = dishNames[i % dishNames.Length],
                        Description = $"{dishNames[i % dishNames.Length]} - Chef's special",
                        Price = Math.Round(5.00M + (decimal)(random.NextDouble() * 25), 2),
                    },
                    new()
                    {
                        Name = dishNames[(i + 7) % dishNames.Length],
                        Description = $"{dishNames[(i + 7) % dishNames.Length]} - House favorite",
                        Price = Math.Round(4.00M + (decimal)(random.NextDouble() * 20), 2),
                    },
                ],
                Address = new()
                {
                    City = cities[cityIndex],
                    Street = $"{streets[streetIndex]} {i + 1}",
                    PostalCode = $"{(char)('A' + cityIndex % 26)}{random.Next(1, 9)}{random.Next(1, 9)} {random.Next(1, 9)}{(char)('A' + random.Next(0, 26))}{(char)('A' + random.Next(0, 26))}"
                },
            };

            restaurants.Add(restaurant);
        }

        return restaurants;
    }
}