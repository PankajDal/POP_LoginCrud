using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models
{
    public class UserLogin
    {


        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? CustomerEmailID { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? CustostomerPassword { get; set; }
    }
}
