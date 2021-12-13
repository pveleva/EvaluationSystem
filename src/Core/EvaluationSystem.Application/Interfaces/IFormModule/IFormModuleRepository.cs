using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces.IFormModule
{
    public interface IFormModuleRepository : IGenericRepository<FormModule>
    {
        public void UpdateFromRepo(int formId, int moduleId, int position);
    }
}
