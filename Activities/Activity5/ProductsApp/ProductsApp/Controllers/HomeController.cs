using Microsoft.AspNetCore.Mvc;
using ProductsApp.Models;
using ProductsApp.Services;
using System.Diagnostics;

namespace ProductsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShowCreateProductForm()
        {
            ViewBag.TaxRate = decimal.Parse(_configuration["ProductMapper:TaxRate"]);
            ViewBag.Images = GetImageNames();

            var productViewModel = new ProductViewModel();
            productViewModel.CreatedAt = DateTime.Now;
            return View(productViewModel);
        }

        public async Task<IActionResult> ShowAllProductsGrid()
        {
            ViewBag.Images = GetImageNames();
            ViewBag.TaxRate = decimal.Parse(_configuration["ProductMapper:TaxRate"]);
            List<ProductViewModel> products = _productService.GetAllProducts().Result.ToList();
            return View(products);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                { // file was uploded
                    product.ImageURL = await PerformFileUpload(product);
                }

                // add product to db
                await _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Redirect to create form if incvalid
                ViewBag.TaxRate = decimal.Parse(_configuration["ProductMapper:TaxRate"]);
                ViewBag.Images = GetImageNames();
                return View("ShowCreateProductForm", product);
            }
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToAction(nameof(ShowAllProducts));
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

        public async Task<IActionResult> ShowAllProducts()
        {
            IEnumerable<ProductViewModel> products = await _productService.GetAllProducts();
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Helper method to get imagesd in image folder
        private List<string> GetImageNames()
        {
            string imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);
            return Directory.EnumerateFiles(imagesPath).Select(fileName => Path.GetFileName(fileName)).ToList();
        }

        public async Task<IActionResult> ShowUpdateProductForm(int id)
        {
            ViewBag.Images = GetImageNames();
            ViewBag.TaxRate = decimal.Parse(_configuration["ProductMapper:TaxRate"]);
            ProductViewModel product = await _productService.GetProductById(id);
            return PartialView(product);
        }

        public async Task<IActionResult> UpdateProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                { // file was uploded
                    product.ImageURL = await PerformFileUpload(product);
                }

                // update product in db
                await _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Redirect to create form if incvalid
                ViewBag.TaxRate = decimal.Parse(_configuration["ProductMapper:TaxRate"]);
                ViewBag.Images = GetImageNames();
                return View("ShowUpdateProductForm", product);
            }
        }

        public IActionResult ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> SearchForProducts(SearchFor searchFor)
        {
            List<ProductViewModel> searchResults = _productService.SearchForProducts(searchFor).Result.ToList();
            ViewBag.SearchTerm = searchFor.SearchTerm;
            ViewBag.InTitle = searchFor.InTitle;
            ViewBag.InDescription = searchFor.InDescription;
            
            return View(searchResults);
        }

        public async Task<IActionResult> ShowProduct(int id)
        {
            ProductViewModel product = await _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductFromModal(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
				{ // file was uploded
					product.ImageURL = await PerformFileUpload(product);
				}

                // add the product to the db
                await _productService.UpdateProduct(product);
                var updatedVM = await _productService.GetProductById(int.Parse(product.Id));
                return PartialView("_ProductCard", updatedVM);
            }
            else
            {
                ViewBag.Images = GetImageNames();
                ViewBag.TaxRate = decimal.Parse(_configuration["ProductMapper:TaxRate"]);
                return PartialView("ShowUpdateProductForm", product);
            }
        }
    }
}
