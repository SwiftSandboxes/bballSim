using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bballsim.commish.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace commish.Controllers
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
            private readonly ITeamOverrider _teamOverrider;
    // public EmployeeController(IDataService dataService)
    // {
    //   _dataService = dataService;
    // }

        public TeamController(ILogger<TeamController> logger, ITeamOverrider teamOverrider)
        {
            _logger = logger;
            _teamOverrider = teamOverrider;
        }

        [HttpGet]
        public IActionResult Get()
        {
           return Ok(_teamOverrider.getTeams());
        }
    }
}
