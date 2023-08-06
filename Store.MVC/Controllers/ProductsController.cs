using AutoMapper;
using Store.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Store.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IReadOnlyQueryableRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IReadOnlyQueryableRepository<Product> productRepository, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? productTypeId)
        {
            try
            {
                ViewBag.Title = "Syrups";
                var products = await _productRepository.GetAllAsync();
                if (productTypeId != null)
                {
                    products = await _productRepository.GetAllAsync(p => p.ProductTypeId == productTypeId);
                }
                var productVms = _mapper.Map<List<ProductVm>>(products);
                return View(productVms);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while getting products.", ex);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> Details(int id) 
        {
            try
            {
                ViewBag.Title = "Details";
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with {id} was not found!");
                    return NotFound();
                }
                var productVm = _mapper.Map<ProductVm>(product);
                return View(productVm);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting product with id {id}.", ex);
                return StatusCode(500);
            }
        }
    }
}
