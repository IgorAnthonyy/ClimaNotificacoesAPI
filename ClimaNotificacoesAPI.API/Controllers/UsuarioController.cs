using ClimaNotificacoesAPI.Application.Dtos;
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

    public UsuarioController(UsuarioService usuarioService)
    {
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
        var usuarioExistente = await _usuarioService.GetByEmailAsync(usuarioDto.Email);

        if (usuarioExistente != null)
            return Conflict($"Já existe outro usuário com o e-mail {usuarioDto.Email}.");

        var usuarioEntitie = usuarioDto.Adapt<Usuario>();
        var usuarioCriado = await _usuarioService.CreateAsync(usuarioEntitie);
        var usuarioResponse = usuarioCriado.Adapt<UsuarioDTOResponse>();
        return CreatedAtAction(nameof(GetUsuario), new { id = usuarioResponse.Id }, usuarioResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDTORequest usuarioDto)
    {
        var usuarioExistente = await _usuarioService.GetByIdAsync(id);
        if (usuarioExistente == null)
        {
            return BadRequest("ID invalido.");
        }
        var usuarioComMesmoEmail = await _usuarioService.GetByEmailAsync(usuarioDto.Email);
        if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.Id != id)
            return Conflict($"Já existe outro usuário com o e-mail {usuarioDto.Email}.");

        var usuarioEntitie = usuarioDto.Adapt<Usuario>();
        usuarioEntitie.Id = id;
        var usuarioAtualizado = await _usuarioService.UpdateAsync(usuarioEntitie);
        var usuarioResponse = usuarioAtualizado.Adapt<UsuarioDTOResponse>();
        return Ok(usuarioResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuarioExistente = await _usuarioService.GetByIdAsync(id);
        if (usuarioExistente == null)
        {
            return NotFound("ID invalido.");
        }
        await _usuarioService.DeleteAsync(id);
        return NoContent();
    }
    [HttpGet("{id}/cidades")]
    public async Task<IActionResult> GetCidadesByUsuarioId(int id)
    {
        var cidades = await _usuarioService.GetCidadesByUsuarioIdAsync(id);
        if (cidades == null || !cidades.Any())
        {
            return NotFound("Nenhuma cidade encontrada para o usuário.");
        }
        var cidadesResponse = cidades.Adapt<List<CidadePorUsuarioDTO>>();
        return Ok(cidadesResponse);
    }
}

