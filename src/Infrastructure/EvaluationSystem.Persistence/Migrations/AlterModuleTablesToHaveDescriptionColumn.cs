using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202201111405)]
    public class AlterModuleTablesToHaveDescriptionColumn : Migration
    {
        public override void Up()
        {
            Alter.Table("ModuleTemplate").AddColumn("Description").AsString(1024).Nullable();
            Alter.Table("AttestationModule").AddColumn("Description").AsString(1024).Nullable();
        }
        public override void Down()
        {
            Delete.Column("Description").FromTable("ModuleTemplate");
            Delete.Column("Description").FromTable("AttestationModule");
        }
    }
}
