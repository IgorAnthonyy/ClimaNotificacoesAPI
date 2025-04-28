using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"];
    }

    public async Task<JObject> ObterPrevisao(string cidade)
    {
        var url = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={_apiKey}&units=metric&lang=pt_br";
        var response = await _httpClient.GetStringAsync(url);
        JObject data = JObject.Parse(response);

        var previsao = new JObject
        {
            ["data"] = System.DateTime.UtcNow, // Data e hora atual
            ["condicao"] = data["weather"][0]["description"], // Descrição do tempo
            ["temperaturaMaxima"] = data["main"]["temp_max"], // Temperatura máxima
            ["temperaturaMinima"] = data["main"]["temp_min"], // Temperatura mínima
            ["umidade"] = data["main"]["humidity"], // Umidade
            ["velocidadeVento"] = data["wind"]["speed"], // Velocidade do vento
        };

        return previsao;
    }
}
