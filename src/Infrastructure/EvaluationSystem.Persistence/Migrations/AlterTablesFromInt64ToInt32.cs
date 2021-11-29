using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202111291101)]
    public class AlterTablesFromInt64ToInt32 : Migration
    {
        public override void Up()
        {
            Execute.Sql("ALTER TABLE AnswerTemplate DROP CONSTRAINT FK_AnswerTemplate_IdQuestion_QuestionTemplate_Id;");
            Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdQuestion_QuestionTemplate_Id;");
            Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdModule_ModuleTemplate_Id;");
            Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdModule_ModuleTemplate_Id;");
            Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdForm_FormTemplate_Id;");

            Execute.Sql("ALTER TABLE AnswerTemplate DROP CONSTRAINT PK_AnswerTemplate;");
            Execute.Sql("ALTER TABLE AnswerTemplate DROP COLUMN Id;");

            Execute.Sql("ALTER TABLE QuestionTemplate DROP CONSTRAINT PK_QuestionTemplate;");
            Execute.Sql("ALTER TABLE QuestionTemplate DROP COLUMN Id;");

            Execute.Sql("ALTER TABLE ModuleTemplate DROP CONSTRAINT PK_ModuleTemplate;");
            Execute.Sql("ALTER TABLE ModuleTemplate DROP COLUMN Id;");

            Execute.Sql("ALTER TABLE FormTemplate DROP CONSTRAINT PK_FormTemplate;");
            Execute.Sql("ALTER TABLE FormTemplate DROP COLUMN Id;");

            Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT PK_ModuleQuestion;");
            Execute.Sql("ALTER TABLE ModuleQuestion DROP COLUMN Id;");

            Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT PK_FormModule;");
            Execute.Sql("ALTER TABLE FormModule DROP COLUMN Id;");

            Alter.Table("FormModule")
                     .AddColumn("Id").AsInt32().PrimaryKey().Identity()
                     .AlterColumn("IdForm").AsInt32()
                     .AlterColumn("IdModule").AsInt32()
                     .AlterColumn("Position").AsInt32();

            Alter.Table("ModuleQuestion")
                    .AddColumn("Id").AsInt32().PrimaryKey().Identity()
                    .AlterColumn("IdModule").AsInt32()
                    .AlterColumn("IdQuestion").AsInt32();

            Alter.Table("FormTemplate")
                    .AddColumn("Id").AsInt32().PrimaryKey().Identity();

            Alter.Table("ModuleTemplate")
                    .AddColumn("Id").AsInt32().PrimaryKey().Identity();

            Alter.Table("QuestionTemplate")
                    .AddColumn("Id").AsInt32().PrimaryKey().Identity();

            Alter.Table("AnswerTemplate")
                    .AddColumn("Id").AsInt32().PrimaryKey().Identity()
                    .AlterColumn("Position").AsInt32().NotNullable()
                    .AlterColumn("IdQuestion").AsInt32().NotNullable();

            Alter.Table("AnswerTemplate")
                    .AddColumn("Id").AsInt32().PrimaryKey().Identity();

            Alter.Table("AnswerTemplate")
                    .AlterColumn("Position").AsInt32().NotNullable()
                    .AlterColumn("IdQuestion").AsInt32().NotNullable();

            //Execute.Sql("ALTER TABLE AnswerTemplate ADD PRIMARY KEY (Id);");
            //Execute.Sql("ALTER TABLE QuestionTemplate ADD PRIMARY KEY (Id);");
            //Execute.Sql("ALTER TABLE ModuleTemplate ADD PRIMARY KEY (Id);");
            //Execute.Sql("ALTER TABLE FormTemplate ADD PRIMARY KEY (Id);");
            //Execute.Sql("ALTER TABLE ModuleQuestion ADD PRIMARY KEY (Id);");
            //Execute.Sql("ALTER TABLE FormModule ADD PRIMARY KEY (Id);");

            Execute.Sql("ALTER TABLE AnswerTemplate ADD CONSTRAINT FK_AnswerQuestion FOREIGN KEY(IdQuestion) REFERENCES QuestionTemplate(Id);");
            Execute.Sql("ALTER TABLE ModuleQuestion ADD CONSTRAINT FK_ModuleQuestionTemplate FOREIGN KEY(IdQuestion) REFERENCES QuestionTemplate(Id);");
            Execute.Sql("ALTER TABLE ModuleQuestion ADD CONSTRAINT FK_ModuleModuleTemplate FOREIGN KEY(IdModule) REFERENCES ModuleTemplate(Id);");
            Execute.Sql("ALTER TABLE FormModule ADD CONSTRAINT FK_FormModuleTemplate FOREIGN KEY(IdModule) REFERENCES ModuleTemplate(Id);");
            Execute.Sql("ALTER TABLE FormModule ADD CONSTRAINT FK_FormFormTemplate FOREIGN KEY(IdForm) REFERENCES FormTemplate(Id);");
        }

        public override void Down()
        {
        }
    }
}
