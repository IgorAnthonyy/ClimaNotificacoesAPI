using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using ClimaNotificacoesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClimaNotificacoesAPI.Infrastructure.Repositories;

public class CidadeRepository : GenericRepository<Cidade>, ICidadeRepository
{
    public CidadeRepository(ClimaNotificacoesDBContext context) : base(context)
    {
    }

    public override async Task<Cidade> GetByIdAsync(int id)
    {
        return await _context.Cidades
         .Include(c => c.Usuario)
         .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<PrevisaoTempo>> GetPrevisaoTempoByCidadeAsync(int cidadeId)
    {
        return await _context.PrevisaoTempos
            .Where(p => p.CidadeId == cidadeId)
            .Include(p => p.Cidade)
            .ToListAsync();
    }
}
