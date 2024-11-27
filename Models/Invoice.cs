using System;
using System.ComponentModel.DataAnnotations;

namespace GarageTracking.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        [Required(ErrorMessage = "Booking ID is required.")]
        public int BookingID { get; set; }

        [Required(ErrorMessage = "Invoice Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        public Booking Booking { get; set; }
    }
}
