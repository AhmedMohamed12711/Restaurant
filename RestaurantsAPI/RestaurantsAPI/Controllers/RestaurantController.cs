using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOs;
using System.Threading.Tasks;

namespace RestaurantsAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController (IRestaurantsService restaurantsService): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants =await restaurantsService.GetAllRestaurants();
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var restaurants = await restaurantsService.GetRestaurantById(id);
            if (restaurants == null) return NotFound();
            return Ok(restaurants);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
        {
            int id=await restaurantsService.Create(createRestaurantDto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }

}
