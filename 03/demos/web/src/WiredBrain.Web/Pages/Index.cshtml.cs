using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WiredBrain.Web.Models;
using WiredBrain.Web.Services;

namespace WiredBrain.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductService _productsService;
        private readonly StockService _stockService;

        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ProductService productsService, StockService stockService, ILogger<IndexModel> logger)
        {
            _productsService = productsService;
            _stockService = stockService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogDebug($"Loading products & stock");
            
            Products = await _productsService.GetProducts();
            foreach (var product in Products)
            {
                var productStock = await _stockService.GetStock(product.Id);
                product.Stock = productStock.Stock;
            }

            _logger.LogDebug($"Products & stock load took: {stopwatch.Elapsed.TotalMilliseconds}ms");
            return Page();
        }
    }
}
