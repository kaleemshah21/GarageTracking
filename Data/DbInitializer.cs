using GarageTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

           
            var customers = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe", Street = "123 Elm St", PostCode = "AB1 2CD", Email = "john.doe@example.com", Phone = "123-456-7890" },
                new Customer { FirstName = "Jane", LastName = "Smith", Street = "456 Oak Ave", PostCode = "CD2 3EF", Email = "jane.smith@example.com", Phone = "234-567-8901" },
                new Customer { FirstName = "Michael", LastName = "Brown", Street = "789 Pine St", PostCode = "EF3 4GH", Email = "michael.brown@example.com", Phone = "345-678-9012" },
                new Customer { FirstName = "Emily", LastName = "Johnson", Street = "101 Maple Dr", PostCode = "GH4 5IJ", Email = "emily.johnson@example.com", Phone = "456-789-0123" },
                new Customer { FirstName = "David", LastName = "Wilson", Street = "202 Birch Ln", PostCode = "IJ5 6KL", Email = "david.wilson@example.com", Phone = "567-890-1234" },
                new Customer { FirstName = "Sarah", LastName = "Taylor", Street = "303 Cedar Blvd", PostCode = "KL6 7MN", Email = "sarah.taylor@example.com", Phone = "678-901-2345" },
                new Customer { FirstName = "Daniel", LastName = "Anderson", Street = "404 Redwood St", PostCode = "MN7 8OP", Email = "daniel.anderson@example.com", Phone = "789-012-3456" },
                new Customer { FirstName = "Sophia", LastName = "Thomas", Street = "505 Cherry Ct", PostCode = "OP8 9QR", Email = "sophia.thomas@example.com", Phone = "890-123-4567" },
                new Customer { FirstName = "Matthew", LastName = "Jackson", Street = "606 Walnut Dr", PostCode = "QR9 0ST", Email = "matthew.jackson@example.com", Phone = "901-234-5678" },
                new Customer { FirstName = "Olivia", LastName = "White", Street = "707 Birchwood Rd", PostCode = "ST0 1UV", Email = "olivia.white@example.com", Phone = "012-345-6789" },
                new Customer { FirstName = "Lucas", LastName = "Martinez", Street = "808 Spruce Ave", PostCode = "UV1 2WX", Email = "lucas.martinez@example.com", Phone = "123-456-7890" },
                new Customer { FirstName = "Chloe", LastName = "Garcia", Street = "909 Willow Ln", PostCode = "WX2 3YZ", Email = "chloe.garcia@example.com", Phone = "234-567-8901" },
                new Customer { FirstName = "James", LastName = "Hernandez", Street = "1010 Palm St", PostCode = "YZ3 4AB", Email = "james.hernandez@example.com", Phone = "345-678-9012" },
                new Customer { FirstName = "Mia", LastName = "Lopez", Street = "1111 Oakwood Blvd", PostCode = "AB4 5CD", Email = "mia.lopez@example.com", Phone = "456-789-0123" },
                new Customer { FirstName = "Benjamin", LastName = "Gonzalez", Street = "1212 Cedar Dr", PostCode = "CD5 6EF", Email = "benjamin.gonzalez@example.com", Phone = "567-890-1234" },
                new Customer { FirstName = "Ava", LastName = "King", Street = "1313 Maple Ave", PostCode = "EF6 7GH", Email = "ava.king@example.com", Phone = "678-901-2345" },
                new Customer { FirstName = "Ethan", LastName = "Scott", Street = "1414 Pine Blvd", PostCode = "GH7 8IJ", Email = "ethan.scott@example.com", Phone = "789-012-3456" },
                new Customer { FirstName = "Isabella", LastName = "Adams", Street = "1515 Redwood Rd", PostCode = "IJ8 9KL", Email = "isabella.adams@example.com", Phone = "890-123-4567" },
                new Customer { FirstName = "Alexander", LastName = "Clark", Street = "1616 Birch St", PostCode = "KL9 0MN", Email = "alexander.clark@example.com", Phone = "901-234-5678" },
                new Customer { FirstName = "Charlotte", LastName = "Lewis", Street = "1717 Cedar Ct", PostCode = "MN0 1OP", Email = "charlotte.lewis@example.com", Phone = "012-345-6789" }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            
            var vehicles = new List<Vehicle>
            {
                new Vehicle { CustomerID = customers[0].CustomerID, CarMake = "Toyota", CarModel = "Corolla", Registration = "REG1001" },
                new Vehicle { CustomerID = customers[1].CustomerID, CarMake = "Honda", CarModel = "Civic", Registration = "REG1002" },
                new Vehicle { CustomerID = customers[2].CustomerID, CarMake = "Ford", CarModel = "Focus", Registration = "REG1003" },
                new Vehicle { CustomerID = customers[3].CustomerID, CarMake = "Chevrolet", CarModel = "Malibu", Registration = "REG1004" },
                new Vehicle { CustomerID = customers[4].CustomerID, CarMake = "Nissan", CarModel = "Altima", Registration = "REG1005" },
                new Vehicle { CustomerID = customers[5].CustomerID, CarMake = "BMW", CarModel = "3 Series", Registration = "REG1006" },
                new Vehicle { CustomerID = customers[6].CustomerID, CarMake = "Mercedes-Benz", CarModel = "C-Class", Registration = "REG1007" },
                new Vehicle { CustomerID = customers[7].CustomerID, CarMake = "Audi", CarModel = "A4", Registration = "REG1008" },
                new Vehicle { CustomerID = customers[8].CustomerID, CarMake = "Tesla", CarModel = "Model 3", Registration = "REG1009" },
                new Vehicle { CustomerID = customers[9].CustomerID, CarMake = "Volkswagen", CarModel = "Golf", Registration = "REG1010" },
                new Vehicle { CustomerID = customers[10].CustomerID, CarMake = "Kia", CarModel = "Optima", Registration = "REG1011" },
                new Vehicle { CustomerID = customers[11].CustomerID, CarMake = "Hyundai", CarModel = "Elantra", Registration = "REG1012" },
                new Vehicle { CustomerID = customers[12].CustomerID, CarMake = "Mazda", CarModel = "CX-5", Registration = "REG1013" },
                new Vehicle { CustomerID = customers[13].CustomerID, CarMake = "Subaru", CarModel = "Outback", Registration = "REG1014" },
                new Vehicle { CustomerID = customers[14].CustomerID, CarMake = "Jeep", CarModel = "Cherokee", Registration = "REG1015" },
                new Vehicle { CustomerID = customers[15].CustomerID, CarMake = "Chrysler", CarModel = "Pacifica", Registration = "REG1016" },
                new Vehicle { CustomerID = customers[16].CustomerID, CarMake = "Ford", CarModel = "F-150", Registration = "REG1017" },
                new Vehicle { CustomerID = customers[17].CustomerID, CarMake = "Toyota", CarModel = "RAV4", Registration = "REG1018" },
                new Vehicle { CustomerID = customers[18].CustomerID, CarMake = "Honda", CarModel = "Pilot", Registration = "REG1019" },
                new Vehicle { CustomerID = customers[19].CustomerID, CarMake = "Chevrolet", CarModel = "Traverse", Registration = "REG1020" }
            };

            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();

            
            var bookings = new List<Booking>
            {
                new Booking { VehicleID = vehicles[0].VehicleId, ServiceType = "Oil Change", ServiceDate = DateTime.Now.AddDays(5), Price = 50.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[1].VehicleId, ServiceType = "Tire Rotation", ServiceDate = DateTime.Now.AddDays(10), Price = 75.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[2].VehicleId, ServiceType = "Brake Inspection", ServiceDate = DateTime.Now.AddDays(3), Price = 40.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[3].VehicleId, ServiceType = "Engine Check", ServiceDate = DateTime.Now.AddDays(8), Price = 120.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[4].VehicleId, ServiceType = "Battery Test", ServiceDate = DateTime.Now.AddDays(12), Price = 30.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[5].VehicleId, ServiceType = "Fluid Flush", ServiceDate = DateTime.Now.AddDays(15), Price = 95.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[6].VehicleId, ServiceType = "Tire Repair", ServiceDate = DateTime.Now.AddDays(7), Price = 50.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[7].VehicleId, ServiceType = "Alignment", ServiceDate = DateTime.Now.AddDays(14), Price = 85.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[8].VehicleId, ServiceType = "Oil Change", ServiceDate = DateTime.Now.AddDays(2), Price = 50.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[9].VehicleId, ServiceType = "AC Check", ServiceDate = DateTime.Now.AddDays(6), Price = 70.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[10].VehicleId, ServiceType = "Windshield Repair", ServiceDate = DateTime.Now.AddDays(9), Price = 35.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[11].VehicleId, ServiceType = "Tire Rotation", ServiceDate = DateTime.Now.AddDays(13), Price = 60.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[12].VehicleId, ServiceType = "Brake Pads Replacement", ServiceDate = DateTime.Now.AddDays(16), Price = 110.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[13].VehicleId, ServiceType = "Transmission Check", ServiceDate = DateTime.Now.AddDays(18), Price = 150.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[14].VehicleId, ServiceType = "Oil Change", ServiceDate = DateTime.Now.AddDays(4), Price = 50.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[15].VehicleId, ServiceType = "Battery Replacement", ServiceDate = DateTime.Now.AddDays(20), Price = 100.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[16].VehicleId, ServiceType = "Cooling System Flush", ServiceDate = DateTime.Now.AddDays(1), Price = 90.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[17].VehicleId, ServiceType = "Exhaust Repair", ServiceDate = DateTime.Now.AddDays(11), Price = 80.00m, Status = Booking.BookingStatus.InProgress },
                new Booking { VehicleID = vehicles[18].VehicleId, ServiceType = "Brake Fluid Change", ServiceDate = DateTime.Now.AddDays(17), Price = 60.00m, Status = Booking.BookingStatus.Waiting },
                new Booking { VehicleID = vehicles[19].VehicleId, ServiceType = "Timing Belt Replacement", ServiceDate = DateTime.Now.AddDays(19), Price = 200.00m, Status = Booking.BookingStatus.InProgress }
            };

            context.Bookings.AddRange(bookings);
            context.SaveChanges();

            
            var users = new List<User>
            {
                new User { Username = "admin", Password = "admin123", Role = "Admin" },
                new User { Username = "employee1", Password = "emp123", Role = "Employee" },
                new User { Username = "employee2", Password = "emp123", Role = "Employee" },
                new User { Username = "employee3", Password = "emp123", Role = "Employee" },
                new User { Username = "employee4", Password = "emp123", Role = "Employee" },
                new User { Username = "employee5", Password = "emp123", Role = "Employee" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
