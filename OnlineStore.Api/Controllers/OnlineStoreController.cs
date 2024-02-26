using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models;
using OnlineStore.Api.Models.Data;
using OnlineStore.Models;

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
        private readonly Services _Services;

        public OnlineStoreController(OnlineStoreContext db)
        {
            _db = db;
            _Services = new Services(db);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerModel customerModel)
        {
            if (customerModel != null)
            {
                bool result = _Services.Create(customerModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }


      

 


      

    }
}
