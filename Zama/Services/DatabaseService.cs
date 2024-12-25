using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite; 
using Zama.Models;
using Microsoft.Maui.Storage;
using Zama.Models;

namespace Zama.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task InitializeAsync()
        {
            if (_database != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "restaurant.db");
            _database = new SQLiteAsyncConnection(databasePath);

            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Table>();
            await _database.CreateTableAsync<Models.MenuItem>();  // Specificăm namespace-ul complet
            await _database.CreateTableAsync<Reservation>();
            await _database.CreateTableAsync<Order>();
            await _database.CreateTableAsync<OrderItem>();
            await _database.CreateTableAsync<Review>();
            await _database.CreateTableAsync<Notification>();
            await _database.CreateTableAsync<Badge>();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }
    }
}