using ClimaNotificacoesAPI.Application.Services;  // Importa os serviços necessários da aplicação
using Microsoft.Extensions.DependencyInjection;  // Usado para injeção de dependências (DI)
using Microsoft.Extensions.Hosting;  // Necessário para criar um serviço em segundo plano (BackgroundService)
using Microsoft.Extensions.Logging;  // Usado para registro de logs da aplicação

// A classe PrevisaoTempoJob herda de BackgroundService para ser executada em segundo plano
public class PrevisaoTempoJob : BackgroundService
{
    // Variáveis privadas para armazenar o provedor de serviços e o logger
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PrevisaoTempoJob> _logger;

    // O construtor recebe o IServiceProvider e o ILogger via injeção de dependência
    public PrevisaoTempoJob(IServiceProvider serviceProvider, ILogger<PrevisaoTempoJob> logger)
    {
        _serviceProvider = serviceProvider;  // Inicializa o _serviceProvider para obter outros serviços
        _logger = logger;  // Inicializa o _logger para registrar logs da execução
    }

    // O método ExecuteAsync é responsável por executar a lógica do job em segundo plano
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Um loop que vai rodar enquanto o token de cancelamento não for solicitado
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Atualizando previsões de tempo...");  // Registra no log que a atualização está começando

            // Cria um escopo para garantir que os serviços sejam liberados corretamente após o uso
            using (var scope = _serviceProvider.CreateScope())
            {
                // Obtém as instâncias dos serviços CidadeService e PrevisaoTempoService do container de dependências
                var cidadeService = scope.ServiceProvider.GetRequiredService<CidadeService>();
                var previsaoTempoService = scope.ServiceProvider.GetRequiredService<PrevisaoTempoService>();

                // Chama o serviço CidadeService para obter todas as cidades cadastradas no sistema
                var cidades = await cidadeService.GetAllAsync();

                // Para cada cidade, tenta atualizar a previsão do tempo
                foreach (var cidade in cidades)
                {
                    try
                    {
                        // Chama o serviço PrevisaoTempoService para buscar e atualizar a previsão de tempo para a cidade
                        await previsaoTempoService.BuscarEAtualizarPrevisaoAsync(cidade);

                        // Registra no log que a previsão foi atualizada com sucesso para essa cidade
                        _logger.LogInformation($"Previsão atualizada para {cidade.Nome}.");
                    }
                    catch (Exception ex)
                    {
                        // Caso ocorra um erro ao atualizar a previsão, registra o erro no log
                        _logger.LogError(ex, $"Erro ao atualizar previsão para a cidade {cidade.Nome}");
                    }
                }
            }

            // Espera 3 minutos antes de executar novamente, respeitando o token de cancelamento
            await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
        }
    }
}
