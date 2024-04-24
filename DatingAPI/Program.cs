using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed; // Add this namespace for IDistributedCache
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using DatingSiteLibrary; // Add this namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Register the Dating service
builder.Services.AddScoped<Dating>();

// Add data protection services
builder.Services.AddDataProtection();

// Add session state services
builder.Services.AddSession();

// Add distributed memory cache
builder.Services.AddDistributedMemoryCache();

// Replace the default session store with distributed session store
builder.Services.TryAddSingleton<Microsoft.AspNetCore.Session.ISessionStore, Microsoft.AspNetCore.Session.DistributedSessionStore>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Add session middleware
app.UseSession();

app.MapControllers();
app.Run();
