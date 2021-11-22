namespace EvaluationSystem.Application.Models.Forms
{
    public class GetFormModuleQuestionAnswerDto
    {
        public int IdForm { get; set; }
        public string NameForm { get; set; }
        public int IdModule { get; set; }
        public string NameModule { get; set; }
        public int IdQuestion { get; set; }
        public string NameQuestion { get; set; }
        public int IdAnswer { get; set; }
        public string AnswerText { get; set; }
    }
}
