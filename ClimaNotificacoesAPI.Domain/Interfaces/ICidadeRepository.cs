using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;

public interface ICidadeRepository : IGenericRepository<Cidade>
{
    Task<List<PrevisaoTempo>> GetPrevisaoTempoByCidadeAsync(int cidadeId);
}
