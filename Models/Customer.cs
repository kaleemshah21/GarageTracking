using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GarageTracking.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Street address cannot exceed 100 characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Post Code is required.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Post Code format.")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        // Read-only property for Full Name
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
