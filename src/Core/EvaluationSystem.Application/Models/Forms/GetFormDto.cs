using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Models.Forms
{
    public class GetFormDto
    {
        public string Name { get; set; }
        public ICollection<GetModulesDto> ModulesDtos { get; set; }
    }
}
