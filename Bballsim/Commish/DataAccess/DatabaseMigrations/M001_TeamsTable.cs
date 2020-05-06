using FluentMigrator;
 
namespace Bballsim.Commish.DatabaseAccess.DatabaseMigrations
{
    [Migration(001)]
    public class M001_script : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("TEAMS")
                .WithColumn("Id").AsString().PrimaryKey()
                .WithColumn("TeamName").AsString()
                .WithColumn("OwnerId").AsString();
        }
    }
}