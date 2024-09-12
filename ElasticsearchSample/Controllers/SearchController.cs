using ElasticsearchSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchSample.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController(ProductService productService) : ControllerBase
{
    [HttpGet("products")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var results = await productService.SearchProducts(query);
        return Ok(results);
    }
}