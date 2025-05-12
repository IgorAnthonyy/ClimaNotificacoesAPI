using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Application.Exceptions;
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

    public CidadeController(CidadeService cidadeService)
    {
        _cidadeService = cidadeService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCidade(int id)
    {
        var cidade = await _cidadeService.GetByIdAsync(id);
        var cidadeResponse = cidade.Adapt<CidadeDTOResponse>();
        return Ok(cidadeResponse);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCidade([FromBody] CidadeDTORequest cidadeDto)
    {
        var cidadeEntitie = cidadeDto.Adapt<Cidade>();
        var cidadeCriada = await _cidadeService.CreateAsync(cidadeEntitie);
        var cidadeBuscar = await _cidadeService.GetByIdAsync(cidadeCriada.Id);
        var cidadeResponse = cidadeBuscar.Adapt<CidadeDTOResponse>();
        return CreatedAtAction(nameof(GetCidade), new { id = cidadeResponse.Id }, cidadeResponse);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCidade(int id)
    {
        await _cidadeService.DeleteAsync(id);
        return NoContent();
    }
    [HttpGet("{id}/PrevisaoTempo")]
    public async Task<IActionResult> GetPrevisaoTempo(int id)
    {
        var previsaoTempo = await _cidadeService.GetPrevisaoTempoByCidadeAsync(id);
        var previsaoTempoResponse = previsaoTempo.Adapt<List<PrevisaoDTOResponse>>();
        return Ok(previsaoTempoResponse);
    }
    [HttpGet]
    public async Task<IActionResult> GetCidades()
    {
        var cidades = await _cidadeService.GetAllAsync();
        var cidadesResponse = cidades.Adapt<List<CidadeDTOResponse>>();
        return Ok(cidadesResponse);
    }
}
