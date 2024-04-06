namespace Foodie.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }    
        public int CategoryId { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CategoryName { get; set; }
        public IFormFile Image { get; set; }

        public CategoryModel Categories { get; set; }
    }
}
