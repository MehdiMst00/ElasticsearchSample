using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticsearchSample.Services;

public class ElasticsearchService
{
    private readonly ElasticsearchClient _client;

    public ElasticsearchService(IConfiguration configuration)
    {
        var settings = new ElasticsearchClientSettings(new Uri(configuration["Elasticsearch:Url"]!))
            .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true)
            .Authentication(
                new BasicAuthentication(configuration["Elasticsearch:Username"]!, configuration["Elasticsearch:Password"]!))
            .DefaultIndex(configuration["Elasticsearch:DefaultIndex"]!);

        _client = new ElasticsearchClient(settings);
    }

    public ElasticsearchClient Client => _client;
}