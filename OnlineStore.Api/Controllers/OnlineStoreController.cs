using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
                string result = _Services.Create(customerModel);
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        public IActionResult GetCustomerByPhone(string id)
        {
            var desk = _Services.GetCustomerByPhone(id);

            return desk == null ? NotFound() : Ok(desk);
        }
        //// получение Desck по id проекта
        //[HttpGet("{project/projectId}")] // исключение поле не может быть пустым
        //public async Task<IEnumerable<CommonModel>> GetProjectDesks(int projectId)
        //{
        //    var user = _usersService.GetUser(HttpContext.User.Identity.Name); // получение пользователя
        //    if (user != null)
        //    {
        //        return await _desksService.GetProjectDesks(projectId, user.Id).ToListAsync();

        //    }
        //    return Array.Empty<CommonModel>();
        //}






    }
}
