using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClimaNotificacoesAPI.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();
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

        usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
        return await _usuarioRepository.AddAsync(usuario);
    }
    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
        return await _usuarioRepository.UpdateAsync(usuario);
    }
    public async Task DeleteAsync(int id)
    {
        await _usuarioRepository.DeleteAsync(id);
    }
    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _usuarioRepository.GetByEmailAsync(email);
    }
    public async Task<IEnumerable<Cidade>> GetCidadesByUsuarioIdAsync(int usuarioId)
    {
        return await _usuarioRepository.GetCidadesByUsuarioIdAsync(usuarioId);
    }
}