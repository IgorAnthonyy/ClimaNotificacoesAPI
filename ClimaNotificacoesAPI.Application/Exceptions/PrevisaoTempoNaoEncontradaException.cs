namespace ClimaNotificacoesAPI.Application.Exceptions;

public class PrevisaoTempoNaoEncontradaException : Exception
{
    public PrevisaoTempoNaoEncontradaException(string message) : base(message) {}
}
