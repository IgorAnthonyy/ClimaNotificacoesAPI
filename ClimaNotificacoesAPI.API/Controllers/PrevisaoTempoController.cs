using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClimaNotificacoesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoTempoController : ControllerBase
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly PrevisaoTempoService _previsaoTempoService;

        public PrevisaoTempoController(PrevisaoTempoService previsaoTempoService, ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
            _previsaoTempoService = previsaoTempoService;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> ObterPrevisao(int id)
        {
            var cidade = await _cidadeRepository.GetByIdAsync(id);

            if (cidade == null)
            {
                return BadRequest("Cidade não encontrada.");
            }

            try
            {
                var previsaoResponse = await _previsaoTempoService.BuscarEAtualizarPrevisaoAsync(cidade);
                return Ok(previsaoResponse);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao obter previsão do tempo: {ex.Message}");
            }
        }
    }
}
