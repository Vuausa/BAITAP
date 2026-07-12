using System.ComponentModel.DataAnnotations;

namespace buoi6.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sinh viên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngành học không được để trống")]
        [Display(Name = "Ngành học")]
        public string Major { get; set; } = string.Empty;

        [Range(18, 60, ErrorMessage = "Tuổi phải từ 18 đến 60")]
        [Display(Name = "Tuổi")]
        public int Age { get; set; }
    }
}
