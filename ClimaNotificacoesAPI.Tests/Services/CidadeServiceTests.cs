using AutoFixture;
using ClimaNotificacoesAPI.Application.Exceptions;
using ClimaNotificacoesAPI.Application.Services;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClimaNotificacoesAPI.Tests.Services;

public class CidadeServiceTests
{
    private readonly Mock<ICidadeRepository> _mockCidadeRepository;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly CidadeService _service;
    private readonly Fixture _fixture;

    public CidadeServiceTests()
    {
        _mockCidadeRepository = new Mock<ICidadeRepository>();
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        var usuarioService = new UsuarioService(_mockUsuarioRepository.Object);
        _service = new CidadeService(_mockCidadeRepository.Object, usuarioService);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCidade_WhenCidadeExists()
    {
        // Arrange
        var cidade = _fixture.Build<Cidade>()
            .Without(c => c.PrevisaoTempos)
            .Without(c => c.Usuario)
            .Create();
        _mockCidadeRepository.Setup(r => r.GetByIdAsync(cidade.Id))
            .ReturnsAsync(cidade);

        // Act
        var result = await _service.GetByIdAsync(cidade.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(cidade);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenCidadeDoesNotExist()
    {
        // Arrange
        var cidadeId = 999;
        _mockCidadeRepository.Setup(r => r.GetByIdAsync(cidadeId))
            .ReturnsAsync((Cidade)null);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(cidadeId);

        // Assert
        await act.Should().ThrowAsync<CidadeNaoEncontradaException>();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateCidade_WhenCidadeIsUnique()
    {
        // Arrange
        var cidade = _fixture.Build<Cidade>()
            .Without(c => c.PrevisaoTempos)
            .Without(c => c.Usuario)
            .Create();
        var usuario = _fixture.Build<Usuario>()
            .With(u => u.Id, cidade.UsuarioId)
            .Without(u => u.Cidades)
            .Create();
        var cidades = new List<Cidade>();
        
        _mockUsuarioRepository.Setup(r => r.GetByIdAsync(cidade.UsuarioId))
            .ReturnsAsync(usuario);
        _mockUsuarioRepository.Setup(r => r.GetCidadesByUsuarioIdAsync(cidade.UsuarioId))
            .ReturnsAsync(cidades);
        _mockCidadeRepository.Setup(r => r.AddAsync(It.IsAny<Cidade>()))
            .ReturnsAsync(cidade);

        // Act
        var result = await _service.CreateAsync(cidade);

        // Assert
        result.Should().NotBeNull();
        result.Nome.Should().Be(cidade.Nome);
        _mockCidadeRepository.Verify(r => r.AddAsync(It.IsAny<Cidade>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenCidadeAlreadyExistsForUser()
    {
        // Arrange
        var cidade = _fixture.Build<Cidade>()
            .Without(c => c.PrevisaoTempos)
            .Without(c => c.Usuario)
            .Create();
        var usuario = _fixture.Build<Usuario>()
            .With(u => u.Id, cidade.UsuarioId)
            .Without(u => u.Cidades)
            .Create();
        var existingCidades = new List<Cidade>
        {
            _fixture.Build<Cidade>()
                .Without(c => c.PrevisaoTempos)
                .Without(c => c.Usuario)
                .With(c => c.Nome, cidade.Nome)
                .With(c => c.UsuarioId, cidade.UsuarioId)
                .Create()
        };
        
        _mockUsuarioRepository.Setup(r => r.GetByIdAsync(cidade.UsuarioId))
            .ReturnsAsync(usuario);
        _mockUsuarioRepository.Setup(r => r.GetCidadesByUsuarioIdAsync(cidade.UsuarioId))
            .ReturnsAsync(existingCidades);

        // Act
        Func<Task> act = async () => await _service.CreateAsync(cidade);

        // Assert
        await act.Should().ThrowAsync<CidadeJaCadastradaParaEsseUsuarioException>();
        _mockCidadeRepository.Verify(r => r.AddAsync(It.IsAny<Cidade>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCidade_WhenCidadeExists()
    {
        // Arrange
        var cidade = _fixture.Build<Cidade>()
            .Without(c => c.PrevisaoTempos)
            .Without(c => c.Usuario)
            .Create();
        _mockCidadeRepository.Setup(r => r.GetByIdAsync(cidade.Id))
            .ReturnsAsync(cidade);
        _mockCidadeRepository.Setup(r => r.DeleteAsync(cidade.Id))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(cidade.Id);

        // Assert
        _mockCidadeRepository.Verify(r => r.DeleteAsync(cidade.Id), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCidades()
    {
        // Arrange
        var cidades = new List<Cidade>
        {
            _fixture.Build<Cidade>().Without(c => c.PrevisaoTempos).Without(c => c.Usuario).Create(),
            _fixture.Build<Cidade>().Without(c => c.PrevisaoTempos).Without(c => c.Usuario).Create(),
            _fixture.Build<Cidade>().Without(c => c.PrevisaoTempos).Without(c => c.Usuario).Create()
        };
        _mockCidadeRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(cidades);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
        result.Should().BeEquivalentTo(cidades);
    }

    [Fact]
    public async Task GetPrevisaoTempoByCidadeAsync_ShouldReturnPrevisoes_WhenCidadeExists()
    {
        // Arrange
        var cidade = _fixture.Build<Cidade>()
            .Without(c => c.PrevisaoTempos)
            .Without(c => c.Usuario)
            .Create();
        var previsoes = new List<PrevisaoTempo>
        {
            _fixture.Build<PrevisaoTempo>().Without(p => p.Cidade).Create(),
            _fixture.Build<PrevisaoTempo>().Without(p => p.Cidade).Create()
        };
        
        _mockCidadeRepository.Setup(r => r.GetByIdAsync(cidade.Id))
            .ReturnsAsync(cidade);
        _mockCidadeRepository.Setup(r => r.GetPrevisaoTempoByCidadeAsync(cidade.Id))
            .ReturnsAsync(previsoes);

        // Act
        var result = await _service.GetPrevisaoTempoByCidadeAsync(cidade.Id);

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(previsoes);
    }
}
