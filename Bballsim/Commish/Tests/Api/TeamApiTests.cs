using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Bballsim.Commish.DataAccess.Models;
using Bballsim.Commish.Services;
using Bballsim.Commish.Views;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net;

namespace Bballsim.Commish.Tests.Api
{
    public class TeamApiTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;


        public TeamApiTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();

        }

        [Fact]
        public async Task CanSaveNewTeam()
        {
            Team batmen = new Team();
            batmen.TeamName = "Batmen";
            batmen.OwnerName = "Wayne";

            HttpResponseMessage httpResponse = await CallCreate(batmen);

            // Must be successful.
            Assert.True(httpResponse.StatusCode.Equals(HttpStatusCode.Created));

        }

        [Fact]
        public async Task CanGetTeams()
        {
            // first ensure the team checked for is present
            Team aTeam = new Team();
            aTeam.TeamName = "ATeam";
            aTeam.OwnerName = "Hannibal";
            var httpCreateResponse = await CallCreate(aTeam);
            httpCreateResponse.EnsureSuccessStatusCode();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/team");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<IEnumerable<Team>>(stringResponse);
            Assert.Contains(players, p => p.TeamName == "ATeam");
        }

        private async Task<HttpResponseMessage> CallCreate(Team team)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpResponse = await _client.PostAsync("/team", httpContent);
            return httpResponse;
        }
    }
}
