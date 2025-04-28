using Api.ecommerce.EC;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Microsoft.AspNetCore.Mvc;

namespace Api.ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ShoppingEC _shoppingEC;
        private readonly ILogger<ShoppingController> _logger;

        public ShoppingController(ShoppingEC shoppingEC, ILogger<ShoppingController> logger)
        {
            _shoppingEC = shoppingEC;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _shoppingEC.Get();
            if (items == null || !items.Any())
            {
                return NotFound("No items found in the cart.");
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _shoppingEC.Get().FirstOrDefault(i => i?.Id == id);
            if (item == null)
            {
                return NotFound("Item not found in the cart.");
            }
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedItem = _shoppingEC.Delete(id);
            if (deletedItem == null)
            {
                return NotFound("Item not found in the cart.");
            }
            return Ok(deletedItem);
        }

        [HttpPost]
        public IActionResult AddOrUpdate([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest("Invalid item data.");
            }

            var newItem = _shoppingEC.AddOrUpdate(item);
            return Ok(newItem);
        }

        [HttpPost("Search")]
        public IActionResult Search([FromBody] QueryRequest query)
        {
            var items = _shoppingEC.Get(query.Query);
            if (items == null || !items.Any())
            {
                return NotFound("No items found for the given search query.");
            }
            return Ok(items);
        }
    }
}


