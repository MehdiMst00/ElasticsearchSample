using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticsearchSample.Models;

namespace ElasticsearchSample.Services;

public class ProductService(ElasticsearchService elasticsearchService)
{
    private readonly string _indexName = "products";

    public async Task IndexProducts(IEnumerable<Product> products)
    {
        var client = elasticsearchService.Client;
        var response = await client.IndexManyAsync(products, _indexName);

        if (!response.IsValidResponse)
        {
            // TODO: Handle errors
        }
    }

    public async Task<List<Product>> SearchProducts(string query)
    {
        var client = elasticsearchService.Client;

        var searchResponse = await client.SearchAsync<Product>(s => s
            .Index(_indexName)
            .Query(q => q
                .FunctionScore(fs => fs
                    .Query(q => q
                        .MultiMatch(mm => mm
                            .Query(query)
                            .Fields("name")
                            .Type(TextQueryType.BestFields)
                            .Fuzziness(new Fuzziness(1))
                            .MinimumShouldMatch("75%")
                        )
                    )
                 .BoostMode(FunctionBoostMode.Multiply)
                )
            )
        );

        return [.. searchResponse.Documents];
    }
}