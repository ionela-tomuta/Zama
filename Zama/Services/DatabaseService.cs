<<<<<<< HEAD
﻿using SQLite;
using Zama.Models;
using Microsoft.Maui.Storage;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite; 
using Zama.Models;
using Microsoft.Maui.Storage;
using Zama.Models;
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1

namespace Zama.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task InitializeAsync()
        {
<<<<<<< HEAD
            if (_database != null) return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "restaurant.db");
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Table>();
            await _database.CreateTableAsync<Models.MenuItem>();  // Specificarea namespace-ului complet
=======
            if (_database != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "restaurant.db");
            _database = new SQLiteAsyncConnection(databasePath);

            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Table>();
            await _database.CreateTableAsync<Models.MenuItem>();  // Specificăm namespace-ul complet
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
            await _database.CreateTableAsync<Reservation>();
            await _database.CreateTableAsync<Order>();
            await _database.CreateTableAsync<OrderItem>();
            await _database.CreateTableAsync<Review>();
            await _database.CreateTableAsync<Notification>();
            await _database.CreateTableAsync<Badge>();
        }

<<<<<<< HEAD
        public SQLiteAsyncConnection GetConnection() => _database;

        public Task<User> GetUserAsync(string email, string password) =>
            _database.Table<User>()
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();

        public Task<User> GetUserByIdAsync(int id) =>
            _database.Table<User>()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

        public Task<int> SaveUserAsync(User user) =>
            _database.InsertAsync(user);

        public Task<List<Reservation>> GetUserReservationsAsync(int userId) =>
            _database.Table<Reservation>()
                .Where(r => r.UserId == userId)
                .ToListAsync();

        public Task<int> SaveReservationAsync(Reservation reservation) =>
            _database.InsertAsync(reservation);
=======
        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
    }
}