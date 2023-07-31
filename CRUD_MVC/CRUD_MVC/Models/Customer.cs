using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
   
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name should be between 2 and 100 characters.")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Customer email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? CustomerEmailID { get; set; }

        [Required(ErrorMessage = "Customer phone number is required.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Invalid phone number.")]
        public long CustomerPhoneNo { get; set; }

        [Required(ErrorMessage = "Customer password is required.")]
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters long.")]
        public string? CustomerPassword { get; set; }

        [Required(ErrorMessage = "Customer role is required.")]
        public string? CustomerRole { get; set; }
        [NotMapped]
        public string? Product { get; set; }

    }
}
