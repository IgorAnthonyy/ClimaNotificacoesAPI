using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ClimaNotificacoesAPI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CidadeController : ControllerBase
{
    private readonly CidadeService _cidadeService;
    private readonly UsuarioService _usuarioService;

    public CidadeController(CidadeService cidadeService, UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        _cidadeService = cidadeService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCidade(int id)
    {
        var cidade = await _cidadeService.GetByIdAsync(id);
        if (cidade == null)
        {
            return NotFound();
        }
        var cidadeResponse = cidade.Adapt<CidadeDTOResponse>();
        return Ok(cidadeResponse);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCidade([FromBody] CidadeDTORequest cidadeDto)
    {
        var cidadesUsuario = await _usuarioService.GetCidadesByUsuarioIdAsync(cidadeDto.UsuarioId);
        var cidadeExistente = false;
        if (cidadesUsuario != null)
        {
            foreach (var cidade in cidadesUsuario)
            {
                if (cidade.Nome == cidadeDto.Nome)
                {
                    cidadeExistente = true;
                    break;
                }
            }
        }
        if (cidadeExistente)
            return Conflict($"Já existe outra cidade com o nome {cidadeDto.Nome}.");

        var cidadeEntitie = cidadeDto.Adapt<Cidade>();
        var cidadeCriada = await _cidadeService.CreateAsync(cidadeEntitie);
        var cidadeBuscar = await _cidadeService.GetByIdAsync(cidadeCriada.Id);
        var cidadeResponse = cidadeBuscar.Adapt<CidadeDTOResponse>();
        return CreatedAtAction(nameof(GetCidade), new { id = cidadeResponse.Id }, cidadeResponse);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCidade(int id)
    {
        var cidade = await _cidadeService.GetByIdAsync(id);
        if (cidade == null)
        {
            return NotFound();
        }
        await _cidadeService.DeleteAsync(id);
        return NoContent();
    }
}
