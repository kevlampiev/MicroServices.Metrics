using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("cpumetrics").WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt32();
        }

        public override void Down()
        {
            Delete.Table("cpumetrics");
        }
    }
}
