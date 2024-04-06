using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Data;
using OnlineStore.Models;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStoreOrderPositionController : ControllerBase
    {
     
        private readonly Services _Services;

        public OnlineStoreOrderPositionController(OnlineStoreContext db)
        {
           
            _Services = new Services(db);
        }


        // 5)	Метод формирования заказа с проверкой наличия требуемого количества товара на складе,
        // а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.

        [HttpPost]
        public IActionResult CreateOrderPosition([FromBody] OrderPositionModel orderPositionModel)
        {

            if (orderPositionModel != null)
            {


                string result = _Services.CreateOrderPosition(orderPositionModel);



                return Ok(result);
            }
            
            return BadRequest();
        }





    }
}
