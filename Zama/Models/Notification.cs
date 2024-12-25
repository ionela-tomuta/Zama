using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zama.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }

        [Ignore]
        public User User { get; set; }
    }
}
