using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Answers
{
    public interface IAnswersRepository
    {
        public Task<List<Answer>> GetAnswers();
        public Task<Answer> GetAnswer(Guid id);
        public Task<Answer> Update(Answer model);
        public Task<Answer> Create(Answer answer);
        public Task<Answer> Delete(Answer model);
        public Task<Answer> AnswerQuestion(Answer answer);

    }
}
