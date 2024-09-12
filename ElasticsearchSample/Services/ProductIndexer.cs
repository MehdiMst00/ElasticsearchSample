using ElasticsearchSample.Models;

namespace ElasticsearchSample.Services;

public class ProductIndexer(ProductService productService)
{
    public async Task IndexAllProducts()
    {
        // TODO: Seed from database
        var products = new List<Product>()
        {
            new () { Id = 1, Name = "فلش شماره یک", Price = 10000 },
            new () { Id = 2, Name = "فلش فلت", Price = 10000 },
            new () { Id = 3, Name = "گوشی موبایل سامسونگ", Price = 10000 },
        };
        await productService.IndexProducts(products);
    }
}