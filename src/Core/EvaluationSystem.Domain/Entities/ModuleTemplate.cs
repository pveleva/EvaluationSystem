using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class ModuleTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<QuestionTemplate> QuestionTemplates = new HashSet<QuestionTemplate>();
    }
}
