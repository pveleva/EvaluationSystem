using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public byte IsReusable { get; set; }

        public ICollection<Answer> AnswerText = new HashSet<Answer>();
    }
}
