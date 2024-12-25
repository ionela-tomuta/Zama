using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zama.Models;

namespace Zama.Services
{
    public class ReservationService : IDataService<Reservation>
    {
        private readonly DatabaseService _databaseService;
        private readonly NotificationService _notificationService;

        public ReservationService(DatabaseService databaseService, NotificationService notificationService)
        {
            _databaseService = databaseService;
            _notificationService = notificationService;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            var db = _databaseService.GetConnection();
            return await db.Table<Reservation>().ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            var db = _databaseService.GetConnection();
            return await db.Table<Reservation>()
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddAsync(Reservation reservation)
        {
            var db = _databaseService.GetConnection();
            var result = await db.InsertAsync(reservation);
            if (result > 0)
            {
                await _notificationService.SendReservationConfirmation(reservation);
            }
            return result;
        }

        public async Task<bool> UpdateAsync(Reservation reservation)
        {
            var db = _databaseService.GetConnection();
            var result = await db.UpdateAsync(reservation);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var db = _databaseService.GetConnection();
            var result = await db.DeleteAsync<Reservation>(id);
            return result > 0;
        }
    }
}