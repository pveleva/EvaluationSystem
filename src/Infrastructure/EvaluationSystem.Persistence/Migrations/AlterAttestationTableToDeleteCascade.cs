using FluentMigrator;

namespace EvaluationSystem.Persistence.Migrations
{
    [Migration(202201061505)]
    public class AlterAttestationTableToDeleteCascade : Migration
    {
        public override void Up()
        {
            Execute.Sql("ALTER TABLE Attestation DROP CONSTRAINT FK_Attestation_IdForm_AttestationForm_Id;");
            Execute.Sql("ALTER TABLE Attestation DROP CONSTRAINT FK_Attestation_IdUserToEvaluate_User_Id;");

            Execute.Sql("ALTER TABLE Attestation ADD CONSTRAINT FK_Attestation_IdForm_AttestationForm_Id FOREIGN KEY(IdForm) REFERENCES AttestationForm(Id) ON DELETE CASCADE;");
            Execute.Sql("ALTER TABLE Attestation ADD CONSTRAINT FK_Attestation_IdUserToEvaluate_User_Id FOREIGN KEY(IdUserToEvaluate) REFERENCES User(Id) ON DELETE CASCADE;");
        }
        public override void Down()
        {
            Execute.Sql("ALTER TABLE Attestation ADD CONSTRAINT FK_Attestation_IdForm_AttestationForm_Id FOREIGN KEY(IdForm) REFERENCES AttestationForm(Id);");
            Execute.Sql("ALTER TABLE Attestation ADD CONSTRAINT FK_Attestation_IdUserToEvaluate_User_Id FOREIGN KEY(IdUserToEvaluate) REFERENCES User(Id);");

            Execute.Sql("ALTER TABLE Attestation DROP CONSTRAINT FK_Attestation_IdForm_AttestationForm_Id;");
            Execute.Sql("ALTER TABLE Attestation DROP CONSTRAINT FK_Attestation_IdUserToEvaluate_User_Id;");
        }
    }
}
