using ClimaNotificacoesAPI.Application.Mappings;  // Importando mapeamentos de perfis
using ClimaNotificacoesAPI.Application.Services;  // Importando serviços da aplicação
using ClimaNotificacoesAPI.Domain.Entities;  // Importando as entidades do domínio
using ClimaNotificacoesAPI.Domain.Interfaces;  // Importando interfaces dos repositórios
using ClimaNotificacoesAPI.Infrastructure.Data;  // Importando contexto do banco de dados
using ClimaNotificacoesAPI.Infrastructure.Repositories;  // Importando implementações de repositórios
using Mapster;  // Importando Mapster, usado para mapeamento de objetos

using Microsoft.EntityFrameworkCore;  // Importando bibliotecas para trabalhar com Entity Framework

var builder = WebApplication.CreateBuilder(args);  // Criando o builder da aplicação Web

// Adicionando os serviços necessários para o controlador de APIs
builder.Services.AddControllers();  // Adiciona suporte a controllers (APIs) no pipeline
builder.Services.AddEndpointsApiExplorer();  // Habilita a descoberta de endpoints na documentação
builder.Services.AddSwaggerGen();  // Habilita o Swagger para documentação da API

// Registrando os repositórios e serviços necessários no container de dependências (DI)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();  // Registrando o repositório de usuários
builder.Services.AddScoped<UsuarioService>();  // Registrando o serviço de usuários
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();  // Registrando o repositório de cidades
builder.Services.AddScoped<CidadeService>();  // Registrando o serviço de cidades
builder.Services.AddScoped<IPrevisaoTempoRepository, PrevisaoTempoRepository>();  // Registrando o repositório de previsões de tempo
builder.Services.AddScoped<PrevisaoTempoService>();  // Registrando o serviço de previsões de tempo
builder.Services.AddHttpClient<WeatherService>();  // Registrando o serviço para consultar o clima
builder.Services.AddScoped<WeatherService>();  // Registrando a implementação do serviço WeatherService
builder.Services.AddHostedService<PrevisaoTempoJob>();  // Registrando o serviço em segundo plano para atualização das previsões de tempo

// Configurando os mapeamentos de objetos para as entidades (Mapster)
UsuarioProfile.ConfigureMappings();  // Configura mapeamento de Usuario
CidadeProfile.ConfigureMappings();  // Configura mapeamento de Cidade
PrevisaoProfile.ConfigureMappings();  // Configura mapeamento de Previsao

// Registrando o contexto do banco de dados e configurando o provedor de banco de dados (SQL Server)
builder.Services.AddDbContext<ClimaNotificacoesDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Configura a conexão com o banco de dados SQL Server usando a string de conexão definida no appsettings.json

var app = builder.Build();  // Construi a aplicação web a partir das configurações definidas

if (app.Environment.IsDevelopment())  // Verifica se o ambiente de execução é "Desenvolvimento"
{
    app.UseSwagger();  // Ativa o Swagger para documentação da API
    app.UseSwaggerUI();  // Ativa a interface do Swagger UI
}

// Realiza a migração do banco de dados ao iniciar a aplicação
using (var scope = app.Services.CreateScope())  // Cria um escopo para obter o contexto do banco de dados
{
    var context = scope.ServiceProvider.GetRequiredService<ClimaNotificacoesDBContext>();  // Obtém o contexto do banco de dados
    try
    {
        context.Database.Migrate();  // Aplica qualquer migração pendente no banco de dados
    }
    catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 1801)  // Caso o banco de dados já exista
    {
        Console.WriteLine("Banco de dados já existe. Ignorando erro 1801.");
    }
    catch (Exception ex)  // Em caso de qualquer outro erro
    {
        Console.WriteLine($"Erro ao aplicar migrations: {ex.Message}");  // Exibe a mensagem de erro
        throw;  // Lança a exceção para ser tratada em um nível superior
    }
}

app.UseHttpsRedirection();  // Força redirecionamento de HTTP para HTTPS
app.MapControllers();  // Mapear os endpoints dos controladores (APIs)
app.Urls.Add("http://0.0.0.0:80");  // Configura a URL para a aplicação escutar (adicionando uma URL alternativa para a API)
app.Run();  // Inicia a aplicação e começa a escutar requisições HTTP
