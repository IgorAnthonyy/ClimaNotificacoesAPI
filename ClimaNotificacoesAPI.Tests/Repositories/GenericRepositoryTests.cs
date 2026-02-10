using AutoFixture;
using ClimaNotificacoesAPI.Domain.Entities;
using ClimaNotificacoesAPI.Infrastructure.Data;
using ClimaNotificacoesAPI.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClimaNotificacoesAPI.Tests.Repositories;

public class GenericRepositoryTests
{
    private readonly ClimaNotificacoesDBContext _context;
    private readonly GenericRepository<Usuario> _repository;
    private readonly Fixture _fixture;

    public GenericRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ClimaNotificacoesDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ClimaNotificacoesDBContext(options);
        _repository = new GenericRepository<Usuario>(_context);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity_WhenEntityIsValid()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();

        // Act
        var result = await _repository.AddAsync(usuario);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        
        var savedEntity = await _context.Usuarios.FindAsync(result.Id);
        savedEntity.Should().NotBeNull();
        savedEntity.Nome.Should().Be(usuario.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(usuario.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(usuario.Id);
        result.Nome.Should().Be(usuario.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenEntityDoesNotExist()
    {
        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var usuarios = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .CreateMany(3)
            .ToList();
        
        await _context.Usuarios.AddRangeAsync(usuarios);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEntity_WhenEntityExists()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        
        _context.Entry(usuario).State = EntityState.Detached;

        usuario.Nome = "Nome Atualizado";

        // Act
        var result = await _repository.UpdateAsync(usuario);

        // Assert
        result.Nome.Should().Be("Nome Atualizado");
        
        var updatedEntity = await _context.Usuarios.FindAsync(usuario.Id);
        updatedEntity.Nome.Should().Be("Nome Atualizado");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEntity_WhenEntityExists()
    {
        // Arrange
        var usuario = _fixture.Build<Usuario>()
            .Without(u => u.Cidades)
            .Create();
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(usuario.Id);

        // Assert
        var deletedEntity = await _context.Usuarios.FindAsync(usuario.Id);
        deletedEntity.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ShouldDoNothing_WhenEntityDoesNotExist()
    {
        // Act
        Func<Task> act = async () => await _repository.DeleteAsync(999);

        // Assert
        await act.Should().NotThrowAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
