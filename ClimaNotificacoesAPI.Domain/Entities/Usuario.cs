namespace ClimaNotificacoesAPI.Domain.Entities;
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }

    public ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();

}