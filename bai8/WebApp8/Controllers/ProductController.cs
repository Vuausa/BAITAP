using Microsoft.AspNetCore.Mvc;
using WebApp8.Models;

namespace WebApp8.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _products = new();
        private static int _nextId = 1;
        private readonly IWebHostEnvironment _env;

        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model, List<IFormFile> images)
        {
            images ??= new List<IFormFile>();

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Vui lòng nhập tên sản phẩm");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (images.Count == 0 || images.All(f => f == null || f.Length == 0))
            {
                ModelState.AddModelError("images", "Vui lòng chọn ít nhất một hình ảnh");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsDir);

            var imagePaths = new List<string>();

            foreach (var file in images)
            {
                if (file is null || file.Length == 0) continue;

                var ext = Path.GetExtension(file?.FileName)?.ToLowerInvariant() ?? "";

                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("images", "Chỉ chấp nhận file .jpg và .png");
                    return View(model);
                }

                var fileName = $"{Guid.NewGuid()}{ext}";
                var filePath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file!.CopyTo(stream);
                }

                imagePaths.Add($"/uploads/{fileName}");
            }

            model.Id = _nextId++;
            model.ImagePaths = imagePaths;
            _products.Add(model);

            return RedirectToAction("Index");
        }
    }
}
