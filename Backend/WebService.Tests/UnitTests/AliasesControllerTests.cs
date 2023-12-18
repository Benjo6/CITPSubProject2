using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebService.Controllers;

namespace WebService.Tests.UnitTests;

public class AliasesControllerTests
{
    private readonly IAliasesService _service;
    private readonly AliasesController _controller;

    public AliasesControllerTests()
    {
        _service = Substitute.For<IAliasesService>();
        _controller = new AliasesController(_service);

        var urlHelper = Substitute.For<IUrlHelper>();
        urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("callbackUrl");
        _controller.Url = urlHelper;
    }

    [Fact]
    public async Task GetAliases_ReturnsOkResult()
    {
        // Arrange
        var expectedAliases = new List<AliasDTO>();
        _service.GetAllAliases(Arg.Any<Filter>()).Returns(expectedAliases);

        // Act
        var result = await _controller.GetAliases();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<AliasesResult>(json);
        List<AliasDTO> returnedAliasDTOs = data.Aliases.Select(a => a.Alias).ToList();
        Assert.Equal(expectedAliases, returnedAliasDTOs);
    }

    [Fact]
    public async Task GetAlias_ReturnsOkResult()
    {
        // Arrange
        var aliasId = "1";
        var expectedAlias = new AliasDTO();
        _service.GetOneAlias(aliasId).Returns(expectedAlias);

        // Act
        var result = await _controller.GetAlias(aliasId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<AliasWithUri>(json);
        var model = data.Alias;
        Assert.Equal(expectedAlias.Id, model.Id);
    }

    [Fact]
    public async Task PutAlias_ReturnsOkResult()
    {
        // Arrange
        var aliasId = "1";
        var alias = new AlterAliasDTO();
        var updatedAlias = new AliasDTO();
        _service.UpdateAlias(aliasId, alias).Returns(updatedAlias);

        // Act
        var result = await _controller.PutAlias(aliasId, alias);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<AliasWithUri>(json);
        var model = data.Alias;
        Assert.Equal(updatedAlias.Id, model.Id);
    }

    [Fact]
    public async Task PostAlias_ReturnsOkResult()
    {
        // Arrange
        var alias = new AlterAliasDTO();
        var createdAlias = new AliasDTO();
        _service.AddAlias(alias).Returns(createdAlias);

        // Act
        var result = await _controller.PostAlias(alias);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<AliasWithUri>(json);
        var model = data.Alias;
        Assert.Equal(createdAlias.Id, model.Id);
    }

    [Fact]
    public async Task DeleteAlias_ReturnsOkResult()
    {
        // Arrange
        var aliasId = "1";
        var deletionResult = true; // Adjust as needed
        _service.DeleteAlias(aliasId).Returns(deletionResult);

        // Act
        var result = await _controller.DeleteAlias(aliasId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(deletionResult, okResult.Value);
    }

    [Fact]
    public async Task GetAliases_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.GetAllAliases(Arg.Any<Filter>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetAliases();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task PutAlias_ReturnsBadRequestOnError()
    {
        // Arrange
        var aliasId = "1";
        var alias = new AlterAliasDTO();
        _service.UpdateAlias(aliasId, alias).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PutAlias(aliasId, alias);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task PostAlias_ReturnsBadRequestOnError()
    {
        // Arrange
        var alias = new AlterAliasDTO();
        _service.AddAlias(alias).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PostAlias(alias);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task DeleteAlias_ReturnsBadRequestOnError()
    {
        // Arrange
        var aliasId = "1";
        _service.DeleteAlias(aliasId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteAlias(aliasId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

}

public class AliasWithUri
{
    public AliasDTO Alias { get; set; }
    public string Uri { get; set; }
}

public class AliasesResult
{
    public IEnumerable<AliasWithUri> Aliases { get; set; }
    public string Uri { get; set; }
}
