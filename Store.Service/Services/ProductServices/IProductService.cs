using Store.Repositories.Specifications.ProductSpecs;
using Store.Services.Helper;
using Store.Services.Services.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.Products
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? productId);
        Task<PagenatiedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification specs);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();
    }
}
