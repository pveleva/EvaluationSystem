using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Persistence
{
    public class Database
    {
        public List<Question> questionData = new List<Question>
        {
            new Question{Id = 1, Content= "Title 1" , Type = Domain.Entities.Type.CheckBox, Answers = new List<Answer>()},
            new Question{Id = 2, Content= "Title 2" , Type = Domain.Entities.Type.Numeric, Answers = new List<Answer>()},
            new Question{Id = 3, Content= "Title 3" ,Type = Domain.Entities.Type.RadioButton,  Answers = new List<Answer>()},
            new Question{Id = 4, Content= "Title 4" , Type = Domain.Entities.Type.TextField, Answers = new List<Answer>()},
        };

        public List<Answer> answerData = new List<Answer>
        {
            new Answer{Id = 1, Content = "Answer 1" , QuestionId = 1},
            new Answer{Id = 2, Content = "Answer 2" , QuestionId = 1},
            new Answer{Id = 3, Content = "Answer 3" , QuestionId = 1},
            new Answer{Id = 4, Content = "Answer 4" , QuestionId = 2},
            new Answer{Id = 5, Content = "Answer 5" , QuestionId = 3},
            new Answer{Id = 6, Content = "Answer 6" , QuestionId = 3},
            new Answer{Id = 7, Content = "Answer 7" , QuestionId = 4}
        };
    }
}
