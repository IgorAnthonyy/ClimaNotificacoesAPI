using AutoFixture;
using AutoFixture.Xunit2;
using ClimaNotificacoesAPI.Application.Exceptions;
using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClimaNotificacoesAPI.Tests.Services;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioRepository> _mockRepository;
    private readonly UsuarioService _service;
    private readonly Fixture _fixture;

    public UsuarioServiceTests()
    {
        _mockRepository = new Mock<IUsuarioRepository>();
        _service = new UsuarioService(_mockRepository.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUsuario_WhenUsuarioExists()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        _mockRepository.Setup(r => r.GetByIdAsync(usuario.Id))
            .ReturnsAsync(usuario);

        // Act
        var result = await _service.GetByIdAsync(usuario.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(usuario);
        _mockRepository.Verify(r => r.GetByIdAsync(usuario.Id), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenUsuarioDoesNotExist()
    {
        // Arrange
        var userId = 999;
        _mockRepository.Setup(r => r.GetByIdAsync(userId))
            .ReturnsAsync((Usuario)null);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(userId);

        // Assert
        await act.Should().ThrowAsync<UsuarioNaoEncontradoException>();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateUsuario_WhenEmailIsUnique()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .With(u => u.Senha, "SenhaForte123!")
            .Create();
        
        _mockRepository.Setup(r => r.GetByEmailAsync(usuario.Email))
            .ReturnsAsync((Usuario)null);
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Usuario>()))
            .ReturnsAsync(usuario);

        // Act
        var result = await _service.CreateAsync(usuario);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(usuario.Email);
        _mockRepository.Verify(r => r.GetByEmailAsync(usuario.Email), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        var existingUsuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        var newUsuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .With(u => u.Email, existingUsuario.Email)
            .Create();
        
        _mockRepository.Setup(r => r.GetByEmailAsync(newUsuario.Email))
            .ReturnsAsync(existingUsuario);

        // Act
        Func<Task> act = async () => await _service.CreateAsync(newUsuario);

        // Assert
        await act.Should().ThrowAsync<EmailJaCadastradoException>();
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Usuario>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUsuario_WhenUsuarioExists()
    {
        // Arrange
        var existingUsuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        var updatedUsuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .With(u => u.Id, existingUsuario.Id)
            .With(u => u.Email, "newemail@test.com")
            .Create();
        
        _mockRepository.Setup(r => r.GetByIdAsync(existingUsuario.Id))
            .ReturnsAsync(existingUsuario);
        _mockRepository.Setup(r => r.GetByEmailAsync(updatedUsuario.Email))
            .ReturnsAsync((Usuario)null);
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Usuario>()))
            .ReturnsAsync(updatedUsuario);

        // Act
        var result = await _service.UpdateAsync(updatedUsuario);

        // Assert
        result.Should().NotBeNull();
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteUsuario_WhenUsuarioExists()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        _mockRepository.Setup(r => r.GetByIdAsync(usuario.Id))
            .ReturnsAsync(usuario);
        _mockRepository.Setup(r => r.DeleteAsync(usuario.Id))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(usuario.Id);

        // Assert
        _mockRepository.Verify(r => r.DeleteAsync(usuario.Id), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsuarios()
    {
        // Arrange
        var usuarios = new List<Usuario>
        {
            _fixture.Build<Usuario>().Without(u => u.Cidades).Create(),
            _fixture.Build<Usuario>().Without(u => u.Cidades).Create(),
            _fixture.Build<Usuario>().Without(u => u.Cidades).Create()
        };
        _mockRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(usuarios);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
        result.Should().BeEquivalentTo(usuarios);
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnUsuario_WhenEmailExists()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        _mockRepository.Setup(r => r.GetByEmailAsync(usuario.Email))
            .ReturnsAsync(usuario);

        // Act
        var result = await _service.GetByEmailAsync(usuario.Email);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(usuario.Email);
    }
}
