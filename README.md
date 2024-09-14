# Elasticsearch Sample
## Elasticsearch sample for asp.net core

#### Setup Elasticsearch and Kibana using docker compose
```bash
docker compose -f ./docker-compose-dev.yaml up -d
```

#### Full-Text Implementation Sample
```c#
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
```
