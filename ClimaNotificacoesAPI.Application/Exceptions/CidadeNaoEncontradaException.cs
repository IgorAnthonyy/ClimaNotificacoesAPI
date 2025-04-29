namespace ClimaNotificacoesAPI.Application.Exceptions;

public class CidadeNaoEncontradaException : Exception
{
    public CidadeNaoEncontradaException(string palavra) : base($"{palavra} não encontrado.") {}
}