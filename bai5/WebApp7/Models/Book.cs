using System.ComponentModel.DataAnnotations;

namespace WebApp7.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Giá sách là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal Price { get; set; }
    }
}
