namespace ProductsApp.Models
{
    public class ProductDTO
    {
        public string? Id { get; set; } // Unique identifier for the product
        public string Name { get; set; } // Name of the product
        public decimal Price { get; set; } // Price of the product
        public string? FormattedPrice { get; set; } // formatted price for display
        public string? Description { get; set; } // Description of the product
        public DateTime? CreatedAt { get; set; } // Timestamp when the product was created
        public string? FormattedDateTime { get; set; } // formatted date time for display
        public string? ImageURL { get; set; } // URL of the product image
        public decimal EstimatedTax { get; set; } // Estimated tax for the product
        public string? FormattedEstimatedTax { get; set; } // Formatted estimated tax for display
    }
}
