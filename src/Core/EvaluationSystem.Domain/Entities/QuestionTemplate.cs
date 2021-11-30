﻿using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class QuestionTemplate : BaseEntity
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool IsReusable { get; set; }

        public ICollection<AnswerTemplate> AnswerText = new HashSet<AnswerTemplate>();
    }
}
