using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Data;
using OnlineStore.Models;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStoreOrderPositionController : ControllerBase
    {
     
        private readonly OrderService _OrderService;


        public OnlineStoreOrderPositionController(OnlineStoreContext db)
        {
           
            _OrderService = new OrderService(db);

        }


        // 5)	Метод формирования заказа с проверкой наличия требуемого количества товара на складе,
        // а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.

        [HttpPost]
        public IActionResult CreateOrderPosition([FromBody] OrderPositionModel orderPositionModel)
        {

            if (orderPositionModel != null)
            {


                var result = _OrderService.CreateOrderPosition(orderPositionModel);



                return Ok(result);
            }
            
            return BadRequest();
        }





    }
}
