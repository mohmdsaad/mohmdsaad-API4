using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Specifications.ProductSpecs;
using Store.Services.Services.Products;
using Store.Services.Services.Products.Dtos;
using Store.Web.Helper;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
            => Ok(await _productService.GetAllBrandsAsync());
        [HttpGet]
        //[Cash(10)]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
            => Ok(await _productService.GetAllTypesAsync());
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllproducts([FromQuery] ProductSpecification input)
            => Ok(await _productService.GetAllProductsAsync(input));
        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(int? id)
            => Ok(await _productService.GetProductByIdAsync(id));
    }
}
