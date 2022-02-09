using Moq;
using System;
using AutoMapper;
using NUnit.Framework;
using System.Collections.Generic;
using EvaluationSystem.Persistence;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Persistence.Dapper;
using EvaluationSystem.Application.Answers;
using EvaluationSystem.Application.Profiles;
using EvaluationSystem.Application.Questions;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Services.Dapper;
using EvaluationSystem.Application.Interfaces.IQuestion;

namespace Tests
{
    public class Tests
    {
        private IAttestationQuestionService _questionService;
        private const int ID = 1;

        [SetUp]
        public void Setup()
        {
            var answerRepoMock = new Mock<IAnswerRepository>();
            var questionRepoMock = new Mock<IQuestionRepository>();

            var config = new ConfigurationBuilder()
                  .SetBasePath(Environment.CurrentDirectory)
                  .AddJsonFile("appsettings.json")
                  .Build();

            _questionService = new QuestionService(new MapperConfiguration((mc) =>
           {
               mc.AddMaps(typeof(AnswerProfile).Assembly);
           }).CreateMapper(), new QuestionRepository(new UnitOfWork(config)), new AnswerRepository(new UnitOfWork(config)));
        }

        [Test]
        public void GetByIdWorksCorrectly()
        {
            var result = _questionService.GetById(ID);
            Assert.That(result.Id == ID);
        }

        [Test]
        public void CreateQuestionDtoWorksCorrectly()
        {
            var insertable = new CreateModuleQuestionDto()
            {
                Name = "asd",
                IsReusable = 1,
                Type = EvaluationSystem.Domain.Entities.Type.RadioButton,
                AnswerText = new List<CreateUpdateAnswerDto>()
                {
                    new() { AnswerText = "Test" }
                }
            };

            var insert = _questionService.Create(insertable);

            var result = _questionService.GetById(insert.Id);
            Assert.That(insertable.Name == result.Name);
        }

        [Test]
        public void UpdateQuestionDtoWorksCorrectly()
        {
            string name = "What's your name?";
            UpdateQuestionDto dto = new UpdateQuestionDto { IsReusable = 1, Name = name, Type = EvaluationSystem.Domain.Entities.Type.CheckBox };
            var Update = _questionService.Update(ID + 1, dto);
            var result = _questionService.GetById(ID + 1);
            Assert.That(Update.Name == result.Name);
        }

    }
}