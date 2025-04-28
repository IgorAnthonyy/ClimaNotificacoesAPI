using ClimaNotificacoesAPI.Domain.Entities;

namespace ClimaNotificacoesAPI.Domain.Interfaces;
public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(int id);
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
    Task<Usuario> GetByEmailAsync(string email);

    Task<IEnumerable<Cidade>> GetCidadesByUsuarioIdAsync(int usuarioId);
}
