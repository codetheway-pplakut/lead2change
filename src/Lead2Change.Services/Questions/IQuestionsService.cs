using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Questions
{
    public interface IQuestionsService
    {
        public Task<List<Question>> GetQuestions();
        public Task<List<Question>> GetArchivedQuestions();
        public Task<Question> GetQuestion(Guid id);
        public Task<Question> Create(Question question);
        public Task<Question> Update(Question model);
        public Task<Question> Delete(Question question);
        public Task<Question> PermanentDelete(Question question);
    }
}
