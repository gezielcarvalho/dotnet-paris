using Microsoft.AspNetCore.Mvc;
using DotNetParis.Models;

namespace DotNetParis.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
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

            var forecast = Enumerable.Range(1, 5).Select(index =>
            
            new Weather
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Date de la prévision
                Random.Shared.Next(-20, 55), // Température aléatoire en °C
                summaries[Random.Shared.Next(summaries.Length)] // Description de la température
            ))
            .ToArray();

            return Ok(forecast);
        }
    }
}