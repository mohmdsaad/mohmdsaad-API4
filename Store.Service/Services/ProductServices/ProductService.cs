using AutoMapper;
using Store.Data.Entities;
using Store.Repositories.Interfaces;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand,int>().GetAllAsNoTrackingAsync();
            //IReadOnlyList<BrandTypeDetailsDto> mappedBrands = brands.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatedAt = x.CreatedAt
            //}).ToList();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }

        public async Task<PagenatiedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecification(input);
            var products = await _unitOfWork.Repository<Product,int>().GetAllWithSpecificationAsync(specs);
            //IReadOnlyList<ProductDetailsDto> mappedProducts = products.Select(x => new ProductDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    BrandName = x.Brand.Name,
            //    CreatedAt = x.CreatedAt,
            //    Description = x.Description,
            //    PictureUrl = x.PictureUrl,
            //    Price = x.Price,
            //    TypeName = x.Type.Name               
            //}).ToList();
            var countSpecs = new ProductWithCountSpecification(input);
            var count =await _unitOfWork.Repository< Product ,int>().GetCountSpecificationAsync(countSpecs);
            var mappedProducts = _mapper.Map< IReadOnlyList< ProductDetailsDto >>(products);
            return new PagenatiedResultDto<ProductDetailsDto>(input.PageIndex,input.PageSize, count, mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();
            //IReadOnlyList<BrandTypeDetailsDto> mappedTypes = types.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatedAt = x.CreatedAt
            //}).ToList();
            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? productId)
        {
            if (productId == null)
                throw new Exception("Id is null");
            var specs = new ProductWithSpecification(productId);
            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecificationByIdAsync(specs);
            if (product == null)
                throw new Exception("This Product is not fount");
            //var mappedProduct = new ProductDetailsDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    CreatedAt = product.CreatedAt,
            //    Price = product.Price,
            //    TypeName = product.Type.Name,
            //    BrandName = product.Brand.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl
            //};
            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);
            return mappedProduct;
        }
    }
}
