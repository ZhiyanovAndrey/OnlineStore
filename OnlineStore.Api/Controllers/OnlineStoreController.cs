using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStoreController : ControllerBase
    {

        // тестовый запрос
        [HttpGet("test")]
        public IActionResult Test()
        {

            string t = $"";
            return Ok($"Привет! Сервер запущен {DateTime.Now.ToString("D")} в {DateTime.Now.ToString("t")}");
        }

        private readonly OnlineStoreContext _db;

        public OnlineStoreController(OnlineStoreContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customerModel)
        {
            if (customerModel != null)
            {
                Customer newCustomer = new Customer(customerModel.Lastname, customerModel.Firstname, customerModel.Firdname,
                    customerModel.Phone);
                _db.Customers.Add(newCustomer);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        //	Метод добавления клиента.


        //	Метод добавления клиента.


        //	Метод получения клиента по номеру телефона.


        //  Метод получения списка товаров, с возможностью фильтрации по типу товара и/или по наличию на складе и сортировки по цене(возрастанию и убыванию).
        //	
        //	
        //	Метод получения списка заказов по конкретному клиенту за выбранный временной период, отсортированный по дате создания.

        // Метод формирования заказа с проверкой наличия требуемого количества товара на складе, а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.

        //	Прочие методы, на усмотрение кандидата.

    }
}
