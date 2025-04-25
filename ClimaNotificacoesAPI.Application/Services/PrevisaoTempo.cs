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

    public async Task<PrevisaoTempo> GetByIdAsync(int id)
    {
        return await _previsaoTempoRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<PrevisaoTempo>> GetAllAsync()
    {
        return await _previsaoTempoRepository.GetAllAsync();
    }
    public async Task<PrevisaoTempo> CreateAsync(PrevisaoTempo previsaoTempo)
    {
        return await _previsaoTempoRepository.AddAsync(previsaoTempo);
    }
    public async Task<PrevisaoTempo> UpdateAsync(PrevisaoTempo previsaoTempo)
    {
        return await _previsaoTempoRepository.UpdateAsync(previsaoTempo);
    }
    public async Task DeleteAsync(int id)
    {
        await _previsaoTempoRepository.DeleteAsync(id);
    }
}