using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    //[Migration(202111291101)]
    //public class AlterTablesFromInt64ToInt32 : Migration
    //{
    //    public override void Up()
    //    {
    //        Execute.Sql("ALTER TABLE AnswerTemplate DROP CONSTRAINT FK_AnswerTemplate_IdQuestion_QuestionTemplate_Id; ");
    //        Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdQuestion_QuestionTemplate_Id;");
    //        Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdModule_ModuleTemplate_Id;");
    //        Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdModule_ModuleTemplate_Id;");
    //        Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdForm_FormTemplate_Id;");

    //        Execute.Sql("ALTER TABLE AnswerTemplate DROP CONSTRAINT PK_AnswerTemplate;");
    //        Execute.Sql("ALTER TABLE QuestionTemplate DROP CONSTRAINT PK_QuestionTemplate;");
    //        Execute.Sql("ALTER TABLE ModuleTemplate DROP CONSTRAINT PK_ModuleTemplate;");
    //        Execute.Sql("ALTER TABLE FormTemplate DROP CONSTRAINT PK_FormTemplate;");
    //        Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT PK_ModuleQuestion;");
    //        Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT PK_FormModule;");

    //        Alter.Table("FormModule")
    //                 .AlterColumn("Id").AsInt32()
    //                 .AlterColumn("IdForm").AsInt32()
    //                 .AlterColumn("IdModule").AsInt32()
    //                 .AlterColumn("Position").AsInt32();

    //        Alter.Table("ModuleQuestion")
    //                .AlterColumn("Id").AsInt32().PrimaryKey()
    //                .AlterColumn("IdModule").AsInt32()
    //                .AlterColumn("IdQuestion").AsInt32();

    //        Alter.Table("FormTemplate")
    //                .AlterColumn("Id").AsInt32().PrimaryKey();

    //        Alter.Table("ModuleTemplate")
    //                .AlterColumn("Id").AsInt32().PrimaryKey();

    //        Alter.Table("QuestionTemplate")
    //                .AlterColumn("Id").AsInt32().PrimaryKey();

    //        Alter.Table("AnswerTemplate")
    //                .AlterColumn("Id").AsInt32().PrimaryKey()
    //                .AlterColumn("Position").AsInt32().NotNullable()
    //                .AlterColumn("IdQuestion").AsInt32().NotNullable();

    //        Alter.Table("AnswerTemplate")
    //            .AlterColumn("Id").AsInt32().PrimaryKey()
    //            .AlterColumn("Position").AsInt32().NotNullable()
    //            .AlterColumn("IdQuestion").AsInt32().NotNullable();

    //        Execute.Sql("ALTER TABLE AnswerTemplate ADD PRIMARY KEY (Id);");
    //        Execute.Sql("ALTER TABLE QuestionTemplate ADD PRIMARY KEY (Id);");
    //        Execute.Sql("ALTER TABLE ModuleTemplate ADD PRIMARY KEY (Id);");
    //        Execute.Sql("ALTER TABLE FormTemplate ADD PRIMARY KEY (Id);");
    //        Execute.Sql("ALTER TABLE ModuleQuestion ADD PRIMARY KEY (Id);");
    //        Execute.Sql("ALTER TABLE FormModule ADD PRIMARY KEY (Id);");

    //        Execute.Sql("ALTER TABLE AnswerTemplate DROP CONSTRAINT FK_AnswerTemplate_IdQuestion_QuestionTemplate_Id; ");
    //        Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdQuestion_QuestionTemplate_Id;");
    //        Execute.Sql("ALTER TABLE ModuleQuestion DROP CONSTRAINT FK_ModuleQuestion_IdModule_ModuleTemplate_Id;");
    //        Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdModule_ModuleTemplate_Id;");
    //        Execute.Sql("ALTER TABLE FormModule DROP CONSTRAINT FK_FormModule_IdForm_FormTemplate_Id;");
    //    }

    //    public override void Down()
    //    {
    //    }
    //}
}
