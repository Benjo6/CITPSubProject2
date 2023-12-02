using Common;
using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class PeopleServiceTests
{
    private readonly IPeopleRepository _peopleRepository;
    private readonly PeopleService _service;
    private Filter _filter;

    public PeopleServiceTests()
    {
        _peopleRepository = Substitute.For<IPeopleRepository>();
        _service = new PeopleService(_peopleRepository);
        _filter = new Filter();
    }

    [Fact]
    public async Task GetAllPerson_ReturnsAllPeople()
    {
        // Arrange
        var people = new List<Person> { new(), new() };
        _peopleRepository.GetAll(_filter).Returns(people);

        // Act
        var result = await _service.GetAllPerson(_filter);

        // Assert
        Assert.Equal(people.Count, result.Count);
    }

    [Fact]
    public async Task GetOnePerson_ReturnsPersonById()
    {
        // Arrange
        var personId = "1";
        var person = new Person();
        _peopleRepository.GetById(personId).Returns(person);

        // Act
        var result = await _service.GetOnePerson(personId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdatePerson_ReturnsUpdatedPerson()
    {
        // Arrange
        var personId = "1";
        var person = new Person();
        var alterPerson = new AlterPersonDTO();
        _peopleRepository.Update(Arg.Any<Person>()).Returns(true);
        _peopleRepository.GetById(personId).Returns(person);

        // Act
        var result = await _service.UpdatePerson(personId, alterPerson);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdatePerson_ReturnsEmptyPersonOnUpdateFailure()
    {
        // Arrange
        var personId = "1";
        var alterPerson = new AlterPersonDTO();
        _peopleRepository.GetById(Arg.Any<string>()).Returns(new Person());
        _peopleRepository.Update(Arg.Any<Person>()).Returns(false);

        // Act
        var result = await _service.UpdatePerson(personId, alterPerson);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(default, result.Id);
    }

    [Fact]
    public async Task AddPerson_ReturnsAddedPerson()
    {
        // Arrange
        var alterPerson = new AlterPersonDTO();
        var person = new Person();
        _peopleRepository.Add(Arg.Any<Person>()).Returns(person);

        // Act
        var result = await _service.AddPerson(alterPerson);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeletePerson_ReturnsTrueOnSuccess()
    {
        // Arrange
        var personId = "1";
        _peopleRepository.GetById(Arg.Any<string>()).Returns(new Person());
        _peopleRepository.Delete(Arg.Any<Person>()).Returns(true);

        // Act
        var result = await _service.DeletePerson(personId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task FindActorsByName_ReturnsActorsByName()
    {
        // Arrange
        var name = "ActorName";
        var actors = new List<ActorBy> { new(), new() };
        _peopleRepository.FindActorsByName(name).Returns(actors);

        // Act
        var result = await _service.FindActorsByName(name);

        // Assert
        Assert.Equal(actors.Count, result.Count);
    }

    [Fact]
    public async Task FindActorsByMovie_ReturnsActorsByMovie()
    {
        // Arrange
        var movieId = "MovieId";
        var actors = new List<ActorBy> { new(), new() };
        _peopleRepository.FindActorsByMovie(movieId).Returns(actors);

        // Act
        var result = await _service.FindActorsByMovie(movieId);

        // Assert
        Assert.Equal(actors.Count, result.Count);
    }

    [Fact]
    public async Task GetPopularActorsInMovie_ReturnsPopularActorsInMovie()
    {
        // Arrange
        var movieId = "MovieId";
        var actors = new List<PopularActor> { new(), new() };
        _peopleRepository.GetPopularActorsInMovie(movieId).Returns(actors);

        // Act
        var result = await _service.GetPopularActorsInMovie(movieId);

        // Assert
        Assert.Equal(actors.Count, result.Count);
    }

    [Fact]
    public async Task GetPopularCoPlayers_ReturnsPopularCoPlayers()
    {
        // Arrange
        var actorName = "ActorName";
        var actors = new List<PopularCoPlayer> { new(), new() };
        _peopleRepository.GetPopularCoPlayers(actorName).Returns(actors);

        // Act
        var result = await _service.GetPopularCoPlayers(actorName);

        // Assert
        Assert.Equal(actors.Count, result.Count);
    }

    [Fact]
    public async Task PersonWords_ReturnsPersonWords()
    {
        // Arrange
        var word = "Word";
        var frequency = 10;
        var actors = new List<PersonWord> { new(), new() };
        _peopleRepository.PersonWords(word, frequency).Returns(actors);

        // Act
        var result = await _service.PersonWords(word, frequency);

        // Assert
        Assert.Equal(actors.Count, result.Count);
    }

    [Fact]
    public async Task DeletePerson_ReturnsFalseOnDeleteError()
    {
        // Arrange
        var personId = "1";
        _peopleRepository.GetById(Arg.Any<string>()).Returns(new Person());
        _peopleRepository.Delete(Arg.Any<Person>()).Returns(false);

        // Act
        var result = await _service.DeletePerson(personId);

        // Assert
        Assert.False(result);
    }
}