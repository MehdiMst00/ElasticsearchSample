using ElasticsearchSample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add elastic search
builder.Services.AddSingleton<ElasticsearchService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductIndexer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var productIndexer = scope.ServiceProvider.GetRequiredService<ProductIndexer>();
    await productIndexer.IndexAllProducts();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
