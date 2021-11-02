using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Questions
{
    public class UpdateQuestionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Domain.Entities.Type Type { get; set; }
    }
}
