﻿using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces.IModuleQuestion
{
    public interface IModuleQuestionRepository : IGenericRepository<ModuleQuestion>
    {
        public void UpdateFromRepo(int moduleId, int questionId, int position);
    }
}
