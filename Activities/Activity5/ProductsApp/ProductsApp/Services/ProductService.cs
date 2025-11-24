using ProductsApp.Models;

namespace ProductsApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDAO _productDAO;
        private readonly IProductMapper _productMapper;

        public ProductService(IProductDAO productDAO, IProductMapper productMapper)
        {
            _productDAO = productDAO;
            _productMapper = productMapper;
        }

        public async Task<int> AddProduct(ProductViewModel product)
        {
            var productDTO = _productMapper.ToDTO(product);
            var productModel = _productMapper.ToModel(productDTO);
            return await _productDAO.AddProduct(productModel);
        }

        public async Task DeleteProduct(string Id)
        {
            int productId = int.Parse(Id);
            var productModel = await _productDAO.GetProductById(productId);
            if (productModel != null)
                await _productDAO.DeleteProduct(productModel);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            IEnumerable<ProductModel> productModels = await _productDAO.GetAllProducts(); // fetch from DB

            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            foreach (ProductModel productModel in productModels)
            {
                ProductDTO productDTO = _productMapper.ToDTO(productModel);
                ProductViewModel productViewModel = _productMapper.ToViewModel(productDTO);
                productViewModels.Add(productViewModel);
            }
            return productViewModels;
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            ProductModel productModel = await _productDAO.GetProductById(id); // fetch from DB
            
            // convert with data calculations
            ProductDTO productDTO = _productMapper.ToDTO(productModel);
            return _productMapper.ToViewModel(productDTO);
        }

        public async Task<IEnumerable<ProductViewModel>> SearchForProducts(SearchFor searchTerm)
        {
            List<ProductViewModel> matchedProducts = new List<ProductViewModel>();

            if (searchTerm.InTitle)
            { // Title matches
                IEnumerable<ProductModel> results = _productDAO.SearchForProductsByName(searchTerm.SearchTerm).Result.ToList();
                foreach (ProductModel productModel in results)
                {
                    ProductDTO productDTO = _productMapper.ToDTO(productModel);
                    ProductViewModel productViewModel = _productMapper.ToViewModel(productDTO);

                    if (!matchedProducts.Contains(productViewModel))
                        matchedProducts.Add(productViewModel);
                }
            }

            if (searchTerm.InDescription)
            {
                IEnumerable<ProductModel> results = _productDAO.SearchForProductsByDescription(searchTerm.SearchTerm).Result.ToList();
                foreach (ProductModel productModel in results)
                {
                    ProductDTO productDTO = _productMapper.ToDTO(productModel);
                    ProductViewModel productViewModel = _productMapper.ToViewModel(productDTO);
                    if (!matchedProducts.Contains(productViewModel))
                        matchedProducts.Add(productViewModel);
                }
            }
            return matchedProducts;
        }

        public async Task UpdateProduct(ProductViewModel product)
        {
            var productDTO = _productMapper.ToDTO(product);
            var productModel = _productMapper.ToModel(productDTO);
            await _productDAO.UpdateProduct(productModel);
        }
    }
}
