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
        public IActionResult CreateOrder([FromBody] OrderModel orderModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (orderModel != null)
            {
                string result = _Services.CreateOrder(orderModel);
                return result == null ? NotFound() : Ok(result);
            }
          ;
            return BadRequest();
        }
    }
}
