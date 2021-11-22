using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class ModuleTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<QuestionTemplate> QuestionTemplates = new HashSet<QuestionTemplate>();
    }
}
