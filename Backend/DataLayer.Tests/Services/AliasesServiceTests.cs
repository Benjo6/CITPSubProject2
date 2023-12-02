using Common;
using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Services;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class AliasesServiceTests
{
    private readonly IGenericRepository<Alias> _repository;
    private readonly AliasesService _service;
    private Filter _filter;

    public AliasesServiceTests()
    {
        _repository = Substitute.For<IGenericRepository<Alias>>();
        _service = new AliasesService(_repository);
        _filter = new Filter();
    }

    [Fact]
    public async Task GetAllAliases_ReturnsAllAliases()
    {
        // Arrange
        var aliases = new List<Alias> { new(), new() };
        _repository.GetAll(_filter).Returns(aliases);

        // Act
        var result = await _service.GetAllAliases(_filter);

        // Assert
        Assert.Equal(aliases.Count, result.Count);
    }

    [Fact]
    public async Task GetOneAlias_ReturnsAliasById()
    {
        // Arrange
        var aliasId = "1";
        var alias = new Alias();
        _repository.GetById(aliasId).Returns(alias);

        // Act
        var result = await _service.GetOneAlias(aliasId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAlias_ReturnsUpdatedAlias()
    {
        // Arrange
        var aliasId = "1";
        var alias = new Alias();
        var alterAlias = new AlterAliasDTO();
        _repository.Update(Arg.Any<Alias>()).Returns(true);
        _repository.GetById(aliasId).Returns(alias);

        // Act
        var result = await _service.UpdateAlias(aliasId, alterAlias);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAlias_ReturnsTrueOnSuccess()
    {
        // Arrange
        var aliasId = "1";
        _repository.Delete(Arg.Any<Alias>()).Returns(true);

        // Act
        var result = await _service.DeleteAlias(aliasId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddAlias_ReturnsAddedAlias()
    {
        // Arrange
        var alterAlias = new AlterAliasDTO();
        var alias = new Alias();
        _repository.Add(Arg.Any<Alias>()).Returns(alias);

        // Act
        var result = await _service.AddAlias(alterAlias);

        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task DeleteAlias_ReturnsFalseOnDeleteError()
    {
        // Arrange
        var aliasId = "1";
        _repository.Delete(Arg.Any<Alias>()).Returns(false);

        // Act
        var result = await _service.DeleteAlias(aliasId);

        // Assert
        Assert.False(result);
    }
}
