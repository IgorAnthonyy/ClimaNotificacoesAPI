using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;
public interface IPrevisaoTempoRepository
{
    Task<IEnumerable<PrevisaoTempo>> GetAllAsync();
    Task<PrevisaoTempo> GetByIdAsync(int id);
    Task<PrevisaoTempo> AddAsync(PrevisaoTempo previsaoTempo);
    Task<PrevisaoTempo> UpdateAsync(PrevisaoTempo previsaoTempo);
    Task DeleteAsync(int id);
}
