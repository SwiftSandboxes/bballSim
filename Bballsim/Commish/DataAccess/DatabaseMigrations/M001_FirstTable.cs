using FluentMigrator;
 
namespace Bballsim.Commish.DatabaseAccess.DatabaseMigrations
{
    [Migration(001)]
    public class M001_script : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Teams")
                .WithColumn("Id").AsString().PrimaryKey()
                .WithColumn("Name").AsString()
                .WithColumn("Owner").AsString();
        }
    }
}