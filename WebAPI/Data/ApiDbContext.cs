using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Zama.API.Models;
using Zama.Models;
using Zama.ViewModels;
namespace Zama.API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Zama.Models.Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Password).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasMany(e => e.Reservations)
                      .WithOne(r => r.User)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity< Zama.Models.Table>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Number).IsRequired();
                entity.Property(e => e.Capacity).IsRequired();
                entity.Property(e => e.Location).IsRequired().HasMaxLength(50);
                entity.HasMany(e => e.Reservations)
                      .WithOne(r => r.Table)
                      .HasForeignKey(r => r.TableId);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ContactPhone).IsRequired();
                entity.Property(e => e.SpecialRequests).HasMaxLength(500);
            });

            /* modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).HasPrecision(10, 2);
                entity.Property(e => e.SpecialPrice).HasPrecision(10, 2);
            }); 
            */
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Message).IsRequired();
                entity.HasOne(n => n.User)
                      .WithMany()
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}