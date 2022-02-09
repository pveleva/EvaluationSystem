using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Models.Forms
{
    public class GetFormModuleQuestionAnswerDto
    {
        public int IdForm { get; set; }
        public string NameForm { get; set; }
        public int ModulePosition { get; set; }
        public int IdModule { get; set; }
        public string NameModule { get; set; }
        public string Description { get; set; }
        public int QuestionPosition { get; set; }
        public Type Type { get; set; }
        public int IdQuestion { get; set; }
        public string NameQuestion { get; set; }
        public int IdAnswer { get; set; }
        public byte IsDefault { get; set; }
        public string AnswerText { get; set; }
    }
}
