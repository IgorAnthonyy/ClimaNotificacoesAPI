using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using ClimaNotificacoesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClimaNotificacoesAPI.Infrastructure.Repositories;

public class PrevisaoTempoRepository : IPrevisaoTempoRepository
{

    private readonly ClimaNotificacoesDBContext _context;

    public PrevisaoTempoRepository(ClimaNotificacoesDBContext context)
    {
        _context = context;
    }

    public async Task<PrevisaoTempo> AddAsync(PrevisaoTempo previsaoTempo)
    {
        _context.PrevisaoTempos.Add(previsaoTempo);
        await _context.SaveChangesAsync();
        return previsaoTempo;

    }

}