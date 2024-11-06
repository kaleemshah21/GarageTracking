namespace GarageTracking.Models
{
    public class Booking
    {
        public enum BookingStatus
        {
            Waiting,
            InProgress,
            Done
        }
        public int BookingID { get; set; }
        public int? VehicleID { get; set; }
        public string ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public Decimal Price { get; set; }
        public BookingStatus Status { get; set; }



        public Vehicle Vehicle { get; set; }
        public Invoice Invoice { get; set; }

    }
}
