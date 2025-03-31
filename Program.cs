var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5151");

// Ajouter les services à l'application.
// En savoir plus sur la configuration de Swagger/OpenAPI : https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurer le pipeline des requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Tableau des descriptions de la température.
var summaries = new[]
{
    "Glacial",      // Freezing
    "Vivifiant",    // Bracing
    "Frisquet",     // Chilly-Willy
    "Frais",        // Cool
    "Doux",         // Mild
    "Tiède",        // Warm
    "Agréable",     // Balmy
    "Chaud",        // Hot
    "Étouffant",    // Sweltering
    "Brûlant"       // Scorching
};

// Définir un endpoint GET pour récupérer la prévision météo.
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Date de la prévision
            Random.Shared.Next(-20, 55), // Température aléatoire en °C
            summaries[Random.Shared.Next(summaries.Length)] // Description de la température
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast") // Nom de l'endpoint
.WithOpenApi(); // Ajout automatique à la documentation OpenAPI

app.Run();

// Modèle de données pour une prévision météo.
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // Calcul de la température en Fahrenheit
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
