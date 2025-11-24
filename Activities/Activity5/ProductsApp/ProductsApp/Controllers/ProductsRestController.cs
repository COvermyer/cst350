using Microsoft.AspNetCore.Mvc;
using ProductsApp.Models;
using ProductsApp.Services;
using System.Diagnostics;

namespace ProductsApp.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsRestController : ControllerBase
    {
        private readonly ILogger<ProductsRestController> _logger;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ProductsRestController(ILogger<ProductsRestController> logger, IProductService productService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<ProductViewModel> products =  await _productService.GetAllProducts();
            return Ok(products); // OK status code is 200
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(); // 404
            }
            return Ok(product); // 200
        }


        // EXAMPLE: api/v1/products/search?searchTerm=phone&inTitle=true&inDescription=false
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> SearchForProducts([FromQuery] string searchTerm, [FromQuery] bool inTitle, [FromQuery] bool inDescription)
        {
            SearchFor searchFor = new SearchFor
            {
                SearchTerm = searchTerm,
                InTitle = inTitle,
                InDescription = inDescription
            };

            var searchResults = await _productService.SearchForProducts(searchFor);
            if (searchResults == null || !searchResults.Any())
            {
                return NotFound("No products found matching search criteria."); // 404
            }
            return Ok(searchResults); // 200
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                { // file was uploded
                    product.ImageURL = await PerformFileUpload(product);
                }

                // add product to db
                await _productService.AddProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product); // 201
            }
            else
            {
                return BadRequest(ModelState); // 400
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            // ensure the product exist befopre trying to delete it
            var product = await _productService.GetProductById(int.Parse(id));
            if (product == null)
                return NotFound(); // 404

            // product exists, make it stop existing
            await _productService.DeleteProduct(id);
            return NoContent(); // 204
        }

        private async Task<string> PerformFileUpload(ProductViewModel product)
        {
            string uniqueFileName = "";
            //check if the user has uploaded an image. Perform steps to save image to server
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(product);
                return NoContent();
            }
            else
            {
                return BadRequest(ModelState); // 400
            }
        }
    }
}
