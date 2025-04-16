using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClimaNotificacoesAPI.Domain.Entities;

namespace Reservas.Infra.Data.Configurations;

public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
{
    public void Configure(EntityTypeBuilder<Cidade> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
        builder.HasOne(u => u.Usuario)
            .WithMany(u => u.Cidades)
            .HasForeignKey(u => u.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}