using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.DTOSs;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Entities;

namespace RestaurantsAPI.Controllers
{
    [Route("api/restuarants/{restuarantId}/dishes")]
    [ApiController]
    public class DishController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId= await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant),new { restaurantId, dishId },null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restuarantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restuarantId));
            return Ok(dishes);
        }
        [HttpGet("{dishId}")]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetByIdForRestaurant([FromRoute] int restuarantId, [FromRoute] int dishId)
        {
            var dishes= await mediator.Send(new GetDishByIdForRestaurantQuery(restuarantId , dishId));
            return Ok(dishes);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}
