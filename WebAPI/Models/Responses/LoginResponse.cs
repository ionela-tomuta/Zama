using Zama.API.Models.DTOs;

namespace Zama.API.Models.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
        public DateTime Expiration { get; set; }
    }
}