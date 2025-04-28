using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Mapster;

namespace ClimaNotificacoesAPI.Application.Services;

public class PrevisaoTempoService
{
    private readonly IPrevisaoTempoRepository _previsaoTempoRepository;
    private readonly WeatherService _weatherService;

    public PrevisaoTempoService(IPrevisaoTempoRepository previsaoTempoRepository, WeatherService weatherService)
    {
        _weatherService = weatherService;
        _previsaoTempoRepository = previsaoTempoRepository;
    }
    public async Task<PrevisaoDTOResponse> BuscarEAtualizarPrevisaoAsync(Cidade cidade)
    {
        var previsaoJson = await _weatherService.ObterPrevisao(cidade.Nome);

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
        return previsaoCriada.Adapt<PrevisaoDTOResponse>();
    }

    public async Task<PrevisaoTempo> CreateAsync(PrevisaoTempo previsaoTempo)
    {
        return await _previsaoTempoRepository.AddAsync(previsaoTempo);
    }

}