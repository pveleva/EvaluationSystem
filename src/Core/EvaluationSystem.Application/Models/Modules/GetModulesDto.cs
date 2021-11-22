using System.Collections.Generic;
using EvaluationSystem.Application.Questions;

namespace EvaluationSystem.Application.Models.Modules
{
    public class GetModulesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetQuestionsDto> QuestionsDtos { get; set; }
    }
}
