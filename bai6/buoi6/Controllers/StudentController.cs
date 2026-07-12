using buoi6.Models;
using Microsoft.AspNetCore.Mvc;

namespace buoi6.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index(string? search, string? sortBy, string? sortOrder)
        {
            var students = StudentRepository.Search(search);

            sortBy ??= "Id";
            sortOrder ??= "asc";

            students = sortBy switch
            {
                "Name" => sortOrder == "desc"
                    ? students.OrderByDescending(s => s.Name).ToList()
                    : students.OrderBy(s => s.Name).ToList(),
                "Email" => sortOrder == "desc"
                    ? students.OrderByDescending(s => s.Email).ToList()
                    : students.OrderBy(s => s.Email).ToList(),
                "Age" => sortOrder == "desc"
                    ? students.OrderByDescending(s => s.Age).ToList()
                    : students.OrderBy(s => s.Age).ToList(),
                _ => sortOrder == "desc"
                    ? students.OrderByDescending(s => s.Id).ToList()
                    : students.OrderBy(s => s.Id).ToList()
            };

            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;

            return View(students);
        }

        public IActionResult Detail(int id)
        {
            var student = StudentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            StudentRepository.Add(student);
            TempData["SuccessMessage"] = "Thêm sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var student = StudentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            if (StudentRepository.GetById(student.Id) == null)
            {
                return NotFound();
            }

            StudentRepository.Update(student);
            TempData["SuccessMessage"] = "Cập nhật sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var student = StudentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = StudentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            StudentRepository.Delete(id);
            TempData["SuccessMessage"] = "Xóa sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
