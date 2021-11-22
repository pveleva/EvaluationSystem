using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Interfaces.IFormModule;

namespace EvaluationSystem.Persistence.Dapper
{
    public class FormModuleRepository : BaseRepository<FormModule>, IFormModuleRepository
    {
        public FormModuleRepository(IConfiguration configuration)
       : base(configuration)
        {
        }
    }
}
