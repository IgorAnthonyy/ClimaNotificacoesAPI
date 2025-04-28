using System.Net.Http;  // Importa a biblioteca para fazer requisições HTTP
using System.Threading.Tasks;  // Importa a biblioteca para usar o tipo Task (assíncrono)
using Microsoft.Extensions.Configuration;  // Importa a biblioteca para acessar configurações
using Newtonsoft.Json.Linq;  // Importa a biblioteca para manipulação de JSON

// A classe WeatherService é responsável por acessar a API do OpenWeatherMap e obter a previsão do tempo
public class WeatherService
{
    private readonly HttpClient _httpClient;  // Instância do HttpClient para fazer requisições HTTP
    private readonly string _apiKey;  // Chave da API do OpenWeatherMap (armazenada em configuração)

    // Construtor da classe que recebe o HttpClient e a configuração da aplicação
    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;  // Inicializa o HttpClient
        _apiKey = configuration["OpenWeatherMap:ApiKey"];  // Obtém a chave da API da configuração
    }

    // Método assíncrono para obter a previsão do tempo de uma cidade
    public async Task<JObject> ObterPrevisao(string cidade)
    {
        // Monta a URL para fazer a requisição à API do OpenWeatherMap com os parâmetros necessários
        var url = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={_apiKey}&units=metric&lang=pt_br";

        // Faz a requisição GET e obtém a resposta como uma string
        var response = await _httpClient.GetStringAsync(url);

        // Faz o parsing da resposta JSON para um JObject (estrutura do JSON)
        JObject data = JObject.Parse(response);

        // Cria um novo JObject para organizar as informações que serão retornadas
        var previsao = new JObject
        {
            ["data"] = System.DateTime.UtcNow,  // Data e hora atuais em formato UTC
            ["condicao"] = data["weather"][0]["description"],  // Descrição da condição climática (exemplo: "céu limpo")
            ["temperaturaMaxima"] = data["main"]["temp_max"],  // Temperatura máxima
            ["temperaturaMinima"] = data["main"]["temp_min"],  // Temperatura mínima
            ["umidade"] = data["main"]["humidity"],  // Umidade do ar
            ["velocidadeVento"] = data["wind"]["speed"],  // Velocidade do vento
        };

        // Retorna o objeto JObject com os dados organizados
        return previsao;
    }
}
