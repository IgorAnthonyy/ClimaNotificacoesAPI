using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario> GetByEmailAsync(string email);
    Task<IEnumerable<Cidade>> GetCidadesByUsuarioIdAsync(int usuarioId);
}
