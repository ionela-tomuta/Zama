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
            await _database.CreateTableAsync<Zama.Models.MenuItem>();
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

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<Reservation>> GetUserReservationsAsync(int userId)
        {
            return await _database.Table<Reservation>()
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                return await _database.UpdateAsync(user);
            }
            return await _database.InsertAsync(user);
        }

        public async Task<User> GetUserAsync(string email, string password)
        {
            return await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<int> SaveReservationAsync(Reservation reservation)
        {
            if (reservation.Id != 0)
            {
                return await _database.UpdateAsync(reservation);
            }
            return await _database.InsertAsync(reservation);
        }

        public async Task<List<Zama.Models.MenuItem>> GetMenuItemsAsync()
        {
            return await _database.Table<Zama.Models.MenuItem>().ToListAsync();
        }

        public async Task<int> SaveOrderAsync(Order order)
        {
            if (order.Id != 0)
            {
                return await _database.UpdateAsync(order);
            }
            return await _database.InsertAsync(order);
        }

        public async Task<int> SaveOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem.Id != 0)
            {
                return await _database.UpdateAsync(orderItem);
            }
            return await _database.InsertAsync(orderItem);
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _database.Table<Order>()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> SaveReviewAsync(Review review)
        {
            if (review.Id != 0)
            {
                return await _database.UpdateAsync(review);
            }
            return await _database.InsertAsync(review);
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            return await _database.Table<Review>().ToListAsync();
        }

        public async Task<int> SaveNotificationAsync(Notification notification)
        {
            if (notification.Id != 0)
            {
                return await _database.UpdateAsync(notification);
            }
            return await _database.InsertAsync(notification);
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _database.Table<Notification>()
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> SaveBadgeAsync(Badge badge)
        {
            if (badge.Id != 0)
            {
                return await _database.UpdateAsync(badge);
            }
            return await _database.InsertAsync(badge);
        }

        public async Task<List<Badge>> GetUserBadgesAsync(int userId)
        {
            return await _database.Table<Badge>()
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}