using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Data;
using OnlineStore.Models;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStoreOrderController : ControllerBase
    {
 
        private readonly Services _Services;

        public OnlineStoreOrderController(OnlineStoreContext db)
        {
           
            _Services = new Services(db);
        }


        [HttpPost]
        public  IActionResult CreateOrder([FromBody] OrderModel orderModel)
        {

            if (orderModel != null)
            {
               string result = _Services.CreateOrder(orderModel);
               return Ok(result); 
            }
          ;
            return BadRequest();
        }




        //  4)	Метод получения списка заказов по конкретному клиенту за выбранный временной период,
        //  отсортированный по дате создания.
        [HttpGet("[action]")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IEnumerable<OrderModel>> GetOrdersByCustomeer(int CustomerId, DateTime dateStart, DateTime dateEnd) // или использовать onlyDate
        {
            return await _Services.GetOrderByCustomer(CustomerId, dateStart, dateEnd);

        }






    }
}
