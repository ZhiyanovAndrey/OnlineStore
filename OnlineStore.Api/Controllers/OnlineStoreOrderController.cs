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

        // асинхронность дает ошибку сервера 500, но создает
        [HttpPost]
        public  IActionResult CreateOrder([FromBody] OrderModel orderModel)
        {

            if (orderModel != null)
            {
               Task<string> result = _Services.CreateOrder(orderModel);
               return Ok(result); 
            }
            return BadRequest();
        }


        // возврашает всегда 200 OK
        // асинхронность дает ошибку сервера 500, но удаляет

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            Task<string> result = _Services.DeleteOrder(id);
            return result == null ? NotFound() : Ok(result);

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
