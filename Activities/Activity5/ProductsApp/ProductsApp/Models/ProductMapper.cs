namespace ProductsApp.Models
{
    public class ProductMapper : IProductMapper
    {
        public string CurrencyFormat { get; set; } = "C";
        public string DateFormat { get; set; } = "D";
        public decimal TaxRate { get; set; } = 0.08M;

        public ProductMapper(string currency, string dateFormat, decimal taxRate)
        {
            CurrencyFormat = currency;
            DateFormat = dateFormat;
            TaxRate = taxRate;
        }

        public ProductDTO ToDTO(ProductModel model)
        {
            return new ProductDTO
            {
                Id = model.Id.ToString(),
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                CreatedAt = model.CreatedAt,
                ImageURL = model.ImageURL,
                EstimatedTax = model.Price * TaxRate,
                FormattedPrice = model.Price.ToString(CurrencyFormat),
                FormattedDateTime = model.CreatedAt.ToString(DateFormat),
                FormattedEstimatedTax = (model.Price * TaxRate).ToString(CurrencyFormat)
            };
        }

        public ProductDTO ToDTO(ProductViewModel viewModel)
        {
            if (viewModel.Id == null)
                return new ProductDTO
                { // creating a new product, no ID yet
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    CreatedAt = viewModel.CreatedAt,
                    ImageURL = viewModel.ImageURL,
                    EstimatedTax = viewModel.Price * TaxRate,
                    FormattedPrice = viewModel.Price.ToString(CurrencyFormat),
                    FormattedDateTime = viewModel.CreatedAt.ToString(DateFormat),
                    FormattedEstimatedTax = (viewModel.Price * TaxRate).ToString(CurrencyFormat)
                };

            // if an id is set, it is an existing product.
            return new ProductDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Price = viewModel.Price,
                Description = viewModel.Description,
                CreatedAt = viewModel.CreatedAt,
                ImageURL = viewModel.ImageURL,
                EstimatedTax = viewModel.Price * TaxRate,
                FormattedPrice = viewModel.Price.ToString(CurrencyFormat),
                FormattedDateTime = viewModel.CreatedAt.ToString(DateFormat),
                FormattedEstimatedTax = (viewModel.Price * TaxRate).ToString(CurrencyFormat)
            };
        }

        public ProductModel ToModel(ProductDTO dto)
        {
            if (dto.Id == null)
                return new ProductModel
                { 
                    // comes from a "Create" operation, so no ID yet. Wil be assigned by DB
                    Name = dto.Name,
                    Price = dto.Price,
                    Description = dto.Description,
                    CreatedAt = dto.CreatedAt ?? DateTime.Now,
                    ImageURL = dto.ImageURL
                };

            // if Id is set, then it is an existing product
            return new ProductModel
            {
                Id = int.Parse(dto.Id), // convert string Id to int
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt ?? DateTime.Now,
                ImageURL = dto.ImageURL
            };
        }

        public ProductViewModel ToViewModel(ProductDTO dto)
        {
            return new ProductViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt ?? DateTime.Now,
                ImageURL = dto.ImageURL ?? string.Empty,
                EstimatedTax = dto.EstimatedTax,
                FormattedPrice = dto.FormattedPrice,
                FormattedDateTime = dto.FormattedDateTime,
                FormattedEstimatedTax = dto.FormattedEstimatedTax
            };
        }
    }
}
