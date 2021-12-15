using System.Collections.Generic;

namespace EvaluationSystem.Domain.Entities
{
    public class AttestationForm : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ModuleTemplate> ModuleTemplates = new HashSet<ModuleTemplate>();
    }
}
