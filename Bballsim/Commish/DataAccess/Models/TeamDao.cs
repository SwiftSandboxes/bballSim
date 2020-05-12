using System.ComponentModel.DataAnnotations.Schema;

namespace Bballsim.Commish.DataAccess.Models
{
    [Table("TEAMS")]
    public class TeamDao
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public string OwnerId { get; set; }
    }
}