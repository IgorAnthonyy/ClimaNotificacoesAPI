using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ClimaNotificacoesAPI.Application.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"];
    }

    public async Task<JObject> GetForecast(string cidade)
    {
        var url = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={_apiKey}&units=metric&lang=pt_br";

        var response = await _httpClient.GetStringAsync(url);

        JObject data = JObject.Parse(response);

        var previsao = new JObject
        {
            ["data"] = System.DateTime.UtcNow,
            ["condicao"] = data["weather"][0]["description"],
            ["temperaturaMaxima"] = data["main"]["temp_max"],
            ["temperaturaMinima"] = data["main"]["temp_min"],
            ["umidade"] = data["main"]["humidity"],
            ["velocidadeVento"] = data["wind"]["speed"],
        };

        return previsao;
    }
}
