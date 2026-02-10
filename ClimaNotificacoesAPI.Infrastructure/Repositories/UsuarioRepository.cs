using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using ClimaNotificacoesAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClimaNotificacoesAPI.Infrastructure.Repositories;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ClimaNotificacoesDBContext context) : base(context)
    {
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<Cidade>> GetCidadesByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Cidades.Where(c => c.UsuarioId == usuarioId)
        .ToListAsync();
    }
}
