using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.AppEvents;
using Lead2Change.Repositories.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Questions
{
    public class QuestionsService : IQuestionsService
    {
        private IQuestionsRepository QuestionsRepository;

        public QuestionsService(AppDbContext dbContext)
        {
            this.QuestionsRepository = new QuestionsRepository(dbContext);
        }
        public async Task<List<Question>> GetQuestions()
        {
            return await this.QuestionsRepository.GetQuestions();
        }
        public async Task<List<Question>> GetArchivedQuestions()
        {
            return await this.QuestionsRepository.GetArchivedQuestions();
        }
        public async Task<Question> GetQuestion(Guid id)
        {
            return await this.QuestionsRepository.GetQuestion(id);
        }
        public async Task<Question> Update(Question model)
        {
            return await this.QuestionsRepository.Update(model);
        }
        public async Task<Question> Create(Question question)
        {
            return await this.QuestionsRepository.Create(question);
        }
        public async Task<Question> Delete(Question question)
        {
            return await QuestionsRepository.Delete(question);
        }
    }
}
