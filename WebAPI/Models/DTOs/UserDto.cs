using Zama.API.Models.DTOs;

namespace Zama.API.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}