using ClimaNotificacoesAPI.Application.Exceptions;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;

namespace ClimaNotificacoesAPI.Application.Services;

public class CidadeService
{
    private readonly UsuarioService _usuarioService;
    private readonly ICidadeRepository _cidadeRepository;
    public CidadeService(ICidadeRepository cidadeRepository, UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        _cidadeRepository = cidadeRepository;
    }
    public async Task<Cidade> GetByIdAsync(int id)
    {
        var cidade = await _cidadeRepository.GetByIdAsync(id);
        if (cidade == null)
        {
            throw new CidadeNaoEncontradaException("Cidade");
        }
        return cidade;

    }
    public async Task<Cidade> CreateAsync(Cidade cidade)
    {
        
        var cidadesUsuario = await _usuarioService.GetCidadesByUsuarioIdAsync(cidade.UsuarioId);
        var cidadeExistente = false;
        if (cidadesUsuario != null)
        {
            foreach (var cidadeIndividual in cidadesUsuario)
            {
                if (cidadeIndividual.Nome == cidade.Nome)
                {
                    cidadeExistente = true;
                    break;
                }
            }
        }
        if (cidadeExistente)
            throw new CidadeJaCadastradaParaEsseUsuarioException(cidade.Nome);
        return await _cidadeRepository.AddAsync(cidade);
    }
    public async Task DeleteAsync(int id)
    {
        var cidade = await GetByIdAsync(id);

        if (cidade == null)
        {
            throw new CidadeNaoEncontradaException("Cidade");
        }
        await _cidadeRepository.DeleteAsync(id);
    }
    public async Task<List<PrevisaoTempo>> GetPrevisaoTempoByCidadeAsync(int cidadeId)
    {
        var cidade = await GetByIdAsync(cidadeId);
        if (cidade == null)
        {
            throw new CidadeNaoEncontradaException("Cidade");
        }
        return await _cidadeRepository.GetPrevisaoTempoByCidadeAsync(cidadeId);
    }
    public async Task<List<Cidade>> GetAllAsync()
    {
        return await _cidadeRepository.GetAllAsync();
    }
}