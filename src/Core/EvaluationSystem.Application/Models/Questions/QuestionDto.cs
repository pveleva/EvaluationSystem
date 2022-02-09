using System;
using System.Collections.Generic;
using EvaluationSystem.Application.Answers;

namespace EvaluationSystem.Application.Questions
{
    public class QuestionDto
    {
        public QuestionDto()
        {
            AnswerText = new List<AnswerDto>();
        }
        public int IdModule { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Domain.Entities.Type Type { get; set; }
        public int Position { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<AnswerDto> AnswerText { get; set; }
    }
}
