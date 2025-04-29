namespace ClimaNotificacoesAPI.Application.Exceptions;

public class CidadeJaCadastradaParaEsseUsuarioException : Exception
{
    public CidadeJaCadastradaParaEsseUsuarioException(string nome)
        : base($"Já existe outra cidade com o nome {nome}.") {}
}
