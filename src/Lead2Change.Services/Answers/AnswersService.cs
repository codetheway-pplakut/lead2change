using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.AppEvents;
using Lead2Change.Repositories.Answers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Lead2Change.Services.Answers
{
    public class AnswersService : IAnswersService
    {
        private IAnswersRepository AnswersRepository;
        public AnswersService(AppDbContext dbContext)
        {
            this.AnswersRepository = new AnswersRepository(dbContext);
        }
        public async Task<Answer> GetAnswer(Guid id)
        {
            return await this.AnswersRepository.GetAnswer(id);
        }
        public async Task<List<Answer>> GetAnswers()
        {
            return await this.AnswersRepository.GetAnswers();
        }
        public async Task<Answer> Update(Answer model)
        {
            return await this.AnswersRepository.Update(model);
        }
    }
        public async Task<Answer> Create(Answer answer)
        {
            return await this.AnswersRepository.Create(answer);
        }
        public async Task<Answer> GetAnswer(Guid id)
        {
            return await this.AnswersRepository.GetAnswer(id);
        }
        public async Task<Answer> Delete(Answer answer)
        {
            return await AnswersRepository.Delete(answer);
        }
    }
}
