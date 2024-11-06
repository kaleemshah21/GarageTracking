namespace GarageTracking.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int CustomerID { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string Registration { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
