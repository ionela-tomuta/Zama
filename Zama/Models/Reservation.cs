using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Zama.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; }
        public string SpecialRequests { get; set; }
        public List<string> DietaryRequirements { get; set; }
        public bool IsAnniversary { get; set; }
        public string OccasionType { get; set; } // "Birthday", "Anniversary", "Business"

        public User User { get; set; }
        public Table Table { get; set; }
    }
}
