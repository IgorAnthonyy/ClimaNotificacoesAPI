using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using ClimaNotificacoesAPI.Infrastructure.Data;

namespace ClimaNotificacoesAPI.Infrastructure.Repositories;

public class PrevisaoTempoRepository : GenericRepository<PrevisaoTempo>, IPrevisaoTempoRepository
{
    public PrevisaoTempoRepository(ClimaNotificacoesDBContext context) : base(context)
    {
    }
}
