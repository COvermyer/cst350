using System.ComponentModel.DataAnnotations;

namespace ProductsApp.Models
{
    public class ProductViewModel
    {
        public string? Id { get; set; } // Unique identifier for the product

        [Required]
        public string Name { get; set; } // Name of the product

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; } // Price of the product

        [Display(Name = "Price")]
        public string? FormattedPrice { get; set; } // formatted price for display

        [Required]
        public string? Description { get; set; } // Description of the product

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } // Timestamp when the product was created - used for data entry

        [Display(Name = "Created At")]
        public string? FormattedDateTime { get; set; } // formatted date time for display

        public string ImageURL { get; set; } // URL of the product image - allowed to be null

        public IFormFile? ImageFile { get; set; } // Image file for upload - allowed to be null

        public decimal EstimatedTax { get; set; } // Estimated tax for the product

        [Display(Name = "Estimated Tax")]
        public string? FormattedEstimatedTax { get; set; } // Formatted estimated tax for display
    }
}
