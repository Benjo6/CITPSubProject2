using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebService.Controllers;

namespace WebService.Tests.UnitTests;

public class EpisodesControllerTests
{
    private readonly IEpisodesService _service;
    private readonly EpisodesController _controller;

    public EpisodesControllerTests()
    {
        _service = Substitute.For<IEpisodesService>();
        _controller = new EpisodesController(_service);
    }

    [Fact]
    public async Task GetEpisodes_ReturnsOkResult()
    {
        // Arrange
        var expectedEpisodes = new List<EpisodeDTO>();
        _service.GetAllEpisodes(Arg.Any<Filter>()).Returns(expectedEpisodes);

        // Act
        var result = await _controller.GetEpisodes();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<EpisodeDTO>>(okResult.Value);
        Assert.Equal(expectedEpisodes, model);
    }

    [Fact]
    public async Task GetEpisode_ReturnsOkResult()
    {
        // Arrange
        var episodeId = "1";
        var expectedEpisode = new EpisodeDTO();
        _service.GetOneEpisode(episodeId).Returns(expectedEpisode);

        // Act
        var result = await _controller.GetEpisode(episodeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<EpisodeDTO>(okResult.Value);
        Assert.Equal(expectedEpisode, model);
    }

    [Fact]
    public async Task PutEpisode_ReturnsOkResult()
    {
        // Arrange
        var episodeId = "1";
        var episode = new AlterEpisodeDTO();
        var updatedEpisode = new EpisodeDTO();
        _service.UpdateEpisode(episodeId, episode).Returns(updatedEpisode);

        // Act
        var result = await _controller.PutEpisode(episodeId, episode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<EpisodeDTO>(okResult.Value);
        Assert.Equal(updatedEpisode, model);
    }

    [Fact]
    public async Task PostEpisode_ReturnsOkResult()
    {
        // Arrange
        var episode = new AlterEpisodeDTO();
        var createdEpisode = new EpisodeDTO();
        _service.AddEpisode(episode).Returns(createdEpisode);

        // Act
        var result = await _controller.PostEpisode(episode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<EpisodeDTO>(okResult.Value);
        Assert.Equal(createdEpisode, model);
    }

    [Fact]
    public async Task DeleteEpisode_ReturnsOkResult()
    {
        // Arrange
        var episodeId = "1";
        var deletionResult = true; // Adjust as needed
        _service.DeleteEpisode(episodeId).Returns(deletionResult);

        // Act
        var result = await _controller.DeleteEpisode(episodeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(deletionResult, okResult.Value);
    }
    
    [Fact]
    public async Task GetEpisodes_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.GetAllEpisodes(Arg.Any<Filter>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetEpisodes();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
    
    [Fact]
    public async Task PutEpisode_ReturnsBadRequestOnError()
    {
        // Arrange
        var episodeId = "1";
        var episode = new AlterEpisodeDTO();
        _service.UpdateEpisode(episodeId, episode).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PutEpisode(episodeId, episode);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task PostEpisode_ReturnsBadRequestOnError()
    {
        // Arrange
        var episode = new AlterEpisodeDTO();
        _service.AddEpisode(episode).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PostEpisode(episode);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteEpisode_ReturnsBadRequestOnError()
    {
        // Arrange
        var episodeId = "1";
        _service.DeleteEpisode(episodeId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteEpisode(episodeId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
}