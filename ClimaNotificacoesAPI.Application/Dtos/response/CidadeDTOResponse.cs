namespace ClimaNotificacoesAPI.Application.Dtos;

public class CidadeDTOResponse {
    public int Id { get; set; }
    public string Nome { get; set; }
    public int UsuarioId { get; set; }
    public UsuarioDTOResponse Usuario { get; set; } = new UsuarioDTOResponse();
}