using Common;
using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Tests.Repositories;

public class GenericRepositoryTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly GenericRepository<Episode> _repository;

    public GenericRepositoryTests(WebAppFactoryFixture fixture)
    {
        // Assigning the passed in WebAppFactoryFixture instance to the private field
        _repository = new GenericRepository<Episode>(fixture.Context);
    }
    
    [Fact]
    public async Task TestGenericRepository()
    {
        await GetAll_ReturnsAllEntities();
        await Add_ReturnsAddedEntity();
        await GetById_ReturnsEntityById();
        await GetById_ThrowsKeyNotFoundExceptionForNonexistentEntity();
    }

    private async Task Add_ReturnsAddedEntity()
    {
        // Arrange
        var episode = new Episode {             
            SeriesId = "1",
            Season = 1,
            Episode1 = 2 };
        
        // Act
        var addedEpisode = await _repository.Add(episode);

        // Assert
        Assert.Equal(episode.Episode1, addedEpisode.Episode1);
    }
    

    private async Task GetAll_ReturnsAllEntities()
    {
        // Arrange

        // Act
        var episodes = await _repository.GetAll(new Filter());

        // Assert
        Assert.True(episodes.Count == 3);
    }

    private async Task GetById_ReturnsEntityById()
    {
        // Act
        var retrievedEpisode = await _repository.GetById("Episode1");

        // Assert
        Assert.Equal("Episode1", retrievedEpisode.Id);
    }

    private async Task GetById_ThrowsKeyNotFoundExceptionForNonexistentEntity()
    {
        // Act and Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetById("2"));
    }
}