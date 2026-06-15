using Microsoft.AspNetCore.Mvc;
using WebApp2.Models;

namespace WebApp2.Controllers
{
    public class SachController : Controller
    {
        private static readonly List<Sach> _danhSachSach = new();

        public IActionResult Index()
        {
            return View(_danhSachSach);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sach sach)
        {
            if (!ModelState.IsValid)
            {
                return View(sach);
            }

            sach.Id = _danhSachSach.Count + 1;
            _danhSachSach.Add(sach);
            return RedirectToAction(nameof(Index));
        }
    }
}
