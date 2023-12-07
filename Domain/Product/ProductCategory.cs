namespace Domain.Product
{
    public class ProductCategory
    {
        public int DatabaseId { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public CategoreyImage CategoryImage { get; set; }
    }
}
