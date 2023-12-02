using Common;
using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Services;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class EpisodesServiceTests
{
    private readonly IGenericRepository<Episode> _repository;
    private readonly EpisodesService _service;
    private Filter _filter;

    public EpisodesServiceTests()
    {
        _repository = Substitute.For<IGenericRepository<Episode>>();
        _service = new EpisodesService(_repository);
        _filter = new Filter();
    }

    [Fact]
    public async Task GetAllEpisodes_ReturnsAllEpisodes()
    {
        // Arrange
        var episodes = new List<Episode> { new(), new() };
        _repository.GetAll(_filter).Returns(episodes);

        // Act
        var result = await _service.GetAllEpisodes(_filter);

        // Assert
        Assert.Equal(episodes.Count, result.Count);
    }

    [Fact]
    public async Task GetOneEpisode_ReturnsEpisodeById()
    {
        // Arrange
        var episodeId = "1";
        var episode = new Episode();
        _repository.GetById(episodeId).Returns(episode);

        // Act
        var result = await _service.GetOneEpisode(episodeId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateEpisode_ReturnsUpdatedEpisode()
    {
        // Arrange
        var episodeId = "1";
        var episode = new Episode();
        var alterEpisode = new AlterEpisodeDTO();
        _repository.Update(Arg.Any<Episode>()).Returns(true);
        _repository.GetById(episodeId).Returns(episode);

        // Act
        var result = await _service.UpdateEpisode(episodeId, alterEpisode);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateEpisode_ReturnsEmptyEpisodeOnUpdateFailure()
    {
        // Arrange
        var episodeId = "1";
        var alterEpisode = new AlterEpisodeDTO();
        _repository.Update(Arg.Any<Episode>()).Returns(false);
        _repository.GetById(Arg.Any<string>()).Returns(new Episode());

        // Act
        var result = await _service.UpdateEpisode(episodeId, alterEpisode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(default, result.Id);
    }

    [Fact]
    public async Task AddEpisode_ReturnsAddedEpisode()
    {
        // Arrange
        var alterEpisode = new AlterEpisodeDTO();
        var episode = new Episode();
        _repository.Add(Arg.Any<Episode>()).Returns(episode);

        // Act
        var result = await _service.AddEpisode(alterEpisode);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteEpisode_ReturnsTrueOnSuccess()
    {
        // Arrange
        var episodeId = "1";
        _repository.GetById(Arg.Any<string>()).Returns(new Episode());

        _repository.Delete(Arg.Any<Episode>()).Returns(true);

        // Act
        var result = await _service.DeleteEpisode(episodeId);

        // Assert
        Assert.True(result);
    }
}