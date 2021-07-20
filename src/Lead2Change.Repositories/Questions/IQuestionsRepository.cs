using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Repositories.Questions
{
    public interface IQuestionsRepository
    {
        public Task<List<Question>> GetQuestions();
        public Task<List<Question>> GetArchivedQuestions();
        public Task<Question> GetQuestion(Guid id);
        public Task<Question> Update(Question model);
        public Task<Question> Create(Question question);
        public Task<Question> Delete(Question question);
    }
}
