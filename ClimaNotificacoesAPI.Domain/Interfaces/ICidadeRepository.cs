using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;
public interface ICidadeRepository
{
    Task<Cidade> GetByIdAsync(int id);
    Task<Cidade> AddAsync(Cidade cidade);
    Task DeleteAsync(int id);
}
