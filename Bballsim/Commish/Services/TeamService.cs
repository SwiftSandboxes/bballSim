using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Bballsim.Commish.DataAccess.Models;
using Bballsim.Commish.DatabaseAccess;
using System;

namespace Bballsim.Commish.Services
{
    public interface ITeamService
    {
        TeamDao getById(int id);
        List<TeamDao> getTeams();
        void saveEntity(TeamDao teamDao);
    }

    public class TeamService : ITeamService
    {
        private Dictionary<int, TeamDao> teamsCache = new Dictionary<int, TeamDao>();
        private readonly CommishDbContext _dbContext;

        public TeamService(CommishDbContext dbcontext)
        {
            _dbContext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }


        // // TODO: Remove once mocking framework in place. Temporary for Unit Testing
        public TeamService()
        {
            TeamDao aTeam = new TeamDao();
            aTeam.Id = "1";
            aTeam.TeamName = "ATeam";
            aTeam.OwnerId = "Hannibal";
            teamsCache.TryAdd(1, aTeam);
        }

        public List<TeamDao> getTeams()
        {
            return _dbContext.Teams.ToList();
        }
        public TeamDao getById(int id)
        {
            return teamsCache[id];
        }

        public void saveEntity(TeamDao teamDao)
        {
            _dbContext.Add(teamDao);
            _dbContext.SaveChanges();
        }
    }
}