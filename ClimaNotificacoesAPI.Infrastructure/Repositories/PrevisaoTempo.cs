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

    public async Task<IEnumerable<PrevisaoTempo>> GetAllAsync()
    {
        return await _context.PrevisaoTempos.ToListAsync();
    }

    public async Task<PrevisaoTempo> GetByIdAsync(int id)
    {
        return await _context.PrevisaoTempos.FindAsync(id);
    }

    public async Task<PrevisaoTempo> AddAsync(PrevisaoTempo previsaoTempo)
    {
        _context.PrevisaoTempos.Add(previsaoTempo);
        await _context.SaveChangesAsync();
        return previsaoTempo;

    }
    public async Task<PrevisaoTempo> UpdateAsync(PrevisaoTempo previsaoTempo)
    {
        _context.PrevisaoTempos.Update(previsaoTempo);
        await _context.SaveChangesAsync();
        return previsaoTempo;
    }
    public async Task DeleteAsync(int id)
    {
        var previsaoTempo = await GetByIdAsync(id);
        if (previsaoTempo != null)
        {
            _context.PrevisaoTempos.Remove(previsaoTempo);
            await _context.SaveChangesAsync();
        }
    }
}