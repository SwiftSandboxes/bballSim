using Bballsim.Commish.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Bballsim.Commish.DatabaseAccess
{
    public class CommishDbContext : DbContext
    {
        public string ConnectionString { get; set; }

        public CommishDbContext(DbContextOptions<CommishDbContext> options) : base(options)
        {
        }

        public DbSet<TeamDao> Teams { get; set; }

    }
}