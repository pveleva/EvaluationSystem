using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202201041734)]
    public class AlterTablesToDeleteCascade : Migration
    {
        public override void Up()
        {
            Execute.Sql("ALTER TABLE AnswerTemplate DROP CONSTRAINT FK_AnswerTemplate_IdQuestion_QuestionTemplate_Id;");
            Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdModule_ModuleTemplate_Id;");
            Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdQuestion_QuestionTemplate_Id");
            Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdForm_FormTemplate_Id;");
            Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdModule_ModuleTemplate_Id;");

            Execute.Sql("ALTER TABLE AnswerTemplate ADD CONSTRAINT FK_QuestionAnswer FOREIGN KEY(IdQuestion) REFERENCES QuestionTemplate(Id) ON DELETE CASCADE;");
            Execute.Sql("ALTER TABLE ModuleQuestion ADD CONSTRAINT FK_ModuleModuleQuestion FOREIGN KEY(IdModule) REFERENCES ModuleTemplate(Id) ON DELETE CASCADE;");
            Execute.Sql("ALTER TABLE ModuleQuestion ADD CONSTRAINT FK_QuestionQuestion FOREIGN KEY(IdQuestion) REFERENCES QuestionTemplate(Id) ON DELETE CASCADE;");
            Execute.Sql("ALTER TABLE FormModule ADD CONSTRAINT FK_FormForm FOREIGN KEY(IdForm) REFERENCES FormTemplate(Id) ON DELETE CASCADE;");
            Execute.Sql("ALTER TABLE FormModule ADD CONSTRAINT FK_FormModuleModule FOREIGN KEY(IdModule) REFERENCES ModuleTemplate(Id) ON DELETE CASCADE;");
        }

        public override void Down()
        {
            Execute.Sql("ALTER TABLE AnswerTemplate ADD CONSTRAINT FK_AnswerTemplate_IdQuestion_QuestionTemplate_Id FOREIGN KEY(IdQuestion) REFERENCES QuestionTemplate(Id);");
            Execute.Sql("ALTER TABLE ModuleQuestion ADD CONSTRAINT FK_ModuleQuestion_IdModule_ModuleTemplate_Id FOREIGN KEY(IdModule) REFERENCES ModuleTemplate(Id);");
            Execute.Sql("ALTER TABLE ModuleQuestion ADD CONSTRAINT FK_ModuleQuestion_IdQuestion_QuestionTemplate_Id FOREIGN KEY(IdQuestion) REFERENCES QuestionTemplate(Id);");
            Execute.Sql("ALTER TABLE FormModule ADD CONSTRAINT FK_FormModule_IdForm_FormTemplate_Id FOREIGN KEY(IdForm) REFERENCES FormTemplate(Id);");
            Execute.Sql("ALTER TABLE FormModule ADD CONSTRAINT FK_ModuleQuestion_IdModule_ModuleTemplate_Id FOREIGN KEY(IdModule) REFERENCES ModuleTemplate(Id);");

            Execute.Sql("ALTER TABLE AnswerTemplate DROP CONSTRAINT FK_QuestionAnswer;");
            Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleModuleQuestion");
            Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_QuestionQuestion");
            Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormForm;");
            Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModuleModule;");
        }
    }
}
