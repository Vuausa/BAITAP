using System.ComponentModel.DataAnnotations;

namespace WebApp2.Models
{
    public class Sach
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Tên sách")]
        public string Ten { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        [Display(Name = "Giá")]
        public decimal Gia { get; set; }
    }
}
