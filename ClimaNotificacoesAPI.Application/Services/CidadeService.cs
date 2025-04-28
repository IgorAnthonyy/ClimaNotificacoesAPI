using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;

namespace ClimaNotificacoesAPI.Application.Services;

public class CidadeService
{
    private readonly ICidadeRepository _cidadeRepository;
    public CidadeService(ICidadeRepository cidadeRepository)
    {
        _cidadeRepository = cidadeRepository;
    }
    public async Task<Cidade> GetByIdAsync(int id)
    {
        return await _cidadeRepository.GetByIdAsync(id);
    }
    public async Task<Cidade> CreateAsync(Cidade cidade)
    {
        return await _cidadeRepository.AddAsync(cidade);
    }
    public async Task DeleteAsync(int id)
    {
        await _cidadeRepository.DeleteAsync(id);
    }
}