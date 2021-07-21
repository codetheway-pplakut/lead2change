using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace Lead2Change.Repositories.Answers
{
    public class AnswersRepository : _BaseRepository, IAnswersRepository
    {
        private AppDbContext AppDbContext;
        public AnswersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<Answer> Create(Answer answer)
        {
            var result = await this.AppDbContext.AddAsync(answer);
            await this.AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Answer> GetAnswer(Guid id)
        {
            return await this.AppDbContext.Answers.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Answer> Delete(Answer model)
        {
            AppDbContext.Answers.Remove(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
        public async Task<List<Answer>> GetAnswers()
        {
            return await this.AppDbContext.Answers.ToListAsync();
        }
        public async Task<Answer> Update(Answer model)
        {
            var result = AppDbContext.Answers.Update(model);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Answer> AnswerQuestion(Answer answer)
        {
            var result = await this.AppDbContext.AddAsync(answer);
            await this.AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
