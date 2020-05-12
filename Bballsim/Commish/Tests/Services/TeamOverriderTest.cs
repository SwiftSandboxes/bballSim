using Bballsim.Commish.Services;
using Xunit;

namespace Commish.Tests.Services
{
    public class TeamDataManagerTest
    {
        private readonly ITeamService _teamOverrider;

        public TeamDataManagerTest()
        {
            // TODO: Add mocking framework and inject mock
            _teamOverrider = new TeamService();
        }

        [Fact]
        public void TestGetById()
        {
            var result = _teamOverrider.getById(1);

            Assert.Equal("Hannibal", result.OwnerId);
        }
    }
}