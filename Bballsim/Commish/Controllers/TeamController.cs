using System;
using Bballsim.Commish.DataAccess.Models;
using Bballsim.Commish.Services;
using Bballsim.Commish.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bballsim.Commish.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TeamController> _logger;
        private readonly ITeamService _teamService;

        public TeamController(ILogger<TeamController> logger, ITeamService teamOverrider)
        {
            _logger = logger;
            _teamService = teamOverrider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_teamService.getTeams());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Team requestedTeam)
        {
            TeamDao teamDao = new TeamDao();
            teamDao.Id = Guid.NewGuid().ToString();
            teamDao.TeamName = requestedTeam.TeamName;
            teamDao.OwnerId = requestedTeam.OwnerName;
            _teamService.saveEntity(teamDao);
            return Created(new Uri("/team/"), requestedTeam);
        }
    }
}
