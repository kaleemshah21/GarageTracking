using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GarageTracking.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Car Make is required.")]
        [StringLength(50, ErrorMessage = "Car Make cannot exceed 50 characters.")]
        public string CarMake { get; set; }

        [Required(ErrorMessage = "Car Model is required.")]
        [StringLength(50, ErrorMessage = "Car Model cannot exceed 50 characters.")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "Registration number is required.")]
        [StringLength(15, ErrorMessage = "Registration number cannot exceed 15 characters.")]
        public string Registration { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
