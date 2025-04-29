using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Mapster;

namespace ClimaNotificacoesAPI.Application.Services;

public class PrevisaoTempoService
{
    private readonly IPrevisaoTempoRepository _previsaoTempoRepository;
    private readonly CidadeService _cidadeService;
    private readonly EmailService _emailService;
    private readonly WeatherService _weatherService;
    private readonly UsuarioService _usuarioService;

    public PrevisaoTempoService(IPrevisaoTempoRepository previsaoTempoRepository, CidadeService cidadeService, UsuarioService ususarioService, EmailService emailService, WeatherService weatherService)
    {
        _weatherService = weatherService;
        _cidadeService = cidadeService;
        _usuarioService = ususarioService;
        _emailService = emailService;
        _previsaoTempoRepository = previsaoTempoRepository;
    }
    public async Task<PrevisaoDTOResponse> FetchAndUpdateForecastAsync(Cidade cidade)
    {
        var previsaoJson = await _weatherService.GetForecast(cidade.Nome);

        if (previsaoJson == null)
            throw new Exception("Previsão do tempo não encontrada.");

        var previsaoEntity = new PrevisaoTempo
        {
            CidadeId = cidade.Id,
            Data = (DateTime)previsaoJson["data"],
            Condicao = (string)previsaoJson["condicao"],
            TemperaturaMaxima = (double)previsaoJson["temperaturaMaxima"],
            TemperaturaMinima = (double)previsaoJson["temperaturaMinima"],
            Umidade = (double)previsaoJson["umidade"],
            VelocidadeVento = (double)previsaoJson["velocidadeVento"]
        };
        var previsaoCriada = await CreateAsync(previsaoEntity);

        if (previsaoEntity.Condicao == "nublado" || previsaoEntity.Condicao == "chuva leve" || previsaoEntity.Condicao == "chuva" || previsaoEntity.Condicao == "trovoada" || previsaoEntity.Condicao == "neve")
        {
            var usuario = await _usuarioService.GetByIdAsync(cidade.UsuarioId);
            var cidadeFetch = await _cidadeService.GetByIdAsync(cidade.Id);
            if (usuario != null)
            {
                await _emailService.SendAlertEmailAsync(usuario.Email, usuario.Nome, previsaoEntity.Condicao, cidadeFetch.Nome);
            }
        }
        return previsaoCriada.Adapt<PrevisaoDTOResponse>();
    }

    public async Task<PrevisaoTempo> CreateAsync(PrevisaoTempo previsaoTempo)
    {
        return await _previsaoTempoRepository.AddAsync(previsaoTempo);
    }

}