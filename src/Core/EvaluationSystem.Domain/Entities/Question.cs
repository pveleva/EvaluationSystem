using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Domain.Entities
{
    public class Question
    {
        public Question()
        {
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public Type Type { get; set; }

        public ICollection<Answer> Answers = new HashSet<Answer>();
    }
}
