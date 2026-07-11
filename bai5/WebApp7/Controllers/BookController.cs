using Microsoft.AspNetCore.Mvc;
using WebApp7.Models;

namespace WebApp7.Controllers
{
    public class BookController : Controller
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Lap Trinh C#", Price = 150000 },
            new Book { Id = 2, Title = "ASP.NET Core", Price = 200000 },
            new Book { Id = 3, Title = "Hoc Python", Price = 120000 }
        };

        public IActionResult Index()
        {
            return View(_books);
        }

        public IActionResult Detail(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
            return RedirectToAction("Index");
        }
    }
}
