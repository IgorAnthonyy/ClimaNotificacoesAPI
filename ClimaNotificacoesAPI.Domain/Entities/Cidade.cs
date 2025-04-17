namespace ClimaNotificacoesAPI.Domain.Entities;

public class Cidade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<PrevisaoTempo> PrevisaoTempos { get; set; } = new List<PrevisaoTempo>();

}