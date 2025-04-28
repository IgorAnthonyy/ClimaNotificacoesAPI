using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClimaNotificacoesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoTempoController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly PrevisaoTempoService _previsaoTempoService;

        public PrevisaoTempoController(WeatherService weatherService, PrevisaoTempoService previsaoTempoService, ICidadeRepository cidadeRepository)
        {
            _weatherService = weatherService;
            _cidadeRepository = cidadeRepository;
            _previsaoTempoService = previsaoTempoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPrevisao(int id)
        {
            var cidade = await _cidadeRepository.GetByIdAsync(id);

            if (cidade == null)
            {
                return BadRequest("Cidade não encontrada.");
            }

            try
            {
                var previsaoJson = await _weatherService.ObterPrevisao(cidade.Nome);

                if (previsaoJson == null)
                {
                    return NotFound("Previsão do tempo não encontrada.");
                }

                var previsaoEntity = new PrevisaoTempo
                {
                    CidadeId = cidade.Id,
                    Data = (DateTime)previsaoJson["data"],
                    Condicao = (string)previsaoJson["condicao"],
                    TemperaturaMaxima = (double)previsaoJson["temperaturaMaxima"],
                    TemperaturaMinima = (double)previsaoJson["temperaturaMinima"],
                    Umidade = (double)previsaoJson["umidade"],
                    VelocidadeVento = (double)previsaoJson["velocidadeVento"]
                };

                var previsaoCriado = await _previsaoTempoService.CreateAsync(previsaoEntity);

                var previsaoResponse = previsaoCriado.Adapt<PrevisaoDTOResponse>();

                return Ok(previsaoResponse);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao obter previsão do tempo: {ex.Message}");
            }
        }
    }
}
