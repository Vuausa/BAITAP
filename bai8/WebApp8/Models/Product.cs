namespace WebApp8.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string> ImagePaths { get; set; } = new();
    }
}
