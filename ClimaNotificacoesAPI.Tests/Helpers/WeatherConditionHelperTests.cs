using ClimaNotificacoesAPI.Application.Helpers;
using FluentAssertions;
using Xunit;

namespace ClimaNotificacoesAPI.Tests.Helpers;

public class WeatherConditionHelperTests
{
    [Theory]
    [InlineData("nublado", true)]
    [InlineData("chuva leve", true)]
    [InlineData("chuva", true)]
    [InlineData("trovoada", true)]
    [InlineData("neve", true)]
    [InlineData("NUBLADO", true)] // Case insensitive
    [InlineData("Chuva", true)] // Case insensitive
    public void RequiresAlert_ShouldReturnTrue_WhenConditionRequiresAlert(string condition, bool expected)
    {
        // Act
        var result = WeatherConditionHelper.RequiresAlert(condition);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("ensolarado", false)]
    [InlineData("céu limpo", false)]
    [InlineData("parcialmente nublado", false)]
    [InlineData("", false)]
    [InlineData("qualquer outra coisa", false)]
    public void RequiresAlert_ShouldReturnFalse_WhenConditionDoesNotRequireAlert(string condition, bool expected)
    {
        // Act
        var result = WeatherConditionHelper.RequiresAlert(condition);

        // Assert
        result.Should().Be(expected);
    }
}
