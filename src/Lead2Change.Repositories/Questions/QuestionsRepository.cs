using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Repositories.Questions
{
    public class QuestionsRepository : _BaseRepository, IQuestionsRepository
    {
        private AppDbContext AppDbContext;

        public QuestionsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<List<Question>> GetQuestions()
        {
            return await this.AppDbContext.Questions.Where(x => x.IsArchived == false).OrderBy( q => q.QuestionString).ToListAsync();
        }
        public async Task<List<Question>> GetArchivedQuestions()
        {
            return await this.AppDbContext.Questions.Where(x => x.IsArchived == true).OrderBy(q => q.QuestionString).ToListAsync();
        }
        public async Task<Question> GetQuestion(Guid id)
        {                     
            return await this.AppDbContext.Questions.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Question> Update(Question model)
        {
            var result = AppDbContext.Questions.Update(model);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Question> Create(Question question)
        {
            var result = await this.AppDbContext.AddAsync(question);
            await this.AppDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Question> Delete(Question model)
        {
            model.IsArchived = true;
            AppDbContext.Questions.Update(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
        public async Task<Question> PermanentDelete(Question model)
        {
            AppDbContext.Questions.Remove(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
    }
}
