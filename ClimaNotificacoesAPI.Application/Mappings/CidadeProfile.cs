using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Domain.Entities;
using Mapster;
namespace ClimaNotificacoesAPI.Application.Mappings;

public static class CidadeProfile
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<Cidade, CidadeDTOResponse>.NewConfig();
        TypeAdapterConfig<CidadeDTORequest, Cidade>.NewConfig();
        TypeAdapterConfig<Usuario, UsuarioDTOResponse>.NewConfig();
    }
}