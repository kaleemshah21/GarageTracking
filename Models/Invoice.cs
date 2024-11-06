namespace GarageTracking.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int BookingID { get; set; }
        public DateTime InvoiceDate { get; set; }


        public Booking Booking { get; set; }
    }
}
