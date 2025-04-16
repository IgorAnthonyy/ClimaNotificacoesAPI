namespace ClimaNotificacoesAPI.Domain.Entities;

public class PrevisaoTempo
{
    public int Id { get; set; }
    public string Cidade { get; set; }
    public DateTime Data { get; set; }
    public string Condicao { get; set; }
    public double TemperaturaMaxima { get; set; }
    public double TemperaturaMinima { get; set; }
    public double Umidade { get; set; }
    public double VelocidadeVento { get; set; }
    public int CidadeId { get; set; }
    public Cidade cidade { get; set; }
}