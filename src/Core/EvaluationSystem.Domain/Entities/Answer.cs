using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public byte IsDefault { get; set; }
        public int Position { get; set; }
        public ICollection<string> AnswerText = new HashSet<string>();
        public int IdQuestion { get; set; }
    }
}
