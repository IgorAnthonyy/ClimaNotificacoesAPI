using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ClimaNotificacoesAPI.Infrastructure.Auth;

namespace ClimaNotificacoesAPI.Application.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendAlertEmailAsync(string destinatario, string nomeUsuario, string alerta, string cidade)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Alerta de Clima", _emailSettings.Username));
            email.To.Add(new MailboxAddress("", destinatario));
            email.Subject = "🚨 Alerta de Clima Ativado";

            var alertasHtml = $"<h2 style='margin-bottom: 5px;'>Na cidade <span style='color:#00ddff;'>{cidade}</span> está {alerta}</h2>";

            email.Body = new TextPart("html")
            {
                Text = $@"
                    <html>
                    <body style='background-color: #000; color: #fff; font-family: Arial, sans-serif; padding: 20px;'>
                        <h2 style='color:#00ddff;'>Olá, {nomeUsuario}!</h2>
                        {alertasHtml}
                        <p>Fique atento às notificações em seu e-mail sempre que ocorrer alguma condição climática crítica.</p>
                        <hr>
                        <p style='font-size: 12px; color: #888;'>Este é um e-mail automático. Por favor, não responda.</p>
                    </body>
                    </html>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
