using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult CreateOrder([FromBody] OrderpositionModel orderModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (orderModel != null)
            {
                string result = _Services.CreateOrder(orderModel);
                return Ok(result); //result == null ? NotFound() :
            }
          ;
            return BadRequest();
        }




        //  4)	Метод получения списка заказов по конкретному клиенту за выбранный временной период,
        //  отсортированный по дате создания.
        [HttpGet("orders/{CustomerId}")]
        public async Task<IEnumerable<OrderpositionModel>> GetOrdersByCustomeer(int CustomerId, DateTime dateStart, DateTime dateEnd) // или использовать onlyDate
        {
            return await _Services.GetOrderByCustomer(CustomerId, dateStart, dateEnd);

        }





        // 5)	Метод формирования заказа с проверкой наличия требуемого количества товара на складе,
        // а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.



    }
}
