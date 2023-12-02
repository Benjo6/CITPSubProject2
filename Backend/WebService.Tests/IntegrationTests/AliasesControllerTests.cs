using System.Net;
using System.Net.Http.Json;
using Common.DataTransferObjects;

namespace WebService.Tests.IntegrationTests
{
    public class AliasesControllerTests : IClassFixture<WebAppFactoryFixture>
    {
        private readonly HttpClient _client;

        public AliasesControllerTests(WebAppFactoryFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task TestAliasesController()
        {
            await GetAliasesReturnsCorrectStatusCodeAndContent();
            await GetAliasReturnsCorrectStatusCodeAndContent();
            await GetAliasReturnsNotFoundForInvalidId();
            await PostAliasReturnsCorrectStatusCodeAndContent();
            await DeleteAliasReturnsCorrectStatusCodeAndContent();
            await PutAliasReturnsCorrectStatusCodeAndContent();
        }

        private async Task GetAliasesReturnsCorrectStatusCodeAndContent()
        {
            var response = await _client.GetAsync("/Aliases");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var aliases = await response.Content.ReadFromJsonAsync<List<AliasDTO>>();
            Assert.NotNull(aliases);
        }

        private async Task GetAliasReturnsCorrectStatusCodeAndContent()
        {
            // Assume you have a valid alias ID to test
            var validAliasId = "1";

            var response = await _client.GetAsync($"/Aliases/{validAliasId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var alias = await response.Content.ReadFromJsonAsync<AliasDTO>();
            Assert.NotNull(alias);
        }

        private async Task GetAliasReturnsNotFoundForInvalidId()
        {
            // Use an invalid alias ID to test
            var invalidAliasId = "invalid_alias_id";

            var response = await _client.GetAsync($"/Aliases/{invalidAliasId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private async Task PutAliasReturnsCorrectStatusCodeAndContent()
        {
            // Assume you have a valid alias ID and an AlterAliasDTO to test
            var validAliasId = "2";
            var alterAlias = new AliasDTO(){  
                
                MovieId = "3",
                Ordering = 3,
                Title = "Alias 3",
                Region = "Region 3",
                Language = "French",
                Types = "Type 3",
                Attributes = "Attribute 3",
                IsOriginal = true};

            var response = await _client.PutAsJsonAsync($"/Aliases/{validAliasId}", alterAlias);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var updatedAlias = await response.Content.ReadFromJsonAsync<AliasDTO>();
            Assert.NotNull(updatedAlias);
        }

        private async Task PostAliasReturnsCorrectStatusCodeAndContent()
        {
            // Create an AlterAliasDTO to post to the controller.
            var alias = new AlterAliasDTO();

            var response = await _client.PostAsJsonAsync("/Aliases", alias);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var addedAlias = await response.Content.ReadFromJsonAsync<AliasDTO>();
            Assert.NotNull(addedAlias);
        }

        private async Task DeleteAliasReturnsCorrectStatusCodeAndContent()
        {
            // Assume you have a valid alias ID to delete
            var validAliasId = "1";

            var response = await _client.DeleteAsync($"/Aliases/{validAliasId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<bool>();
            Assert.True(result);
        }
    }
}
