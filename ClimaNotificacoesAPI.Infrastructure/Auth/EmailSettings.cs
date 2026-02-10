namespace ClimaNotificacoesAPI.Infrastructure.Auth;

public class EmailSettings
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
}
