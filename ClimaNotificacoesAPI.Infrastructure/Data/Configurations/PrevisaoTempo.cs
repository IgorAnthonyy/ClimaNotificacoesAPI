using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClimaNotificacoesAPI.Domain.Entities;

namespace Reservas.Infra.Data.Configurations;

public class PrevisaoTempoConfiguration : IEntityTypeConfiguration<PrevisaoTempo>
{
    public void Configure(EntityTypeBuilder<PrevisaoTempo> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Data).IsRequired();
        builder.Property(u => u.Condicao).IsRequired().HasMaxLength(100);
        builder.Property(u => u.TemperaturaMaxima).IsRequired();
        builder.Property(u => u.TemperaturaMinima).IsRequired();
        builder.Property(u => u.Umidade).IsRequired();
        builder.Property(u => u.VelocidadeVento).IsRequired();

        builder.HasOne(u => u.Cidade)
            .WithMany(u => u.PrevisaoTempos)
            .HasForeignKey(u => u.CidadeId)
            .OnDelete(DeleteBehavior.Cascade);; 
        
    }
}