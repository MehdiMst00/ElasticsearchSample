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
builder.Services.AddSingleton<ProductIndexer>();

var app = builder.Build();

var productIndexer = app.Services.GetRequiredService<ProductIndexer>();
await productIndexer.IndexAllProducts();

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
