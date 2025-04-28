using ClimaNotificacoesAPI.Application.Mappings;
using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using ClimaNotificacoesAPI.Infrastructure.Data;
using ClimaNotificacoesAPI.Infrastructure.Repositories;
using Mapster;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();
builder.Services.AddScoped<IPrevisaoTempoRepository, PrevisaoTempoRepository>();
builder.Services.AddScoped<PrevisaoTempoService>();

UsuarioProfile.ConfigureMappings();
CidadeProfile.ConfigureMappings();
PrevisaoProfile.ConfigureMappings();

builder.Services.AddDbContext<ClimaNotificacoesDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ClimaNotificacoesDBContext>();
    context.Database.Migrate();
}


app.UseHttpsRedirection();
app.MapControllers();
app.Urls.Add("http://0.0.0.0:80");
app.Run();
