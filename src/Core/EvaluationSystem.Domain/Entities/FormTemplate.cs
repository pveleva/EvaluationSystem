using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class FormTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ModuleTemplate> ModuleTemplates = new HashSet<ModuleTemplate>();
    }
}
