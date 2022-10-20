using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(2)]
    public class SecondMigration : Migration
    {
        public override void Up()
        {
            Create.Table("dotnetmetrics").WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt32();
            Create.Table("hddmetrics").WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt32();
            Create.Table("networkmetrics").WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt32();
            Create.Table("rammetrics").WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt32();
        }

        public override void Down()
        {
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
        }
    }
}
