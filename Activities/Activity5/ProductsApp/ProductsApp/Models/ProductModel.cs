namespace ProductsApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; } // Unique identifier for the product
        public string Name { get; set; } // Name of the product
        public decimal Price { get; set; } // Price of the product
        public string Description { get; set; } // Description of the product
        public DateTime CreatedAt { get; set; } // Timestamp when the product was created
        public string ImageURL { get; set; } // URL of the product image

        public ProductModel(int id, string name, decimal price, string description, DateTime createdAt, string imageURL)
        { // parameterized constructor
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            CreatedAt = createdAt;
            ImageURL = imageURL;
        }

        public ProductModel()
        { // Default constructor
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ProductModel);
        }

        public bool Equals(ProductModel? other)
        {
            return other != null &&
                   Id == other.Id &&
                   Name == other.Name &&
                   Price == other.Price &&
                   Description == other.Description &&
                   CreatedAt == other.CreatedAt &&
                   ImageURL == other.ImageURL;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
