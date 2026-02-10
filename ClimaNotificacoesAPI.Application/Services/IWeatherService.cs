using Newtonsoft.Json.Linq;

namespace ClimaNotificacoesAPI.Application.Services;

public interface IWeatherService
{
    Task<JObject> GetForecast(string cidade);
}
