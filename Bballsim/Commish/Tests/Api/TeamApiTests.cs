using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bballsim.Commish.Views;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Bballsim.Commish.Tests.Api
{
    public class TeamApiTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TeamApiTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetTeams()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/team");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<IEnumerable<Team>>(stringResponse);
            Assert.Contains(players, p => p.TeamName=="BTeam");
        }
    }
}
