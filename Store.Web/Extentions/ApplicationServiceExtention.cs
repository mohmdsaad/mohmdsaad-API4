using Microsoft.AspNetCore.Mvc;
using Store.Repositories.Interfaces;
using Store.Repositories.Repositories;
using Store.Services.Services.Products.Dtos;
using Store.Services.Services.Products;
using Store.Services.HandleResponses;
using Store.Services.Services.CashService;
using Store.Service.Services.BasketServices.Dtos;
using Store.Service.Services.BasketServices;
using Store.Repository.Basket;

namespace Store.Web.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                                .Where(model => model.Value?.Errors.Count() > 0)
                                                .SelectMany(model => model.Value?.Errors)
                                                .Select(error => error.ErrorMessage)
                                                .ToList();
                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
