using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (customerModel != null)
            {
                string result = _Services.CreateCustomer(customerModel);
                return result == null ? NotFound() : Ok(result);
            }
          ;
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            string result = _Services.Delete(id);
            return result == null ? NotFound() : Ok(result);

        }


        // 2)	Метод получения клиента по номеру телефона.

        [HttpGet("{id}")]
        public IActionResult GetCustomerByPhone(string id)
        {
            var customer = _Services.GetCustomerByPhone(id);

            return customer == null ? NotFound() : Ok(customer);
        }

        // Метод получения всех покупателей
        [HttpGet("customers")]
        public async Task<IEnumerable<CustomerModel>> GetCustomers()
        {
            return await _db.Customers.OrderBy(p => p.Lastname).Select(u => u.ToDto()).ToListAsync();
        }

        // Метод получения всех продуктов
        [HttpGet("products")]
        public async Task<IEnumerable<Product>> Getproducts()
        {
            return await _db.Products.ToListAsync();
        }

        //3) Метод получения списка товаров, с возможностью фильтрации по типу товара и/или по наличию на складе и сортировки по цене (возрастанию и убыванию).

        [HttpGet("products/{sortOrder}")]
        public async Task<IEnumerable<Product>> GetProducts(string sortOrder)
        {
            var products = _db.Products.Select(u => u);


            switch (sortOrder)
            {
                case "ProductName_desc":
                    products = products.OrderByDescending(p => p.Productname);
                    break;
                case "ProductName":
                    products = products.OrderBy(p => p.Productname);
                    break;
                case "ProductPrice_desc":
                    products = products.OrderByDescending(p => p.Unitprice);
                    break;
                case "ProductPrice":
                    products = products.OrderBy(p => p.Unitprice);
                    break;
                case "UnitsinstockFilter":
                    products = products.Where(p => p.Unitsinstock > 0);
                    break;
                default:
                    products = products.OrderBy(p => p.Productname);
                    break;
            }


            return await products.ToListAsync();
        }

        [HttpGet("products/filter/{CategoryId}")]
        public async Task<IEnumerable<Category>> GetProductsByCategory(int CategoryId)
        {

            var products = _db.Categories.Include(c => c.Products).Where(p => p.Categoryid == CategoryId);
            return await products.ToListAsync();
        }










    }
}
