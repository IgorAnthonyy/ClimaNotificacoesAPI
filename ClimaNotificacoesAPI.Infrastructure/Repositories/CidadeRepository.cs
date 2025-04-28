using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using ClimaNotificacoesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClimaNotificacoesAPI.Infrastructure.Repositories;

public class CidadeRepository : ICidadeRepository
{
    private readonly ClimaNotificacoesDBContext _context;
    public CidadeRepository(ClimaNotificacoesDBContext context)
    {
        _context = context;
    }

    public async Task<Cidade> GetByIdAsync(int id)
    {
        return await _context.Cidades
         .Include(c => c.Usuario)
         .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cidade> AddAsync(Cidade cidade)
    {
        await _context.Cidades.AddAsync(cidade);
        await _context.SaveChangesAsync();
        return cidade;
    }
    public async Task DeleteAsync(int id)
    {
        var cidade = await GetByIdAsync(id);
        if (cidade != null)
        {
            _context.Cidades.Remove(cidade);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<PrevisaoTempo>> GetPrevisaoTempoByCidadeAsync(int cidadeId)
    {
        return await _context.PrevisaoTempos
            .Where(p => p.CidadeId == cidadeId)
            .Include(p => p.Cidade)
            .ToListAsync();
    }
    public async Task<List<Cidade>> GetAllAsync()
    {
        return await _context.Cidades
            .ToListAsync();
    }
}