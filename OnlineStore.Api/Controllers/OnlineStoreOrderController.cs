using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models;
using OnlineStore.Api.Models.Data;
using OnlineStore.Models;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStoreOrderController : ControllerBase
    {
        private readonly OnlineStoreContext _db;
        private readonly Services _Services;

        public OnlineStoreOrderController(OnlineStoreContext db)
        {
            _db = db;
            _Services = new Services(db);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (order != null)
            {
                string result = _Services.CreateOrder(order);
                return result == null ? NotFound() : Ok(result);
            }
          ;
            return BadRequest();
        }
    }
}
