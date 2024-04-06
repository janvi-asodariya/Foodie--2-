namespace Foodie.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set;}
        public string Name { get; set;}
        public string ImageUrl { get; set;}
        public int IsActive { get; set;}
        public DateTime CreatedDate { get; set;}
        public IFormFile Image { get; set;}
    }
}
