namespace buoi6.Models
{
    public static class StudentRepository
    {
        private static readonly List<Student> _students = new()
        {
            new Student { Id = 1, Name = "Nguyễn Văn An", Email = "an.nguyen@email.com", Phone = "0901234567", Major = "Công nghệ thông tin", Age = 20 },
            new Student { Id = 2, Name = "Trần Thị Bình", Email = "binh.tran@email.com", Phone = "0912345678", Major = "Kế toán", Age = 21 },
            new Student { Id = 3, Name = "Lê Văn Cường", Email = "cuong.le@email.com", Phone = "0923456789", Major = "Quản trị kinh doanh", Age = 22 },
            new Student { Id = 4, Name = "Phạm Thị Dung", Email = "dung.pham@email.com", Phone = "0934567890", Major = "Marketing", Age = 19 },
            new Student { Id = 5, Name = "Hoàng Văn Em", Email = "em.hoang@email.com", Phone = "0945678901", Major = "Công nghệ thông tin", Age = 23 }
        };

        private static int _nextId = 6;

        public static List<Student> GetAll()
        {
            return _students;
        }

        public static Student? GetById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public static void Add(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
        }

        public static void Update(Student student)
        {
            var existing = GetById(student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Email = student.Email;
                existing.Phone = student.Phone;
                existing.Major = student.Major;
                existing.Age = student.Age;
            }
        }

        public static void Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _students.Remove(student);
            }
        }

        public static List<Student> Search(string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return _students;
            }

            keyword = keyword.Trim().ToLower();
            return _students.Where(s =>
                s.Name.ToLower().Contains(keyword) ||
                s.Email.ToLower().Contains(keyword) ||
                s.Phone.Contains(keyword) ||
                s.Major.ToLower().Contains(keyword)
            ).ToList();
        }
    }
}
