using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationSystem.Persistence.EF.Repositories
{
    public class AnswerRepository : BaseRepository<AnswerTemplate>, IAnswerRepository
    {
        private EvaluationSystemDbContext _context;
        public AnswerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<AnswerTemplate> GetAll(int questionId)
        {
            _context.ModuleTemplates.Include(mt => mt.QuestionTemplates).ToList();
        }
    }
}
