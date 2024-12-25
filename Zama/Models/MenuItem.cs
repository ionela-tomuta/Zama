using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zama.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Allergens { get; set; }
        public List<string> DietaryLabels { get; set; } // "Vegan", "Vegetarian", "Gluten-Free"
        public string ImageUrl { get; set; }
        public double AverageRating { get; set; }
        public int TimesOrdered { get; set; }
        public bool IsSpecialOfTheDay { get; set; }
        public decimal? SpecialPrice { get; set; }
        public int PrepTimeMinutes { get; set; }
    }
}
