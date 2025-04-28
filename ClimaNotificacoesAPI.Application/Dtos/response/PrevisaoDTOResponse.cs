namespace ClimaNotificacoesAPI.Application.Dtos;

public class PrevisaoDTOResponse 
{
    public DateTime Data { get; set; }
    public string Condicao { get; set; }
    public double TemperaturaMaxima { get; set; }
    public double TemperaturaMinima { get; set; }
    public double Umidade { get; set; }
    public double VelocidadeVento { get; set; }
}