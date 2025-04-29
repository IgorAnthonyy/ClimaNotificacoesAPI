using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClimaNotificacoesAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PrevisaoTempoController : ControllerBase
{
    private readonly PrevisaoTempoService _previsaoTempoService;

    public PrevisaoTempoController(PrevisaoTempoService previsaoTempoService)
    {
        _previsaoTempoService = previsaoTempoService;
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> ObterPrevisao(int id)
    {

        try
        {
            var previsaoResponse = await _previsaoTempoService.FetchAndUpdateForecastAsync(id);
            return Ok(previsaoResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}


