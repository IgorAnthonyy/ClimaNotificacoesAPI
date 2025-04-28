using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;
public interface ICidadeRepository
{
    Task<Cidade> GetByIdAsync(int id);
    Task<Cidade> AddAsync(Cidade cidade);
    Task DeleteAsync(int id);

    Task<List<PrevisaoTempo>> GetPrevisaoTempoByCidadeAsync(int cidadeId);

    Task<List<Cidade>> GetAllAsync();
}
