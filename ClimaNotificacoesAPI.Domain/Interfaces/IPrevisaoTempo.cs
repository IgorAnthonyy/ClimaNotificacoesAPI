using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;
public interface IPrevisaoTempoRepository
{
    Task<PrevisaoTempo> AddAsync(PrevisaoTempo previsaoTempo);
}
