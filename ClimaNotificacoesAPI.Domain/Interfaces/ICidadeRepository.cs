using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;
public interface ICidadeRepository
{
    Task<IEnumerable<Cidade>> GetAllAsync();
    Task<Cidade> GetByIdAsync(int id);
    Task<Cidade> AddAsync(Cidade cidade);
    Task<Cidade> UpdateAsync(Cidade cidade);
    Task DeleteAsync(int id);
}
