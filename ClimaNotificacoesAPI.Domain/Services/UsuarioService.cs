using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;

namespace ClimaNotificacoesAPI.Domain.Services;

public class UsuarioService 
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _usuarioRepository.GetByIdAsync(id);
    }
    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _usuarioRepository.GetAllAsync();
    }
    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        return await _usuarioRepository.AddAsync(usuario);
    }
    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        return await _usuarioRepository.UpdateAsync(usuario);
    }
    public async Task DeleteAsync(int id)
    {
        await _usuarioRepository.DeleteAsync(id);
    }
}