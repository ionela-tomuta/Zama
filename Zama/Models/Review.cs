using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zama.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MenuItemId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public List<string> Tags { get; set; } // "Tasty", "Good Value", "Large Portion"
        public bool IsVerifiedPurchase { get; set; }
    }
}
