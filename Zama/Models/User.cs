using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zama.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } // "Admin", "Customer", "Staff"
        public DateTime? DateOfBirth { get; set; }
        public int LoyaltyPoints { get; set; }
        public List<string> DietaryPreferences { get; set; }
        public List<string> Allergies { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Badge> Badges { get; set; }
    }

}
