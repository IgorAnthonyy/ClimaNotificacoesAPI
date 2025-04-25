using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Domain.Entities;
using Mapster;
namespace ClimaNotificacoesAPI.Application.Mappings;

public static class PrevisaoProfile
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<PrevisaoTempo, PrevisaoDTOResponse>.NewConfig();
        TypeAdapterConfig<PrevisaoDTORequest, PrevisaoTempo>.NewConfig();
        TypeAdapterConfig<Cidade, CidadeDTOResponse>.NewConfig();
    }
}