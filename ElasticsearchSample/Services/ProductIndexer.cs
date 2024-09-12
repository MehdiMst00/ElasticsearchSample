using ElasticsearchSample.Models;

namespace ElasticsearchSample.Services;

public class ProductIndexer(ProductService productService)
{
    public async Task IndexAllProducts()
    {
        // TODO: Seed from database
        var products = new List<Product>()
        {
            new () { Id = 1, Name = "فلش شماره یک", Description = "", Price = 10000 },
            new () { Id = 2, Name = "فلش فلت", Description = "", Price = 10000 },
            new () { Id = 3, Name = "گوشی موبایل سامسونگ", Description = "" ,Price = 10000 },
            new () { Id = 4, Name = "فلش شماره دو", Description = "", Price = 10000 },
        };
        await productService.IndexProducts(products);
    }
}