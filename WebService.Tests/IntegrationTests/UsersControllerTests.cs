using System.Net;
using System.Net.Http.Json;
using Common.DataTransferObjects;

namespace WebService.Tests.IntegrationTests;

public class UsersControllerTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly HttpClient _client;

    public UsersControllerTests(WebAppFactoryFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task TestUsersController()
    {
        await GetUsersReturnsCorrectStatusCodeAndContent();
        await GetUserReturnsCorrectStatusCodeAndContent();
    }

    private async Task GetUsersReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.GetAsync("/Users");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var users = await response.Content.ReadFromJsonAsync<List<UserDTO>>();
        Assert.True(users != null && users.Count > 0);
    }

    private async Task GetUserReturnsCorrectStatusCodeAndContent()
    {
        // Create expected user DTO based on your dummy data.
        var expectedUser = new UserDTO
        {
            Username = "user1",
            Email = "user1@example.com",
            Password = "password1",
        };

        var response = await _client.GetAsync("/Users/User1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var user = await response.Content.ReadFromJsonAsync<UserDTO>();
        Assert.Equal(expectedUser.Username, user.Username);
    }
    
}