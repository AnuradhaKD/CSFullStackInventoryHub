using SharedModels;

var builder = WebApplication.CreateBuilder(args);

// CORS: open for development; tighten for production
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Simple in-memory cache (per-run) for demo
var products = new[]
{
    new Product
    {
        Id = 1, Name = "Laptop", Price = 1200.50, Stock = 25,
        Category = new Category { Id = 101, Name = "Electronics" }
    },
    new Product
    {
        Id = 2, Name = "Headphones", Price = 50.00, Stock = 100,
        Category = new Category { Id = 102, Name = "Accessories" }
    }
};

// Endpoint with basic caching semantics (ETag)
app.MapGet("/api/productlist", (HttpContext ctx) =>
{
    var etag = "\"v1-products\"";
    ctx.Response.Headers.ETag = etag;

    // Simple conditional GET handling
    if (ctx.Request.Headers.TryGetValue("If-None-Match", out var inm) && inm == etag)
    {
        return Results.StatusCode(StatusCodes.Status304NotModified);
    }

    return Results.Ok(products);
});

app.Run();
