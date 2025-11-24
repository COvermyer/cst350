using ProductsApp.Models;

namespace ProductsApp.Services
{
    public interface IProductDAO
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductModel?> GetProductById(int id);
        Task<int> AddProduct(ProductModel product);
        Task UpdateProduct(ProductModel product);
        Task DeleteProduct(ProductModel product);
        Task<IEnumerable<ProductModel>> SearchForProductsByName(string searchTerm);
        Task<IEnumerable<ProductModel>> SearchForProductsByDescription(string searchTerm);
    }
}
