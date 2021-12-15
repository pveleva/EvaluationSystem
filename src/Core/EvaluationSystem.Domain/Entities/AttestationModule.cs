using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class AttestationModule : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<QuestionTemplate> QuestionTemplates = new HashSet<QuestionTemplate>();
    }
}
