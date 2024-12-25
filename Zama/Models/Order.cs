using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Zama.Models
{
    [Table("Orders")]
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int EstimatedWaitTime { get; set; }
        public string SpecialInstructions { get; set; }
        public int? WaiterStaffId { get; set; }
        public bool IsRated { get; set; }
        public int? LoyaltyPointsEarned { get; set; }

        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
