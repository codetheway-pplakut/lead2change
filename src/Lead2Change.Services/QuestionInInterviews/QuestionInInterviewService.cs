using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Goals;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

using Lead2Change.Data.Contexts;
using Lead2Change.Repositories.QuestionInInterviews;

namespace Lead2Change.Services.QuestionInInterviews
{
    public class QuestionInInterviewService : IQuestionInInterviewService
    {
        private IQuestionInInterviewRepository _questionInInterviewRepository;

        public QuestionInInterviewService(AppDbContext dbContext)
        {
            this._questionInInterviewRepository = new QuestionInInterviewRepository(dbContext);
        }
        
        public async Task<QuestionInInterview> Update(QuestionInInterview model)
        {
            return await this._questionInInterviewRepository.Update(model);
        }
        public async Task<QuestionInInterview> Create(QuestionInInterview model)
        {
            return await this._questionInInterviewRepository.Create(model);
        }
        public async Task<List<QuestionInInterview>> GetQuestionInInterviews()
        {
            return await this._questionInInterviewRepository.GetQuestionInInterviews();

        }
        public async Task<QuestionInInterview> Delete(QuestionInInterview model)
        {
            return await _questionInInterviewRepository.Delete(model);
        }
    }
}
