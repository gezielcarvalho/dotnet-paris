namespace DotNetParis.Models
{
    // Modèle de données pour une prévision météo.
    public class Weather
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }

        // Calcul de la température en Fahrenheit
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public Weather(DateOnly date, int temperatureC, string? summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }
    }
}
