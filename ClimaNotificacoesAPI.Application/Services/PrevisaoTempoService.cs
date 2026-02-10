using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Application.Exceptions;
using ClimaNotificacoesAPI.Application.Helpers;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Mapster;

namespace ClimaNotificacoesAPI.Application.Services;

public class PrevisaoTempoService
{
    private readonly IPrevisaoTempoRepository _previsaoTempoRepository;
    private readonly CidadeService _cidadeService;
    private readonly EmailService _emailService;
    private readonly IWeatherService _weatherService;
    private readonly UsuarioService _usuarioService;

    public PrevisaoTempoService(IPrevisaoTempoRepository previsaoTempoRepository, CidadeService cidadeService, UsuarioService usuarioService, EmailService emailService, IWeatherService weatherService)
    {
        _weatherService = weatherService;
        _cidadeService = cidadeService;
        _usuarioService = usuarioService;
        _emailService = emailService;
        _previsaoTempoRepository = previsaoTempoRepository;
    }
    public async Task<PrevisaoDTOResponse> FetchAndUpdateForecastAsync(int cidadeId)
    {
        var cidade = await _cidadeService.GetByIdAsync(cidadeId);

        var previsaoJson = await _weatherService.GetForecast(cidade.Nome);

        if (previsaoJson == null)
            throw new PrevisaoTempoNaoEncontradaException("Previsão do tempo não encontrada.");

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

        if (WeatherConditionHelper.RequiresAlert(previsaoEntity.Condicao))
        {
            var usuario = await _usuarioService.GetByIdAsync(cidade.UsuarioId);
            await _emailService.SendAlertEmailAsync(usuario.Email, usuario.Nome, previsaoEntity.Condicao, cidade.Nome);
        }
        return previsaoCriada.Adapt<PrevisaoDTOResponse>();
    }

    public async Task<PrevisaoTempo> CreateAsync(PrevisaoTempo previsaoTempo)
    {
        return await _previsaoTempoRepository.AddAsync(previsaoTempo);
    }

}