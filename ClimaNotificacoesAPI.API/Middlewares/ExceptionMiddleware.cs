using ClimaNotificacoesAPI.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace ClimaNotificacoesAPI.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Continua a execução normal
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro capturado no middleware de exceções.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var statusCode = ex switch
        {
            EmailJaCadastradoException => HttpStatusCode.Conflict,
            UsuarioNaoEncontradoException => HttpStatusCode.NotFound,
            CredencialIncorretaException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new
        {
            erro = ex.Message
        });

        return context.Response.WriteAsync(result);
    }
}
