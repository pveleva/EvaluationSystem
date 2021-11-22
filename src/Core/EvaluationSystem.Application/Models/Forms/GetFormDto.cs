using System.Collections.Generic;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Models.Forms
{
    public class GetFormDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdModule { get; set; }
        public List<GetModulesDto> ModulesDtos { get; set; }
    }
}
