using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private static readonly List<Book> _books =
        [
            new Book { Id = 1, Name = "Clean Code", Price = 20 },
            new Book { Id = 2, Name = "ASP.NET MVC", Price = 15 },
            new Book { Id = 3, Name = "Design Pattern", Price = 25 }
        ];

        private static int _nextId = 4;

        public IActionResult Index()
        {
            return View(_books);
        }

        public IActionResult Detail(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Book());
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            var newBook = new Book
            {
                Id = _nextId++,
                Name = book.Name,
                Price = book.Price
            };
            _books.Add(newBook);

            ViewBag.SuccessMessage = "Thêm sách thành công!";
            return View(new Book());
        }
    }

}
