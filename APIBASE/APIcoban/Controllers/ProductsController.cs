using Microsoft.AspNetCore.Mvc;
using APIcoban.Models;

namespace APIcoban.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Sử dụng static list làm cơ sở dữ liệu tạm thời (In-Memory Database)
        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Bàn phím cơ", Price = 1500000 },
            new Product { Id = 2, Name = "Chuột gaming", Price = 800000 },
            new Product { Id = 3, Name = "Màn hình 24 inch", Price = 3500000 }
        };

        private static int _nextId = 4;

        // GET /api/products (Lấy danh sách tất cả sản phẩm)
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(Products);
        }

        // GET /api/products/{id} (Lấy chi tiết sản phẩm theo ID)
        [HttpGet("{id}")]
        public IActionResult GetProductById(string id)
        {
            // Validate id: phải là số nguyên dương
            // Ở đây sử dụng string để bắt được mọi trường hợp nhập vào (ví dụ: chữ cái, số âm, số thập phân)
            if (string.IsNullOrWhiteSpace(id) || !int.TryParse(id, out int parsedId) || parsedId <= 0)
            {
                return BadRequest(new { errors = new[] { "ID phải là số nguyên dương." } });
            }

            var product = Products.FirstOrDefault(p => p.Id == parsedId);
            if (product == null)
            {
                return NotFound(new { message = $"Không tìm thấy sản phẩm với ID = {parsedId}" });
            }

            return Ok(product);
        }

        // POST /api/products (Thêm sản phẩm mới)
        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation)
            if (!ModelState.IsValid)
            {
                // Trích xuất toàn bộ thông báo lỗi và đưa vào một danh sách
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { errors });
            }

            // Tạo sản phẩm mới
            var newProduct = new Product
            {
                Id = _nextId++,
                Name = request.Name,
                Price = request.Price
            };

            // Thêm vào cơ sở dữ liệu tạm thời
            Products.Add(newProduct);

            // Trả về Status 201 Created cùng đường dẫn và dữ liệu sản phẩm vừa tạo
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id.ToString() }, newProduct);
        }
    }
}
