using DotNetParis.Services;
using DotNetParis.Repositories;
using DotNetParis.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5151");

// Ajouter les services à l'application.
// En savoir plus sur la configuration de Swagger/OpenAPI : https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ProductService>(); // Register ProductService
builder.Services.AddScoped<ProductRepository>(); // Register ProductRepository
builder.Services.AddScoped<ApplicationDbContext>(); // Register ApplicationDbContext as a simple service

var app = builder.Build();

// Configurer le pipeline des requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


