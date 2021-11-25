using System.Collections.Generic;
using EvaluationSystem.Application.Questions;

namespace EvaluationSystem.Application.Models.Modules
{
    public class GetModulesDto
    {
        public int IdForm { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public List<QuestionDto> QuestionsDtos { get; set; }
    }
}
