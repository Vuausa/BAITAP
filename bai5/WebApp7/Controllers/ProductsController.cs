using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Database giả lập
        private static List<Product> _products = new List<Product>();

        // 1 & 2. POST /api/products - Thêm mới & Tự động validate
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            // Tự tăng ID
            product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);

            return Ok(new { Message = "Thêm sản phẩm thành công", Data = product });
        }

        // 3. GET /api/products/{id} - Lấy theo ID & Validate id > 0
        [HttpGet("{id:int:min(1)}")]
        public IActionResult GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = "Không tìm thấy sản phẩm." });
            }

            return Ok(product);
        }
    }
}