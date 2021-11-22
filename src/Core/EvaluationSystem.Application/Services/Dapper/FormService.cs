using System;
using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IForm;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class FormService : IFormService
    {
        private IMapper _mapper;
        private IFormRepository _formRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FormService(IMapper mapper, IFormRepository formRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _formRepository = formRepository;
            _unitOfWork = unitOfWork;
        }

        public List<CreateUpdateFormDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public CreateUpdateFormDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ExposeFormDto Create(CreateUpdateFormDto formDto)
        {
            using (_unitOfWork)
            {
                FormTemplate form = _mapper.Map<FormTemplate>(formDto);
                int formId = _formRepository.Create(form);
                form.Id = formId;

                _unitOfWork.Commit();
                return _mapper.Map<ExposeFormDto>(form);
            }
        }

        public ExposeFormDto Update(int id, CreateUpdateFormDto formDto)
        {
            using (_unitOfWork)
            {
                FormTemplate formToUpdate = _formRepository.GetByID(id);

                _mapper.Map(formDto, formToUpdate);

                formToUpdate.Id = id;
                _formRepository.Update(formToUpdate);

                _unitOfWork.Commit();
                return _mapper.Map<ExposeFormDto>(formToUpdate);
            }
        }

        public void DeleteFromRepo(int id)
        {
            using (_unitOfWork)
            {
                _formRepository.DeleteFromRepo(id);
                _unitOfWork.Commit();
            }
        }
    }
}
