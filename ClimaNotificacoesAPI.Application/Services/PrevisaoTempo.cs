using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;

namespace ClimaNotificacoesAPI.Application.Services;

public class PrevisaoTempoService
{
    private readonly IPrevisaoTempoRepository _previsaoTempoRepository;

    public PrevisaoTempoService(IPrevisaoTempoRepository previsaoTempoRepository)
    {
        _previsaoTempoRepository = previsaoTempoRepository;
    }

    public async Task<PrevisaoTempo> CreateAsync(PrevisaoTempo previsaoTempo)
    {
        return await _previsaoTempoRepository.AddAsync(previsaoTempo);
    }

}