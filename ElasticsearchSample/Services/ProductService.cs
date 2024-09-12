using Elastic.Clients.Elasticsearch;
using ElasticsearchSample.Models;

namespace ElasticsearchSample.Services;

public class ProductService(ElasticsearchService elasticsearchService)
{
    public async Task IndexProducts(IEnumerable<Product> products)
    {
        var client = elasticsearchService.Client;
        var response = await client.IndexManyAsync(products);

        if (!response.IsValidResponse)
        {
            // Handle errors
        }
    }

    public async Task<List<Product>> SearchProducts(string query)
    {
        var client = elasticsearchService.Client;

        var searchResponse = await client.SearchAsync<Product>(s => s
            .Query(q => q
                .MultiMatch(mm => mm
                    .Query(query)
                    .Fields("Name")
                    .Fuzziness(new Fuzziness(2))
                )
            )
        );

        return [.. searchResponse.Documents];
    }
}