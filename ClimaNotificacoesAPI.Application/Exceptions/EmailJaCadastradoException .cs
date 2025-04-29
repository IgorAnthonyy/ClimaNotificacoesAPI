namespace ClimaNotificacoesAPI.Application.Exceptions;

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException(string email)
        : base($"Já existe outro usuário com o e-mail {email}.") { }
}
