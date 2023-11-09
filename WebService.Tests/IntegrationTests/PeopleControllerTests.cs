using System.Net;
using System.Net.Http.Json;
using Common.DataTransferObjects;

namespace WebService.Tests.IntegrationTests;

public class PeopleControllerTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly HttpClient _client;

    public PeopleControllerTests(WebAppFactoryFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task TestPeopleController()
    {
        await GetPeopleReturnsCorrectStatusCodeAndContent();
        await GetPersonReturnsCorrectStatusCodeAndContent();
        await GetPersonReturnsNotFoundForInvalidId();
        await PostPersonReturnsCorrectStatusCodeAndContent();
        await PutPersonReturnsCorrectStatusCodeAndContent();
        await DeletePersonReturnsCorrectStatusCodeAndContent();
    }

    private async Task GetPeopleReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.GetAsync("/People");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var people = await response.Content.ReadFromJsonAsync<List<GetAllPersonDTO>>();
        Assert.True(people!.Count == 3); 
    }

    private async Task GetPersonReturnsCorrectStatusCodeAndContent()
    {
        // Create expected person DTO based on your dummy data.
        var expectedPerson = new GetOnePersonDTO()
        {
            Name = "John Doe",
            BirthYear = "1975",
            DeathYear = "2021",
            Professions = "Actor, Director",
            KnownFor = "The Shawshank Redemption, The Godfather"
        };

        var response = await _client.GetAsync("/People/Person1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var person = await response.Content.ReadFromJsonAsync<GetOnePersonDTO>();
        Assert.Equal(expectedPerson.Name, person.Name);
    }

    private async Task GetPersonReturnsNotFoundForInvalidId()
    {
        var response = await _client.GetAsync("/People/invalid_id");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task PostPersonReturnsCorrectStatusCodeAndContent()
    {
        // Create a person DTO to post to the controller.
        var person = new AlterPersonDTO
        {
            Name = "Leo",
            BirthYear = "1985",
            DeathYear = null,
            Professions = "Actor",
            KnownFor = "The King"
                
        };

        var response = await _client.PostAsJsonAsync("/People", person);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var addedPerson = await response.Content.ReadFromJsonAsync<UpdatePersonDTO>();
        Assert.Equal(addedPerson!.Name, person.Name); // Check other properties as well.
    }

    private async Task PutPersonReturnsCorrectStatusCodeAndContent()
    {
        // Create a person DTO to update the existing person with ID 1.
        var person = new AlterPersonDTO
        {
            Name = "Paul Man",
            BirthYear = "1975",
            DeathYear = "2021",
            Professions = "Actor, Director",
            KnownFor = "The Shawshank Redemption, The Godfather"
        };

        var response = await _client.PutAsJsonAsync("/People/Person1", person);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var updatedPerson = await response.Content.ReadFromJsonAsync<UpdatePersonDTO>();
            Assert.Equal(person.Name, updatedPerson?.Name); // Check other properties as well.
        }
    }

    private async Task DeletePersonReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.DeleteAsync("/People/Person2");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}