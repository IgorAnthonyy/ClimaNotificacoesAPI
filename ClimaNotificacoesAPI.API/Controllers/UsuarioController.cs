using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Application.Exceptions;
using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ClimaNotificacoesAPI.API.Controllers;
[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;
    private readonly TokenService _tokenService;

    public UsuarioController(UsuarioService usuarioService, TokenService tokenService)
    {
        _tokenService = tokenService;
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        var usuarioResponse = usuario.Adapt<UsuarioDTOResponse>();
        return Ok(usuarioResponse);
    }
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _usuarioService.GetAllAsync();
        var usuariosResponse = usuarios.Adapt<List<UsuarioDTOResponse>>();
        return Ok(usuariosResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] UsuarioDTORequest usuarioDto)
    {
        try
        {
            var usuarioEntity = usuarioDto.Adapt<Usuario>();
            var usuarioCriado = await _usuarioService.CreateAsync(usuarioEntity);
            var usuarioResponse = usuarioCriado.Adapt<UsuarioDTOResponse>();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioResponse.Id }, usuarioResponse);
        }
        catch (EmailJaCadastradoException ex)
        {
            return Conflict(ex.Message);
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDTORequest usuarioDto)
    {
        try
        {
            var usuarioEntity = usuarioDto.Adapt<Usuario>();
            usuarioEntity.Id = id;
            var usuarioAtualizado = await _usuarioService.UpdateAsync(usuarioEntity);
            var usuarioResponse = usuarioAtualizado.Adapt<UsuarioDTOResponse>();
            return Ok(usuarioResponse);
        }
        catch (EmailJaCadastradoException ex)
        {
            return Conflict(ex.Message);
        }
        catch (UsuarioNaoEncontradoException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        try
        {
            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
        catch (UsuarioNaoEncontradoException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("{id}/cidades")]
    public async Task<IActionResult> GetCidadesByUsuarioId(int id)
    {
        try
        {
            var cidades = await _usuarioService.GetCidadesByUsuarioIdAsync(id);
            var cidadesResponse = cidades.Adapt<List<CidadePorUsuarioDTO>>();
            return Ok(cidadesResponse);
        }
        catch (UsuarioNaoEncontradoException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTORequest usuarioLoginDto)
    {
        try
        {
            var usuario = await _usuarioService.LoginAsync(usuarioLoginDto);
            var token = _tokenService.GerarToken(usuario); 
            var usuarioResponse = usuario.Adapt<UsuarioDTOResponse>();
            var loginResponse = new LoginDTOResponse
            {
                Usuario = usuarioResponse,
                Token = token
            };
            return Ok(loginResponse);
        }
        catch (UsuarioNaoEncontradoException ex)
        {
            return NotFound(ex.Message);
        }
        catch (CredencialIncorretaException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}


