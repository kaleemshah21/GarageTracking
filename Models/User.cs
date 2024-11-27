using System.ComponentModel.DataAnnotations;

namespace GarageTracking.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(20, ErrorMessage = "Role cannot exceed 20 characters.")]
        public string Role { get; set; }
    }
}
