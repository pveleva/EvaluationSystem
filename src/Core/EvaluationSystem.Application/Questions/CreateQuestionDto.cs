using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Questions
{
    public class CreateQuestionDto
    {
        public CreateQuestionDto()
        {
        }
        public string Content { get; set; }
        public Domain.Entities.Type Type { get; set; }

        public ICollection<Answer> Answers = new HashSet<Answer>();
    }
}
