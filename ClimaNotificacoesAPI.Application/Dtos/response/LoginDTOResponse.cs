namespace ClimaNotificacoesAPI.Application.Dtos;
public class LoginDTOResponse
{
    public string Token { get; set; }
    public UsuarioDTOResponse Usuario { get; set; }
}