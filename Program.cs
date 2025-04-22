using DotNetParis.Services;
using DotNetParis.Repositories;
using DotNetParis.Data;

// Creer le constructeur de la application
var builder = WebApplication.CreateBuilder(args);

// Ajouter les services à l'application.
builder.Services.AddEndpointsApiExplorer();

// Ajouter le service de Swagger pour la documentation de l'API.
builder.Services.AddSwaggerGen();

// Ajouter les controleurs pour gérer les requêtes HTTP.
builder.Services.AddControllers();

// Ajouter les services de l'application ici.
builder.Services.AddScoped<ProductService>(); 
builder.Services.AddScoped<ProductRepository>(); 
builder.Services.AddScoped<ApplicationDbContext>(); 

// Construire l'application.
var app = builder.Build();

// Configurer le pipeline des requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    // Utiliser Swagger uniquement en mode développement.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utiliser le middleware de routage pour gérer les requêtes HTTP.
app.UseHttpsRedirection();

// Appliquer les routes pour les contrôleurs.
app.MapControllers();

// Démarrer l'application.
app.Run();


