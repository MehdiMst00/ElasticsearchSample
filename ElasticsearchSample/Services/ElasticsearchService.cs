using Elastic.Clients.Elasticsearch;

namespace ElasticsearchSample.Services;

public class ElasticsearchService(IConfiguration configuration)
{
    private readonly ElasticsearchClient _client = new(new Uri(configuration["Elasticsearch:Url"]!));

    public ElasticsearchClient Client => _client;
}