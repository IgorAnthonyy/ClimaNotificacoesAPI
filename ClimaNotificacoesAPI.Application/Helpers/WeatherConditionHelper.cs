using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Application.Helpers;

public static class WeatherConditionHelper
{
    private static readonly HashSet<string> AlertConditions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "nublado",
        "chuva leve",
        "chuva",
        "trovoada",
        "neve"
    };

    public static bool RequiresAlert(string condition)
    {
        return AlertConditions.Contains(condition);
    }
}
