using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Bballsim.Commish.DataAccess.Models;
using Bballsim.Commish.DatabaseAccess;
using System;

namespace Bballsim.Commish.Services
{
    public interface ITeamOverrider
    {
        TeamDao getById(int id);
        List<TeamDao> getTeams();
    }

    public class TeamOverrider : ITeamOverrider
    {
        private Dictionary<int, TeamDao> teamsCache = new Dictionary<int, TeamDao>();
        private readonly CommishDbContext _dbContext;

        public TeamOverrider(CommishDbContext dbcontext)
        {
            _dbContext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }


        // TODO: Remove once mocking framework in place. Temporary for Unit Testing
        public TeamOverrider()
        {
            TeamDao aTeam = new TeamDao();
            aTeam.Id = 1;
            aTeam.TeamName = "ATeam";
            aTeam.OwnerId = "Hannibal";
            aTeam.LeagueId = "Eighties";
            teamsCache.TryAdd(aTeam.Id, aTeam);
        }

        public List<TeamDao> getTeams()
        {
            return _dbContext.Teams.ToList();
        }
        public TeamDao getById(int id)
        {
            return teamsCache[id];
        }
    }
}