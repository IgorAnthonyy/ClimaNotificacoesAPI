namespace ClimaNotificacoesAPI.Application.Exceptions;

public class UsuarioNaoEncontradoException : Exception
{
    public UsuarioNaoEncontradoException() : base("Usuário não encontrado.") {}
}