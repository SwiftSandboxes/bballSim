using Bballsim.Commish.Services;
using Xunit;

namespace Commish.Tests.Services
{
    public class TeamOverriderTest
    {
        private readonly ITeamOverrider _teamOverrider;

        public TeamOverriderTest()
        {
            _teamOverrider = new TeamOverrider();
        }

        [Fact]
        public void TestGetById()
        {
            var result = _teamOverrider.getById(1);

            Assert.Equal("Hannibal", result.OwnerId);
        }
    }
}