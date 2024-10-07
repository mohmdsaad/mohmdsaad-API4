using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repositories
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var productBrands = File.ReadAllText("../Store.Repositories/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);

                    if (brands is not null)
                        await context.ProductBrands.AddRangeAsync(brands);
                }
                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var productTypes = File.ReadAllText("../Store.Repositories/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);

                    if (types is not null)
                        await context.ProductTypes.AddRangeAsync(types);
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Store.Repositories/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null)
                        await context.Products.AddRangeAsync(products);
                }
                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Store.Repositories/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    if (deliveryMethods is not null)
                        await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);

            }
        }
    }
}
