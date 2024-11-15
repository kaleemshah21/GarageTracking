using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Models;

namespace GarageTracking.Data
{
    public class TrackingContext : DbContext
    {
        public TrackingContext (DbContextOptions<TrackingContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");



            // Customer and Vehicle relationship
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Vehicles)
                .WithOne(v => v.Customer)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            // Vehicle and Booking relationship
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Bookings)
                .WithOne(b => b.Vehicle)
                .OnDelete(DeleteBehavior.SetNull); // Set VehicleID to null when Vehicle is deleted

            // Booking and Invoice relationship
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Invoice)
                .WithOne(i => i.Booking)
                .HasForeignKey<Invoice>(i => i.BookingID)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Adjust based on your needs
        }
    }
}
