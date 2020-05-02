using FluentMigrator;
 
namespace Bballsim.Commish.DatabaseMigrations
{
    [Migration(2020041601)]
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