using GarageTracking.Models;

namespace GarageTracking.Data
{
    public class DbInitializer
    {
        public static void Initialize(TrackingContext context)
        {
            // Look for any customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer { FirstName = "John", LastName = "Doe", Street = "123 Elm St", PostCode = "A1B 2C3", Email = "john.doe@example.com", Phone = "123-456-7890" },
                new Customer { FirstName = "Jane", LastName = "Smith", Street = "456 Oak St", PostCode = "B2C 3D4", Email = "jane.smith@example.com", Phone = "234-567-8901" },
                new Customer { FirstName = "Michael", LastName = "Brown", Street = "789 Pine St", PostCode = "C3D 4E5", Email = "michael.brown@example.com", Phone = "345-678-9012" },
                new Customer { FirstName = "Emily", LastName = "Johnson", Street = "101 Maple St", PostCode = "D4E 5F6", Email = "emily.johnson@example.com", Phone = "456-789-0123" },
                new Customer { FirstName = "David", LastName = "Wilson", Street = "202 Birch St", PostCode = "E5F 6G7", Email = "david.wilson@example.com", Phone = "567-890-1234" }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Seed Vehicles
            var vehicles = new Vehicle[]
            {
                new Vehicle { CustomerID = customers[0].CustomerID, CarMake = "Toyota", CarModel = "Corolla", Registration = "ABC123" },
                new Vehicle { CustomerID = customers[1].CustomerID, CarMake = "Honda", CarModel = "Civic", Registration = "DEF456" },
                new Vehicle { CustomerID = customers[2].CustomerID, CarMake = "Ford", CarModel = "Focus", Registration = "GHI789" },
                new Vehicle { CustomerID = customers[3].CustomerID, CarMake = "Chevrolet", CarModel = "Malibu", Registration = "JKL012" },
                new Vehicle { CustomerID = customers[4].CustomerID, CarMake = "Nissan", CarModel = "Altima", Registration = "MNO345" }
            };

            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();

            // Seed Bookings
            var bookings = new Booking[]
            {
                new Booking { VehicleID = vehicles[0].VehicleId, ServiceType = "Oil Change", ServiceDate = DateTime.Now.AddDays(1), Price = 29.99m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[1].VehicleId, ServiceType = "Tire Rotation", ServiceDate = DateTime.Now.AddDays(2), Price = 49.99m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[2].VehicleId, ServiceType = "Brake Inspection", ServiceDate = DateTime.Now.AddDays(3), Price = 39.99m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[3].VehicleId, ServiceType = "Battery Replacement", ServiceDate = DateTime.Now.AddDays(4), Price = 99.99m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[4].VehicleId, ServiceType = "Full Service", ServiceDate = DateTime.Now.AddDays(5), Price = 199.99m, Status = Booking.BookingStatus.Waiting }
            };

            context.Bookings.AddRange(bookings);
            context.SaveChanges();

            // Seed Invoices
            var invoices = new Invoice[]
            {
                new Invoice { BookingID = bookings[0].BookingID, InvoiceDate = DateTime.Now },
                new Invoice { BookingID = bookings[1].BookingID, InvoiceDate = DateTime.Now },
                new Invoice { BookingID = bookings[2].BookingID, InvoiceDate = DateTime.Now },
                new Invoice { BookingID = bookings[3].BookingID, InvoiceDate = DateTime.Now },
                new Invoice { BookingID = bookings[4].BookingID, InvoiceDate = DateTime.Now }
            };




        }
    }
}
