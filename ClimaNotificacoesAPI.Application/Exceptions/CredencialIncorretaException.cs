namespace ClimaNotificacoesAPI.Application.Exceptions;

public class CredencialIncorretaException : Exception
{
    public CredencialIncorretaException()
        : base("Credenciais incorretas") {}
}
