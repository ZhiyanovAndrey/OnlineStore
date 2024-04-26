using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Models;
using OnlineStore.Api.Models.Data;
using OnlineStore.Models;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStoreCustomerController : ControllerBase
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

        public OnlineStoreCustomerController(OnlineStoreContext db)
        {
            _db = db;
            _Services = new Services(db);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (customerModel != null)
            {
                try
                {
                    var result = await _Services.CreateCustomerAsync(customerModel);
                    return result == null ? NotFound() : Ok(result);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
          ;
            return BadRequest();
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
           
            var result = await _Services.DeleteCustomerAsync(id);
            return result == null ? NotFound() : Ok(result);

        }


        // 2)	Метод получения клиента по номеру телефона.

        [HttpGet("customers/{phone}")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        public IActionResult GetCustomerByPhone(string phone)
        {
            var customer = _Services.GetCustomerByPhoneAsync(phone);

            return customer == null ? NotFound() : Ok(customer);
        }

        // Метод получения всех покупателей
        [HttpGet("customers")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
        public async Task<IEnumerable<CustomerModel>> GetCustomers()
        {
            return await _db.Customers.OrderBy(p => p.Lastname).Select(u => u.ToDto()).ToListAsync();
        }

        // Метод получения всех продуктов

        [HttpGet("products")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
        public async Task<IEnumerable<Product>> Getproducts()
        {
            return await _db.Products.ToListAsync();
        }

        //3) Метод получения списка товаров, с возможностью фильтрации по типу товара и/или по наличию на складе и сортировки по цене (возрастанию и убыванию).

        [HttpGet("products/{sortOrder}")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
        public async Task<IEnumerable<Product>> GetProducts(string sortOrder)
        {
            return await _Services.GetProductsWithSort(sortOrder);
        }



        [HttpGet("products/filter/{CategoryId}")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
        public async Task<IEnumerable<Category>> GetProductsByCategory(int CategoryId)
        {

            var products = _db.Categories.Include(c => c.Products).Where(p => p.Categoryid == CategoryId);
            return await products.ToListAsync();
        }










    }
}
