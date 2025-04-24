using ClimaNotificacoesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Reservas.Infra.Data.Configurations;

namespace ClimaNotificacoesAPI.Infrastructure.Data;

public class ClimaNotificacoesDBContext : DbContext
{
    public ClimaNotificacoesDBContext(DbContextOptions<ClimaNotificacoesDBContext> options)
        : base(options)
    {
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<PrevisaoTempo> PrevisaoTempos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new CidadeConfiguration());
        modelBuilder.ApplyConfiguration(new PrevisaoTempoConfiguration());

    }
}
