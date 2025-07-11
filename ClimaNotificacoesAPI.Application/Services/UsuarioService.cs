using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Application.Exceptions;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

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
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
        {
            throw new UsuarioNaoEncontradoException();
        }
        return usuario;
    }
    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _usuarioRepository.GetAllAsync();
    }
    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        var usuarioExistente = await GetByEmailAsync(usuario.Email);

        if (usuarioExistente != null)
            throw new EmailJaCadastradoException(usuario.Email);

        usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
        return await _usuarioRepository.AddAsync(usuario);
    }
    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        var usuarioExistente = await GetByIdAsync(usuario.Id);
        if (usuarioExistente == null)
        {
            throw new UsuarioNaoEncontradoException();
        }
        var usuarioComMesmoEmail = await GetByEmailAsync(usuario.Email);
        if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.Id != usuario.Id)
            throw new EmailJaCadastradoException(usuario.Email);

        usuarioExistente.Nome = usuario.Nome;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
        return await _usuarioRepository.UpdateAsync(usuarioExistente);
    }
    public async Task DeleteAsync(int id)
    {
        var usuarioExistente = await GetByIdAsync(id);
        if (usuarioExistente == null)
        {
            throw new UsuarioNaoEncontradoException();
        }
        await _usuarioRepository.DeleteAsync(id);
    }
    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _usuarioRepository.GetByEmailAsync(email);
    }
    public async Task<IEnumerable<Cidade>> GetCidadesByUsuarioIdAsync(int usuarioId)
    {
        var usuario = await GetByIdAsync(usuarioId);
        if (usuario == null)
        {
            throw new UsuarioNaoEncontradoException();
        }
        var cidades = await _usuarioRepository.GetCidadesByUsuarioIdAsync(usuarioId);
        return cidades;
    }
    public async Task<Usuario> LoginAsync(LoginDTORequest login){
        var usuario = await _usuarioRepository.GetByEmailAsync(login.Email);
        if (usuario == null)
        {
            throw new UsuarioNaoEncontradoException();
        }
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, login.Senha);
        if (resultado == PasswordVerificationResult.Failed)
        {
            throw new CredencialIncorretaException();
        }
        return usuario;
    }
}