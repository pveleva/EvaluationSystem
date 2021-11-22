using System;
using Dapper;
using System.Data;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IForm;

namespace EvaluationSystem.Persistence.Dapper
{
    public class FormRepository : BaseRepository<FormTemplate>, IFormRepository
    {
        public FormRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public List<CreateUpdateFormDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<CreateUpdateFormDto> GetByIDFromRepo(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteFromRepo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string delete = @"DELETE FROM FormModule WHERE IdForm = @Id";
                dbConnection.Execute(delete, new { Id = id });

                dbConnection.Delete<FormTemplate>(id);
            }
        }

    }
}
