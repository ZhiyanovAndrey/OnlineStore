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

        private readonly OrderService _OrderService;
        private readonly Services _Services;

        public OnlineStoreOrderController(OnlineStoreContext db) // принимаем db в конструктор контроллера
        {

            _OrderService = new OrderService(db); // и передаем db в конструктор OrderService 
            _Services = new Services(db); 



        }



        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderModel orderModel)
        {

            if (orderModel != null)
            {
                try
                {
                    var result = await _OrderService.CreateOrderAsync(orderModel); // метод расширения CreateOrderAsync принимающий тип OrderService
                    return Ok(result);

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var result = await _OrderService.DeleteOrder(id);
            return result == null ? NotFound() : Ok();

        }


        //  4)	Метод получения списка заказов по конкретному клиенту за выбранный временной период,
        //  отсортированный по дате создания.
        [HttpGet("[action]")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IEnumerable<OrderModel>> GetOrdersByCustomeer(int CustomerId, DateTime dateStart, DateTime dateEnd) 
        {
            try
            {
            return await _Services.GetOrderByCustomer(CustomerId, dateStart, dateEnd);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("orders")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _OrderService.GetOrdersAsync();
        }




    }
}
