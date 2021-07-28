using System.Collections.Generic;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Repositories.QuestionInInterviews
{
    public class QuestionInInterviewRepository : _BaseRepository, IQuestionInInterviewRepository
    {
        private AppDbContext AppDbContext;

        public QuestionInInterviewRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<List<QuestionInInterview>> GetQuestionInInterviews()
        {
            return await this.AppDbContext.QuestionInInterviews.ToListAsync();
        }

        public async Task<QuestionInInterview> Update(QuestionInInterview model)
        {
            var result = AppDbContext.QuestionInInterviews.Update(model);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
       
        public async Task<QuestionInInterview> Create(QuestionInInterview model)
        {
            var result = await this.AppDbContext.AddAsync(model);
            await this.AppDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<QuestionInInterview> Delete(QuestionInInterview model)
        {
            AppDbContext.QuestionInInterviews.Remove(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
    }
}
