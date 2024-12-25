using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SQLite;
using Zama.Models;

namespace Zama.Services
{
    public class AuthService
    {
        private readonly DatabaseService _databaseService;
        private readonly ISecureStorage _secureStorage;

        public AuthService(DatabaseService databaseService, ISecureStorage secureStorage)
        {
            _databaseService = databaseService;
            _secureStorage = secureStorage;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var db = _databaseService.GetConnection();
            var hashedPassword = HashPassword(password);

            var user = await db.Table<User>()
                .Where(u => u.Email == email && u.Password == hashedPassword)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                await _secureStorage.SetAsync("userId", user.Id.ToString());
                await _secureStorage.SetAsync("userEmail", user.Email);
            }

            return user;
        }

        private string HashPassword(string password)
        {
            // Implement proper password hashing
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
