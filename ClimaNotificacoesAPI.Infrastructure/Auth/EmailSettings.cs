using System.ComponentModel.DataAnnotations;

namespace ClimaNotificacoesAPI.Infrastructure.Auth;

public class EmailSettings
{
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public string SmtpServer { get; set; } = string.Empty;
    
    [Required]
    public int SmtpPort { get; set; }
}
