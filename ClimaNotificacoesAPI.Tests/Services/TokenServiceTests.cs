using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Infrastructure.Auth;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace ClimaNotificacoesAPI.Tests.Services;

public class TokenServiceTests
{
    private readonly TokenService _service;
    private readonly JwtSettings _jwtSettings;

    public TokenServiceTests()
    {
        _jwtSettings = new JwtSettings
        {
            Key = "minha-chave-super-secreta-para-testes-minima-32-caracteres",
            Issuer = "test-issuer",
            Audience = "test-audience",
            ExpireMinutes = 60
        };

        var options = Options.Create(_jwtSettings);
        _service = new TokenService(options);
    }

    [Fact]
    public void GenerateToken_ShouldReturnValidJwtToken_WhenUsuarioIsValid()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = 1,
            Nome = "Test User",
            Email = "test@example.com",
            Senha = "SenhaForte123!",
            Telefone = "11999999999"
        };

        // Act
        var token = _service.GenerateToken(usuario);

        // Assert
        token.Should().NotBeNullOrEmpty();
        
        // Validate token structure
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        jwtToken.Should().NotBeNull();
        jwtToken.Issuer.Should().Be(_jwtSettings.Issuer);
        jwtToken.Audiences.Should().Contain(_jwtSettings.Audience);
    }

    [Fact]
    public void GenerateToken_ShouldIncludeCorrectClaims_WhenUsuarioIsValid()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = 123,
            Nome = "João Silva",
            Email = "joao@example.com",
            Senha = "SenhaForte123!",
            Telefone = "11999999999"
        };

        // Act
        var token = _service.GenerateToken(usuario);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        var claims = jwtToken.Claims.ToList();
        claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == "123");
        claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "João Silva");
    }

    [Fact]
    public void GenerateToken_ShouldSetCorrectExpiration_WhenUsuarioIsValid()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = 1,
            Nome = "Test User",
            Email = "test@example.com",
            Senha = "SenhaForte123!",
            Telefone = "11999999999"
        };
        var beforeGeneration = DateTime.UtcNow;

        // Act
        var token = _service.GenerateToken(usuario);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        var expectedExpiration = beforeGeneration.AddMinutes(_jwtSettings.ExpireMinutes);
        jwtToken.ValidTo.Should().BeCloseTo(expectedExpiration, TimeSpan.FromSeconds(10));
    }

    [Fact]
    public void GenerateToken_ShouldGenerateDifferentTokens_ForDifferentUsuarios()
    {
        // Arrange
        var usuario1 = new Usuario { Id = 1, Nome = "User 1", Email = "user1@test.com", Senha = "senha", Telefone = "11111111111" };
        var usuario2 = new Usuario { Id = 2, Nome = "User 2", Email = "user2@test.com", Senha = "senha", Telefone = "22222222222" };

        // Act
        var token1 = _service.GenerateToken(usuario1);
        var token2 = _service.GenerateToken(usuario2);

        // Assert
        token1.Should().NotBe(token2);
    }
}
