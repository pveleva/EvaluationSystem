namespace EvaluationSystem.Application.Models.Modules
{
    public class GetModuleQuestionAnswerDto
    {
        public int IdModule { get; set; }
        public string NameModule { get; set; }
        public int IdQuestion { get; set; }
        public string NameQuestion { get; set; }
        public int IdAnswer { get; set; }
        public string AnswerText { get; set; }
    }
}
