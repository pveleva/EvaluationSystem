using System;

namespace EvaluationSystem.Application.Questions
{
    public class GetQuestionsDto
    {
        public int IdModule { get; set; }
        public string NameModule { get; set; }
        public int QuestionPosition { get; set; }
        public Domain.Entities.Type Type { get; set; }
        public int IdQuestion { get; set; }
        public string NameQuestion { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int IdAnswer { get; set; }
        public byte IsDefault { get; set; }
        public string AnswerText { get; set; }
    }
}
