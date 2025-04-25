using ClimaNotificacoesAPI.Application.Dtos;
using ClimaNotificacoesAPI.Domain.Entities;
using Mapster;
namespace ClimaNotificacoesAPI.Application.Mappings;

public static class UsuarioProfile
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<Usuario, UsuarioDTOResponse>.NewConfig();
        TypeAdapterConfig<UsuarioDTORequest, Usuario>.NewConfig();
    }
}