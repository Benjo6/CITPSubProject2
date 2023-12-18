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

public class PeopleControllerTests
{
    private readonly IPeopleService _service;
    private readonly PeopleController _controller;

    public PeopleControllerTests()
    {
        _service = Substitute.For<IPeopleService>();
        _controller = new PeopleController(_service);

        var urlHelper = Substitute.For<IUrlHelper>();
        urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("callbackUrl");
        _controller.Url = urlHelper;
    }

    [Fact]
    public async Task GetPeople_ReturnsOkResult()
    {
        // Arrange
        var expectedPeople = new List<GetAllPersonDTO>();
        _service.GetAllPerson(Arg.Any<Filter>()).Returns(expectedPeople);

        // Act
        var result = await _controller.GetPeople();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<PeopleResult>(json);
        var returnedPeopleDTOs = data.People.Select(a => a.Person).ToList();
        Assert.Equal(expectedPeople, returnedPeopleDTOs);
    }

    [Fact]
    public async Task GetPerson_ReturnsOkResult()
    {
        // Arrange
        var personId = "1";
        var expectedPerson = new GetOnePersonDTO();
        _service.GetOnePerson(personId).Returns(expectedPerson);

        // Act
        var result = await _controller.GetPerson(personId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<PeopleWithUri>(json);
        var model = data.Person;
        Assert.Equal(expectedPerson.Id, model.Id);
    }

    [Fact]
    public async Task PutPerson_ReturnsOkResult()
    {
        // Arrange
        var personId = "1";
        var person = new AlterPersonDTO();
        var updatedPerson = new UpdatePersonDTO();
        _service.UpdatePerson(personId, person).Returns(updatedPerson);

        // Act
        var result = await _controller.PutPerson(personId, person);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<PeopleWithUri>(json);
        var model = data.Person;
        Assert.Equal(updatedPerson.Id, model.Id);
    }

    [Fact]
    public async Task FindActorsByName_ReturnsOkResult()
    {
        // Arrange
        var name = "John";
        var expectedActors = new List<ActorBy>(); // Adjust as needed
        _service.FindActorsByName(name).Returns(expectedActors);

        // Act
        var result = await _controller.FindActorsByName(name);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<ActorByResult>(json);
        Assert.Equal(expectedActors, data.Actors);
    }

    [Fact]
    public async Task FindActorsByMovie_ReturnsOkResult()
    {
        // Arrange
        var movieId = "123";
        var expectedActors = new List<ActorBy>(); // Adjust as needed
        _service.FindActorsByMovie(movieId).Returns(expectedActors);

        // Act
        var result = await _controller.FindActorsByMovie(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<ActorByResult>(json);
        Assert.Equal(expectedActors, data.Actors);

    }

    [Fact]
    public async Task GetPopularCoPlayers_ReturnsOkResult()
    {
        // Arrange
        var actorName = "John Doe";
        var expectedActors = new List<PopularCoPlayer>(); // Adjust as needed
        _service.GetPopularCoPlayers(actorName).Returns(expectedActors);

        // Act
        var result = await _controller.GetPopularCoPlayers(actorName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<CoActorsResult>(json);
        Assert.Equal(expectedActors, data.Actors);
    }

    [Fact]
    public async Task PostPerson_ReturnsOkResult()
    {
        // Arrange
        var person = new AlterPersonDTO();
        var createdPerson = new UpdatePersonDTO();
        _service.AddPerson(person).Returns(createdPerson);

        // Act
        var result = await _controller.PostPerson(person);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<PeopleWithUri>(json);
        var model = data.Person;
        Assert.Equal(createdPerson.Id, model.Id);
    }

    [Fact]
    public async Task DeletePerson_ReturnsOkResult()
    {
        // Arrange
        var personId = "1";
        var deletionResult = true; // Adjust as needed
        _service.DeletePerson(personId).Returns(deletionResult);

        // Act
        var result = await _controller.DeletePerson(personId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<PeopleBooleanResult>(json);
        Assert.True(data.Result);
    }

    [Fact]
    public async Task GetPeople_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.GetAllPerson(Arg.Any<Filter>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetPeople();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task GetPerson_ReturnsBadRequestOnError()
    {
        // Arrange
        var personId = "1";
        _service.GetOnePerson(personId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetPerson(personId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task PutPerson_ReturnsBadRequestOnError()
    {
        // Arrange
        var personId = "1";
        var person = new AlterPersonDTO();
        _service.UpdatePerson(personId, person).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PutPerson(personId, person);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task FindActorsByName_ReturnsBadRequestOnError()
    {
        // Arrange
        var name = "John";
        _service.FindActorsByName(name).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.FindActorsByName(name);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task FindActorsByMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var movieId = "123";
        _service.FindActorsByMovie(movieId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.FindActorsByMovie(movieId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task GetPopularCoPlayers_ReturnsBadRequestOnError()
    {
        // Arrange
        var actorName = "John Doe";
        _service.GetPopularCoPlayers(actorName).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetPopularCoPlayers(actorName);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task PostPerson_ReturnsBadRequestOnError()
    {
        // Arrange
        var person = new AlterPersonDTO();
        _service.AddPerson(person).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PostPerson(person);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task DeletePerson_ReturnsBadRequestOnError()
    {
        // Arrange
        var personId = "1";
        _service.DeletePerson(personId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeletePerson(personId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }
}
public class PeopleWithUri
{
    public GetAllPersonDTO Person { get; set; }
    public string Uri { get; set; }
}

public class PeopleResult
{
    public IEnumerable<PeopleWithUri> People { get; set; }
    public string Uri { get; set; }
}

public class ActorByResult
{
    public IEnumerable<ActorBy> Actors { get; set; }
    public string Uri { get; set; }
}

public class CoActorsResult
{
    public IEnumerable<PopularCoPlayer> Actors { get; set; }
    public string Uri { get; set; }
}



public class PeopleBooleanResult
{
    public bool Result { get; set; }
    public string Uri { get; set; }
}
