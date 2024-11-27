using System;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Vehicle ID is required.")]
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "Service Type is required.")]
        [StringLength(100, ErrorMessage = "Service Type cannot exceed 100 characters.")]
        public string ServiceType { get; set; }

        [Required(ErrorMessage = "Service Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [Display(Name = "Service Date")]
        public DateTime ServiceDate { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10,000.")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid price format.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public BookingStatus Status { get; set; }

        public Vehicle Vehicle { get; set; }
        public Invoice Invoice { get; set; }
    }
}
