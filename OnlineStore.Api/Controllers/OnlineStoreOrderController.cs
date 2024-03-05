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
        public IActionResult CreateOrder([FromBody] OrderModel orderModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (orderModel != null)
            {
                Task<string> result = _Services.CreateOrder(orderModel);
                return result == null ? NotFound() : Ok(result);
            }
          ;
            return BadRequest();
        }




        //  4)	Метод получения списка заказов по конкретному клиенту за выбранный временной период,
        //  отсортированный по дате создания.
        [HttpGet("orders/{CustomerId}")]
        public async Task<IEnumerable<Order>> GetOrdersByCustomeer(int CustomerId, DateTime dateStart, DateTime dateEnd) // или использовать onlyDate
        {
            // Where(x => x.Age >= userParameter.MinAgeFilter && x.Age <= userParameter.MaxAgeFilter)
            var orders = _db.Orders.Include(o => o.Customer)
                .Where(c => c.Customerid == CustomerId)
                .Where(o => o.Orderdate >= dateStart.Date && o.Orderdate <= dateEnd.Date)
                .OrderBy(o => o.Orderdate);
            return await orders.ToListAsync();


          
        }



        // 5)	Метод формирования заказа с проверкой наличия требуемого количества товара на складе,
        // а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.

    }
}
