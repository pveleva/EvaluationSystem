﻿using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces.IForm
{
    public interface IFormService
    {
        List<GetFormDto> GetAll();
        GetFormDto GetById(int id);
        ExposeFormDto Create(CreateUpdateFormDto form);
        ExposeFormDto Update(int id, CreateUpdateFormDto formDto);
        void DeleteFromRepo(int id);
    }
}
