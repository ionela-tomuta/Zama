namespace Zama.API.Models.Requests
{
    public class CreateMenuItemRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<string> Allergens { get; set; }
        public List<string> DietaryLabels { get; set; }
        public string ImageUrl { get; set; }
        public int PrepTimeMinutes { get; set; }
    }

    public class UpdateMenuItemRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsAvailable { get; set; }
        public List<string>? Allergens { get; set; }
        public List<string>? DietaryLabels { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsSpecialOfTheDay { get; set; }
        public decimal? SpecialPrice { get; set; }
    }

    public class MenuItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Allergens { get; set; }
        public List<string> DietaryLabels { get; set; }
        public string ImageUrl { get; set; }
        public double AverageRating { get; set; }
        public int TimesOrdered { get; set; }
        public bool IsSpecialOfTheDay { get; set; }
        public decimal? SpecialPrice { get; set; }
        public int PrepTimeMinutes { get; set; }
    }
}