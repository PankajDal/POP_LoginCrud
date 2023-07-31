using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models
{
    public class Product
    {
        [Required]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name should be between 2 and 100 characters.")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product price must be a positive value.")]
        public int ProductPrice { get; set; }

        [Required(ErrorMessage = "Valid date is required.")]
      
        public DateTime ValidDate { get; set; }
        public int Quantity { get; set; }
        public byte[]? ProductImage { get; set; }


    }


}
